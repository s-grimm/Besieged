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

namespace BesiegedServer
{
    public class BesiegedGameInstance
    {
        public ConcurrentBag<ConnectedClient> ConnectedClients { get; set; }
        public BlockingCollection<Command> MessageQueue { get; set; }
        public string GameId { get; set; }
        public string Name { get; set; }
        public int MaxPlayers { get; set; }
        public GameState GameState { get; set; }
        public bool IsGameInstanceFull { get; set; }
        
        private List<Color> _colors = new List<Color>() { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Violet, Color.Cyan, Color.HotPink };

        public BesiegedGameInstance()
        {
            ConnectedClients = new ConcurrentBag<ConnectedClient>();
            MessageQueue = new BlockingCollection<Command>();
            IsGameInstanceFull = false;
            MaxPlayers = 2;

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

        public BesiegedGameInstance(string gameId, string name, int maxPlayers)
        {
            GameId = gameId;
            Name = name;
            MaxPlayers = maxPlayers;
            ConnectedClients = new ConcurrentBag<ConnectedClient>();
            MessageQueue = new BlockingCollection<Command>();
            IsGameInstanceFull = false;

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
                foreach (ConnectedClient client in ConnectedClients)
                {
                    client.ClientCallBack.Notify(commandChatMessage.ToXml());
                }
            }
        }
    }
}
