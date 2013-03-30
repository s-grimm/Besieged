using Framework;
using Framework.Commands;
using System;
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

        public void AddPlayer(ConnectedClient client)
        {
            Player player = new Player(client.Name, client.ClientId, client.Callback, ColorPool.Pop());
            Players.Add(player);
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
                    Color = p.PlayerColor
                });
            }
            player.Callback.Notify(aggregate.ToXml());
        }

        public void StartProcessingMessages()
        {
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
                foreach (Player player in Players)
                {
                    player.Callback.Notify(commandChatMessage.ToXml());
                }
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
    }
}
