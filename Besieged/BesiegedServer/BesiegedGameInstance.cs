using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BesiegedServer
{
    public class BesiegedGameInstance
    {
        public List<ConnectedClient> ConnectedClients { get; set; }
        public string UniqueIdentifier { get; set; }
        public string Name { get; set; }
        public GameState GameState { get; set; }
        public bool IsGameInstanceFull { get; set; }

        public BesiegedGameInstance()
        {
            ConnectedClients = new List<ConnectedClient>();
            IsGameInstanceFull = false;
        }

        public BesiegedGameInstance(string uniqueIdentifier, string name)
        {
            UniqueIdentifier = uniqueIdentifier;
            Name = name;
            ConnectedClients = new List<ConnectedClient>();
            IsGameInstanceFull = false;
        }
    }
}
