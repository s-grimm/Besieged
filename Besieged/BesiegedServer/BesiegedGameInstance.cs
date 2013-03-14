using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Concurrent;

namespace BesiegedServer
{
    public class BesiegedGameInstance
    {
        public ConcurrentBag<ConnectedClient> ConnectedClients { get; set; }
        public string UniqueIdentifier { get; set; }
        public string Name { get; set; }
        public GameState GameState { get; set; }
        public bool IsGameInstanceFull { get; set; }
        private List<Color> _colors = new List<Color>() { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Violet, Color.Cyan, Color.HotPink };

        public BesiegedGameInstance()
        {
            ConnectedClients = new ConcurrentBag<ConnectedClient>();
            IsGameInstanceFull = false;
        }

        public BesiegedGameInstance(string uniqueIdentifier, string name)
        {
            UniqueIdentifier = uniqueIdentifier;
            Name = name;
            ConnectedClients = new ConcurrentBag<ConnectedClient>();
            IsGameInstanceFull = false;
        }
    }
}
