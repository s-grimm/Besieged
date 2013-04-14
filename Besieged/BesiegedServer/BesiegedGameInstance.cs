using BesiegedServer.Pathing;
using Framework;
using Framework.BesiegedMessages;
using Framework.Unit;
using Framework.Utilities.Xml;
using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Utilities;

namespace BesiegedServer
{
    public class BesiegedGameInstance : IDisposable
    {
        public List<Player> Players { get; set; }

        public string GameId { get; set; }

        public string Name { get; set; }

        public int MaxPlayers { get; set; }

        public GameState GameState { get; set; }

        private string m_GameCreatorClientId { get; set; }

        public bool IsGameInstanceFull { get; set; }

        public string Password { get; set; }

        public Stack<PlayerColor.PlayerColorEnum> ColorPool { get; set; }

        private Queue<Player> m_PlayerTurnOrder = new Queue<Player>();
        private Player m_CurrentPlayer;

        private IDisposable m_GenericGameMessageSubscriber { get; set; }

        private IDisposable m_GameMessageSubscriber { get; set; }

        private Pathing.PathFinder pathFinder;

        private enum State { WaitingForPlayers, AllPlayersReady, GameStarted, PlayerTurn, Reconfigure, BattlePhase };

        private enum Trigger { AllPlayersReady, PlayerNotReady, CreatorPressedStart, GameStarted, PlayerLeft, PlayerTurn, StartBattlePhase };

        private StateMachine<State, Trigger> m_GameMachine;
        private State m_CurrentState = State.WaitingForPlayers;

        public BesiegedGameInstance(string gameId, string name, int maxPlayers, string password, string creatorId)
        {
            GameId = gameId;
            Name = name;
            MaxPlayers = maxPlayers;
            m_GameCreatorClientId = creatorId;
            Players = new List<Player>();
            ColorPool = PlayerColor.GetColors();
            IsGameInstanceFull = false;
            Password = password;

            ConfigureMachine();
        }

        private void ConfigureMachine()
        {
            m_GameMachine = new StateMachine<State, Trigger>(() => m_CurrentState, newState => m_CurrentState = newState);
            ProcessMessages();

            #region WaitingForPlayers
            m_GameMachine.Configure(State.WaitingForPlayers)
                .Permit(Trigger.AllPlayersReady, State.AllPlayersReady)
                .OnEntryFrom(Trigger.PlayerNotReady, x =>
                {
                    GenericClientMessage waiting = new GenericClientMessage() { MessageEnum = ClientMessage.ClientMessageEnum.PlayerNotReady };
                    LookupPlayerById(m_GameCreatorClientId).Callback.SendMessage(waiting.ToXml());
                })
                .OnEntryFrom(Trigger.PlayerLeft, x =>
                {
                    SendUpdatedGameInfo();
                })
                .PermitReentry(Trigger.PlayerLeft)
                .Ignore(Trigger.PlayerNotReady)
                .Ignore(Trigger.CreatorPressedStart);
            #endregion

            #region AllPlayersReady
            m_GameMachine.Configure(State.AllPlayersReady)
                .OnEntry(x =>
                {
                    GenericClientMessage ready = new GenericClientMessage() { MessageEnum = ClientMessage.ClientMessageEnum.AllPlayersReady };
                    LookupPlayerById(m_GameCreatorClientId).Callback.SendMessage(ready.ToXml());
                })
                .Permit(Trigger.PlayerNotReady, State.WaitingForPlayers)
                .Permit(Trigger.PlayerLeft, State.WaitingForPlayers)
                .Permit(Trigger.CreatorPressedStart, State.GameStarted);
            #endregion

            #region GameStarted
            m_GameMachine.Configure(State.GameStarted)
                .OnEntry(x =>
                {
                    List<KeyValuePair<string, Army.ArmyTypeEnum>> PlayerInfos = new List<KeyValuePair<string, Army.ArmyTypeEnum>>();
                    foreach (Player player in Players)
                    {
                        PlayerInfos.Add(new KeyValuePair<string, Army.ArmyTypeEnum>(player.ClientId, player.ArmyType));
                        m_PlayerTurnOrder.Enqueue(player);
                    }
                    GameState = new GameState(PlayerInfos);
                    pathFinder = new PathFinder(GameState);
                    ConsoleLogger.Push(String.Format("Game {0} has been started", Name));
                    NotifyAllPlayers(new GenericClientMessage() { MessageEnum = ClientMessage.ClientMessageEnum.TransitionToLoadingState }.ToXml()); // switch all the players to loading while we send the gamestate
                    ClientGameStateMessage gamestate = new ClientGameStateMessage() { State = GameState };
                    GenericClientMessage start = new GenericClientMessage() { MessageEnum = ClientMessage.ClientMessageEnum.StartGame };
                    AggregateMessage aggregate = new AggregateMessage();
                    aggregate.MessageList.Add(gamestate);
                    aggregate.MessageList.Add(start);
                    NotifyAllPlayers(aggregate.ToXml());
                    m_GameMachine.Fire(Trigger.GameStarted);
                })
                .Permit(Trigger.GameStarted, State.PlayerTurn)
                .Ignore(Trigger.PlayerNotReady);
            #endregion

            #region PlayerTurn
            m_GameMachine.Configure(State.PlayerTurn)
                .PermitReentry(Trigger.PlayerTurn)
                .Permit(Trigger.PlayerLeft, State.Reconfigure)
                .Permit(Trigger.StartBattlePhase, State.BattlePhase)
                .OnEntry(x =>
                {
                    // notify the current player that its their turn
                    m_CurrentPlayer = m_PlayerTurnOrder.Dequeue();
                    m_CurrentPlayer.Callback.SendMessage((new GenericClientMessage() { MessageEnum = ClientMessage.ClientMessageEnum.ActiveTurn }).ToXml());

                    // notify all other players that they have to wait
                    foreach (Player player in m_PlayerTurnOrder)
                    {
                        player.Callback.SendMessage((new WaitingForTurnMessage() { ActivePlayerName = m_CurrentPlayer.Name }).ToXml());
                    }

                    // add the current player back on the queue
                    m_PlayerTurnOrder.Enqueue(m_CurrentPlayer);
                });
            #endregion

            m_GameMachine.Configure(State.BattlePhase)
                .OnEntry(x =>
                {

                });

            #region Reconfigure
            m_GameMachine.Configure(State.Reconfigure)
                .OnEntry(x =>
                {
                    // this state allows us to reconfigure the players involved in the game in case someone leaves or is defeated
                    Queue<Player> tempPlayers = new Queue<Player>();
                    foreach (Player player in m_PlayerTurnOrder)
                    {
                        if (Players.FirstOrDefault(p => p.ClientId == player.ClientId) != null)
                        {
                            tempPlayers.Enqueue(player);
                        }
                    }
                    if (tempPlayers.Count > 0)
                    {
                        m_PlayerTurnOrder.Clear();
                        m_PlayerTurnOrder = tempPlayers;
                        m_GameMachine.Fire(Trigger.PlayerTurn);
                    }
                })
                .Permit(Trigger.PlayerTurn, State.PlayerTurn);
            #endregion
        }

        private void ProcessMessages()
        {
            m_GenericGameMessageSubscriber = BesiegedServer.MessageSubject
                .Where(message => message is GenericGameMessage && message.GameId == GameId)
                .Subscribe(message =>
                {
                    var genericMessage = message as GenericGameMessage;
                    switch (genericMessage.MessageEnum)
                    {
                        case GameMessage.GameMessageEnum.PlayerNotReady:
                            LookupPlayerById(message.ClientId).IsReady.Value = false;
                            break;

                        case GameMessage.GameMessageEnum.Start:
                            m_GameMachine.Fire(Trigger.CreatorPressedStart);
                            break;

                        case GameMessage.GameMessageEnum.PlayerLeft:
                            RemovePlayer(message.ClientId);
                            ConsoleLogger.Push(string.Format("Leave game command received from {0} Client Id {1} in game {2}", LookupPlayerName(message.ClientId), message.ClientId, Name));
                            break;

                        case GameMessage.GameMessageEnum.StartBattlePhase:

                            break;

                        default:
                            break;
                    }
                });

            m_GameMessageSubscriber = BesiegedServer.MessageSubject
                .Where(message => message is GameMessage && !(message is GenericGameMessage) && message.GameId == GameId)
                .Subscribe(message =>
                {
                    if (message is GameChatMessage)
                    {
                        string chatMessage = string.Format("{0}: {1}", LookupPlayerName(message.ClientId), (message as GameChatMessage).Contents);
                        ClientChatMessage clientChat = new ClientChatMessage() { Contents = chatMessage };
                        NotifyAllPlayers(clientChat.ToXml());
                    }

                    else if (message is JoinGameMessage)
                    {
                        if ((message as JoinGameMessage).Password == Password)
                        {
                            AddPlayer(BesiegedServer.GetConnectedClientById(message.ClientId));
                        }
                        else
                        {
                            ErrorDialogMessage error = new ErrorDialogMessage() { Contents = "Incorrect Password!" };
                            BesiegedServer.GetConnectedClientById(message.ClientId).Callback.SendMessage(error.ToXml());
                        }
                    }

                    else if (message is PlayerReadyMessage)
                    {
                        var player = LookupPlayerById(message.ClientId);
                        player.IsReady.Value = true;
                        player.ArmyType = (message as PlayerReadyMessage).ArmyType;
                    }

                    else if (message is EndMoveTurnMessage)
                    {
                        // we need to validate all of the users moves against the server's gamestate to make sure they are valid
                        EndMoveTurnMessage endMessage = message as EndMoveTurnMessage;

                        List<UnitMove> InvalidMoves = new List<UnitMove>();
                        endMessage.Moves.ForEach(move =>
                        {
                            if (!ValidateMove(move)) InvalidMoves.Add(move);
                        });

                        if (InvalidMoves.Count == 0)
                        {
                            endMessage.Moves.ForEach(move => // update the positions of all the units
                                {
                                    BaseUnit selectedUnit =
                                        GameState.Units.FirstOrDefault(
                                            unit =>
                                            unit.X_Position == move.StartCoordinate.XCoordinate &&
                                            unit.Y_Position == move.StartCoordinate.YCoordinate);
                                    if (selectedUnit == null) return;
                                    selectedUnit.X_Position = move.EndCoordinate.XCoordinate;
                                    selectedUnit.Y_Position = move.EndCoordinate.YCoordinate;
                                    selectedUnit.MovementLeft = selectedUnit.Movement;
                                });
                            Players.Where(x => x.ClientId != message.ClientId).ToList().ForEach(player => player.Callback.SendMessage((new UpdatedUnitPositionMessage() { Moves = endMessage.Moves }).ToXml()));
                            if (pathFinder.IsAnyUnitWithinAttackableRange(message.ClientId))
                            {
                                LookupPlayerById(message.ClientId).Callback.SendMessage((new GenericClientMessage() { MessageEnum = ClientMessage.ClientMessageEnum.StartBattlePhase }).ToXml());
                            }
                            else
                            {
                                m_GameMachine.Fire(Trigger.PlayerTurn);
                            }
                        }
                        else
                        {
                            // we need to notify the client of invalid moves
                        }
                    }
                });
        }

        private bool ValidateMove(UnitMove move)
        {
            BaseUnit selectedUnit = GameState.Units.FirstOrDefault(unit => unit.X_Position == move.StartCoordinate.XCoordinate && unit.Y_Position == move.StartCoordinate.YCoordinate);
            if (selectedUnit == null) return false;

            int totalMovementCost = pathFinder.FindPath(move.StartCoordinate.XCoordinate, move.StartCoordinate.YCoordinate, move.EndCoordinate.XCoordinate, move.EndCoordinate.YCoordinate);
            if (totalMovementCost == -1 || totalMovementCost > selectedUnit.MovementLeft) return false;
            selectedUnit.MovementLeft -= totalMovementCost;
            return true;
        }

        private void CheckIfAllAreReady()
        {
            if (Players.All(p => p.IsReady.Value))
            {
                m_GameMachine.Fire(Trigger.AllPlayersReady);
            }
        }

        public void RemovePlayer(string clientId)
        {
            if (clientId == m_GameCreatorClientId)
            {
                GenericClientMessage disbanded = new GenericClientMessage() { MessageEnum = ClientMessage.ClientMessageEnum.GameDisbanded };
                NotifyAllPlayers(disbanded.ToXml());
                BesiegedServer.DisbandGame(GameId);
            }
            else
            {
                Player player = Players.FirstOrDefault(x => x.ClientId == clientId);
                Players.Remove(player);
                ConsoleLogger.Push(string.Format("{0} with ClientId {1} has left game {2}", player.Name, player.ClientId, Name));

                AggregateMessage aggregate = new AggregateMessage();
                GenericClientMessage remove = new GenericClientMessage() { MessageEnum = ClientMessage.ClientMessageEnum.RemovePlayer, ClientId = player.ClientId };
                ClientChatMessage chatMessage = new ClientChatMessage() { Contents = string.Format("* {0} has left the game *", player.Name) };
                aggregate.MessageList.Add(remove);
                aggregate.MessageList.Add(chatMessage);
                NotifyAllPlayers(aggregate.ToXml());

                ColorPool.Push(player.PlayerColor);
                player.Callback.SendMessage((new GenericClientMessage() { MessageEnum = ClientMessage.ClientMessageEnum.TransitionToMultiplayerMenuState }).ToXml());
                player = null;
                m_GameMachine.Fire(Trigger.PlayerLeft);
            }
        }

        public void AddPlayer(ConnectedClient client)
        {
            Player player = new Player(client.Name, client.ClientId, client.Callback, ColorPool.Pop());
            Players.Add(player);
            ConsoleLogger.Push(string.Format("{0} with ClientId {1} has joined game {2}", player.Name, player.ClientId, Name));
            SendUpdatedGameInfo();
            player.IsReady.ValueChanged += (from, to) =>
            {
                PlayerInfoMessage playerInfo = new PlayerInfoMessage()
                {
                    ClientId = player.ClientId,
                    Name = player.Name,
                    Color = player.PlayerColor,
                    IsReady = to
                };

                NotifyAllPlayers(playerInfo.ToXml());

                if (to)
                {
                    CheckIfAllAreReady();
                }
                else
                {
                    m_GameMachine.Fire(Trigger.PlayerNotReady);
                }
            };

            AggregateMessage aggregate = new AggregateMessage();

            PlayerGameInfoMessage playerGameInfo = new PlayerGameInfoMessage()
            {
                GameId = this.GameId,
                IsCreator = (Players.Count == 1) ? true : false
            };

            aggregate.MessageList.Add(playerGameInfo);

            PlayerInfoMessage playerInfoMessage = new PlayerInfoMessage()
            {
                ClientId = player.ClientId,
                Name = player.Name,
                Color = player.PlayerColor,
                IsReady = false
            };

            foreach (Player p in Players)
            {
                if (p.ClientId != player.ClientId)
                {
                    p.Callback.SendMessage(playerInfoMessage.ToXml());
                }
                aggregate.MessageList.Add(new PlayerInfoMessage()
                {
                    ClientId = p.ClientId,
                    Name = p.Name,
                    Color = p.PlayerColor,
                    IsReady = p.IsReady.Value
                });
            }
            player.Callback.SendMessage(aggregate.ToXml());
        }

        public string LookupPlayerName(string clientId)
        {
            var player = Players.Where(x => x.ClientId == clientId).FirstOrDefault();
            if (player == null)
            {
                return "*Player Left*";
            }
            else
            {
                return player.Name;
            }
        }

        public Player LookupPlayerById(string clientId)
        {
            var player = Players.Where(x => x.ClientId == clientId).FirstOrDefault();
            return player;
        }

        public void NotifyAllPlayers(string message)
        {
            foreach (Player player in Players)
            {
                player.Callback.SendMessage(message); //timeout here
            }
        }

        public void SendUpdatedGameInfo()
        {
            string capacity = string.Format("{0}/{1} players", Players.Count, MaxPlayers);
            GameInfoMessage gameInfo = new GameInfoMessage(GameId, Name, capacity, false, Password != string.Empty ? true : false);
            BesiegedServer.NotifyAllConnectedClients(gameInfo.ToXml());
        }

        public void Dispose()
        {
            m_GameMessageSubscriber.Dispose();
            m_GenericGameMessageSubscriber.Dispose();
        }
    }
}