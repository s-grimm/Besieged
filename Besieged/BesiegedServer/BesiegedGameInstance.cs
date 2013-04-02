using Framework;
using Framework.Commands;
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

namespace BesiegedServer
{
    public class BesiegedGameInstance
    {
        public ConcurrentBag<Player> Players { get; set; }
        public BlockingCollection<Command> MessageQueue { get; set; }
        public string GameId { get; set; }
        public string Name { get; set; }
        public int MaxPlayers { get; set; }
        public GameState GameState { get; set; }
        private string m_GameCreatorClientId { get; set; }
        public bool IsGameInstanceFull { get; set; }
        public string Password { get; set; }
        public Stack<PlayerColor.PlayerColorEnum> ColorPool { get; set; }

        enum State { WaitingForPlayers, AllPlayersReady, GameStarted };
        enum Trigger { AllPlayersReady, PlayerNotReady, CreatorPressedStart };

        StateMachine<State, Trigger> m_GameMachine;
        State m_CurrentState = State.WaitingForPlayers;
        
        //public BesiegedGameInstance()
        //{
        //    Players = new ConcurrentBag<Player>();
        //    MessageQueue = new BlockingCollection<Command>();
        //    ColorPool = PlayerColor.GetColors();
        //    IsGameInstanceFull = false;
        //    MaxPlayers = 2;
        //    Password = "";

        //    ConfigureMachine();
        //}

        public BesiegedGameInstance(string gameId, string name, int maxPlayers, string password, string creatorId)
        {
            GameId = gameId;
            Name = name;
            MaxPlayers = maxPlayers;
            m_GameCreatorClientId = creatorId;
            Players = new ConcurrentBag<Player>();
            MessageQueue = new BlockingCollection<Command>();
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
                    WaitingForPlayer waiting = new WaitingForPlayer();
                    LookupPlayerById(m_GameCreatorClientId).Callback.SendMessage(waiting.ToXml());
                })
                .Ignore(Trigger.PlayerNotReady)
                .Ignore(Trigger.CreatorPressedStart);

            m_GameMachine.Configure(State.AllPlayersReady)
                .OnEntry(x => 
                    {
                        AllAreReady ready = new AllAreReady();
                        LookupPlayerById(m_GameCreatorClientId).Callback.SendMessage(ready.ToXml());
                    })
                .Permit(Trigger.PlayerNotReady, State.WaitingForPlayers)
                .Permit(Trigger.CreatorPressedStart, State.GameStarted);

            m_GameMachine.Configure(State.GameStarted)
                .OnEntry(x =>
                {
                    StartGame start = new StartGame();
                    NotifyAllPlayers(start.ToXml());
                })
                .Ignore(Trigger.PlayerNotReady);
        }

        private void ProcessMessages()
        {
            IDisposable genericGameMessageSubscriber = BesiegedServer.MessagePublisher
                .Where(message => message is GenericGameMessage && message.GameId == GameId)
                .Subscribe(message =>
                {
                    var genericMessage = message as GenericGameMessage;
                    switch (genericMessage.MessageEnum)
                    {
                        case GameMessage.GameMessageEnum.PlayerReady:
                            LookupPlayerById(message.ClientId).IsReady.Value = true;
                            break;
                        case GameMessage.GameMessageEnum.PlayerNotReady:
                            LookupPlayerById(message.ClientId).IsReady.Value = false;
                            break;
                        case GameMessage.GameMessageEnum.Start:
                            m_GameMachine.Fire(Trigger.CreatorPressedStart);
                            break;
                        case GameMessage.GameMessageEnum.PlayerJoin:
                            break;
                        default:
                            break;
                    }
                });

            IDisposable gameMessageSubscriber = BesiegedServer.MessagePublisher
                .Where(message => message is GameMessage && !(message is GameMessage))
                .Subscribe(message =>
                {
                    // do stuff with server bound messages here
                });
        }

        private void CheckIfAllAreReady()
        {
            if (Players.All(p => p.IsReady.Value))
            {
                m_GameMachine.Fire(Trigger.AllPlayersReady);
            }
        }

        private void Transition(string message)
        {
            CommandChatMessage chat = new CommandChatMessage()
            {
                Contents = message
            };
            NotifyAllPlayers(chat.ToXml());
        }

        public void AddPlayer(ConnectedClient client)
        {
            Player player = new Player(client.Name, client.ClientId, client.Callback, ColorPool.Pop());
            Players.Add(player);
            player.IsReady.ValueChanged += (from, to) =>
            {
                PlayerChangedInfo playerChanged = new PlayerChangedInfo()
                {
                    ClientId = player.ClientId,
                    Name = player.Name,
                    Color = player.PlayerColor,
                    IsReady = to
                };

                NotifyAllPlayers(playerChanged.ToXml());
                
                if (to)
                {
                    CheckIfAllAreReady();
                }
                else
                {
                    m_GameMachine.Fire(Trigger.PlayerNotReady);
                }
            };

            CommandAggregate aggregate = new CommandAggregate();

            PlayerGameInfo playerGameInfo = new PlayerGameInfo()
            {
                GameId = this.GameId,
                Color = player.PlayerColor,
                IsCreator = (Players.Count == 1) ? true : false
            };
            if (Players.Count == 0)
            {
                playerGameInfo.IsCreator = true;
            }

            aggregate.Commands.Add(playerGameInfo);

            PlayerChangedInfo playerChangedInfo = new PlayerChangedInfo()
            {
                ClientId = player.ClientId,
                Name = player.Name,
                Color = player.PlayerColor
            };

            foreach (Player p in Players)
            {
                if (p.ClientId != player.ClientId)
                {
                    p.Callback.SendMessage(playerChangedInfo.ToXml());
                }
                aggregate.Commands.Add(new PlayerChangedInfo()
                {
                    ClientId = p.ClientId,
                    Name = p.Name,
                    Color = p.PlayerColor,
                    IsReady = p.IsReady.Value
                });
            }
            player.Callback.SendMessage(aggregate.ToXml());
        }

        public void ProcessMessage(Command command)
        {
            if (command is CommandChatMessage)
            {
                CommandChatMessage commandChatMessage = command as CommandChatMessage;
                string message = string.Format("{0}: {1}", LookupPlayerName(commandChatMessage.ClientId), commandChatMessage.Contents);
                commandChatMessage.Contents = message;
                NotifyAllPlayers(commandChatMessage.ToXml());
            }
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
                player.Callback.SendMessage(message);
            }
        }
    }
}
