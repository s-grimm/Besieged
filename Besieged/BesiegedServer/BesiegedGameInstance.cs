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
using Framework.Utilities.Xml;
using Framework.ServiceContracts;

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
        public string GameCreatorClientId { get; set; }
        public bool IsGameInstanceFull { get; set; }
        public string Password { get; set; }
        public Stack<PlayerColor.PlayerColorEnum> ColorPool { get; set; }

        enum State { WaitingForPlayers, AllPlayersReady };
        enum Trigger { AllPlayersReady, PlayerNotReady };

        StateMachine<State, Trigger> m_GameMachine;
        State m_CurrentState = State.WaitingForPlayers;
        
        public BesiegedGameInstance()
        {
            Players = new ConcurrentBag<Player>();
            MessageQueue = new BlockingCollection<Command>();
            ColorPool = PlayerColor.GetColors();
            IsGameInstanceFull = false;
            MaxPlayers = 2;
            Password = "";

            StartProcessingMessages();
        }

        public BesiegedGameInstance(string gameId, string name, int maxPlayers)
        {
            GameId = gameId;
            Name = name;
            MaxPlayers = maxPlayers;
            Players = new ConcurrentBag<Player>();
            MessageQueue = new BlockingCollection<Command>();
            ColorPool = PlayerColor.GetColors();
            IsGameInstanceFull = false;
            Password = string.Empty;

            StartProcessingMessages();
        }

        public BesiegedGameInstance(string gameId, string name, int maxPlayers, string password)
        {
            GameId = gameId;
            Name = name;
            MaxPlayers = maxPlayers;
            Players = new ConcurrentBag<Player>();
            MessageQueue = new BlockingCollection<Command>();
            ColorPool = PlayerColor.GetColors();
            IsGameInstanceFull = false;
            Password = password;

            StartProcessingMessages();
        }

        private void ConfigureMachine()
        {
            m_GameMachine = new StateMachine<State, Trigger>(() => m_CurrentState, newState => m_CurrentState = newState);

            m_GameMachine.Configure(State.WaitingForPlayers)
                .OnEntryFrom(Trigger.PlayerNotReady, t => Transition("WAITING FOR PLAYER!"))
                .Permit(Trigger.AllPlayersReady, State.AllPlayersReady)
                .Ignore(Trigger.PlayerNotReady);

            m_GameMachine.Configure(State.AllPlayersReady)
                .OnEntry(t => Transition("ALL PLAYERS ARE READY!"))
                .Permit(Trigger.PlayerNotReady, State.WaitingForPlayers);
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
                IsCreator = false
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
                    p.Callback.Notify(playerChangedInfo.ToXml());
                }
                aggregate.Commands.Add(new PlayerChangedInfo()
                {
                    ClientId = p.ClientId,
                    Name = p.Name,
                    Color = p.PlayerColor,
                    IsReady = p.IsReady.Value
                });
            }
            player.Callback.Notify(aggregate.ToXml());
        }

        public void StartProcessingMessages()
        {
            ConfigureMachine();
            
            // Start spinning the process message loop
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Command command = MessageQueue.Take();
                    ProcessMessage(command);
                }
            }, TaskCreationOptions.LongRunning);
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

            else if (command is PlayerReady)
            {
                LookupPlayerById(command.ClientId).IsReady.Value = true;
            }

            else if (command is PlayerNotReady)
            {
                LookupPlayerById(command.ClientId).IsReady.Value = false;
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

        public void NotifyAllPlayers(string command)
        {
            foreach (Player player in Players)
            {
                player.Callback.Notify(command);
            }
        }
    }
}
