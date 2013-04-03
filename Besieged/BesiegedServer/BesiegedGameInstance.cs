using Framework;
using Framework.Unit;
using System;
using Stateless;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Reactive.Concurrency;
using Framework.Utilities.Xml;
using Framework.ServiceContracts;
using Framework.BesiegedMessages;
using Utilities;

namespace BesiegedServer
{
    public class BesiegedGameInstance : IDisposable
    {
        public ConcurrentBag<Player> Players { get; set; }
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

        enum State { WaitingForPlayers, AllPlayersReady, GameStarted, PlayerTurn };
        enum Trigger { AllPlayersReady, PlayerNotReady, CreatorPressedStart, GameStarted };

        StateMachine<State, Trigger> m_GameMachine;
        State m_CurrentState = State.WaitingForPlayers;
        
        public BesiegedGameInstance(string gameId, string name, int maxPlayers, string password, string creatorId)
        {
            GameId = gameId;
            Name = name;
            MaxPlayers = maxPlayers;
            m_GameCreatorClientId = creatorId;
            Players = new ConcurrentBag<Player>();
            ColorPool = PlayerColor.GetColors();
            IsGameInstanceFull = false;
            Password = password;

            ConfigureMachine();
        }

        private void ConfigureMachine()
        {

            m_GameMachine = new StateMachine<State, Trigger>(() => m_CurrentState, newState => m_CurrentState = newState);
            ProcessMessages();

            m_GameMachine.Configure(State.WaitingForPlayers)
                .Permit(Trigger.AllPlayersReady, State.AllPlayersReady)
                .OnEntryFrom(Trigger.PlayerNotReady, x =>
                {
                    GenericClientMessage waiting = new GenericClientMessage() { MessageEnum = ClientMessage.ClientMessageEnum.PlayerNotReady };
                    LookupPlayerById(m_GameCreatorClientId).Callback.SendMessage(waiting.ToXml());

                })
                .Ignore(Trigger.PlayerNotReady)
                .Ignore(Trigger.CreatorPressedStart);

            m_GameMachine.Configure(State.AllPlayersReady)
                .OnEntry(x => 
                    {
                        GenericClientMessage ready = new GenericClientMessage() { MessageEnum = ClientMessage.ClientMessageEnum.AllPlayersReady };
                        LookupPlayerById(m_GameCreatorClientId).Callback.SendMessage(ready.ToXml());

                    })
                .Permit(Trigger.PlayerNotReady, State.WaitingForPlayers)
                .Permit(Trigger.CreatorPressedStart, State.GameStarted);

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
                    
                    ConsoleLogger.Push(String.Format("Game {0} has been started", Name));
                    NotifyAllPlayers(new GenericClientMessage() { MessageEnum = ClientMessage.ClientMessageEnum.TransitionToLoadingState }.ToXml()); // switch all the players to loading while we send the gamestate
                    ClientGameStateMessage gamestate = new ClientGameStateMessage() { State = GameState };
                    GenericClientMessage start = new GenericClientMessage() { MessageEnum = ClientMessage.ClientMessageEnum.StartGame };
                    AggregateMessage aggregate = new AggregateMessage();
                    aggregate.MessageList.Add(gamestate);
                    aggregate.MessageList.Add(start);
                    NotifyAllPlayers(aggregate.ToXml());

                    
                })
                .Ignore(Trigger.PlayerNotReady);

            m_GameMachine.Configure(State.PlayerTurn)
                .OnEntry(x =>
                {
                    // notify the current player that its their turn
                    m_CurrentPlayer = m_PlayerTurnOrder.Dequeue();
                    m_CurrentPlayer.Callback.SendMessage((new GenericClientMessage() { MessageEnum = ClientMessage.ClientMessageEnum.ActiveTurn }).ToXml());
                    // notify all other players that they have to wait
                    foreach (Player player in m_PlayerTurnOrder)
                    {
                        player.Callback.SendMessage((new GenericClientMessage() { MessageEnum = ClientMessage.ClientMessageEnum.WaitingForTurn }).ToXml());
                    }
                    // add the current player back on the queue
                    m_PlayerTurnOrder.Enqueue(m_CurrentPlayer);
                });
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
                });
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
                // we probably need to reconfigure here especially if we're already in the GameStarte state
            }
        }

        public void AddPlayer(ConnectedClient client)
        {
            Player player = new Player(client.Name, client.ClientId, client.Callback, ColorPool.Pop());
            Players.Add(player);
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

        public void Dispose()
        {
            m_GameMessageSubscriber.Dispose();
            m_GenericGameMessageSubscriber.Dispose();
        }
    }
}
