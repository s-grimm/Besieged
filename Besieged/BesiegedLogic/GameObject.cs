using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Drawing;
namespace BesiegedLogic
{
    public static class GameObject
    {
        private static readonly List<Color> _colors = new List<Color>() { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Violet, Color.Cyan, Color.HotPink };
        private static ConcurrentBag<Framework.Server.Client> _clients = new ConcurrentBag<Framework.Server.Client>();

        public static bool AddNewClient(string Alias)
        {
            try
            {
                var newClient = new Framework.Server.Client(Alias);
                newClient.ClientID = (_clients.Max(x=>x.ClientID) + 1);
                _clients.Add(newClient);
                return true;
            }
            catch(Exception ex)
            {
                //log ex
                return false;
            }
        }

        public static bool ClientDisconnect(int clientID)
        {
            return false;
        }

        public static List<Framework.Server.Client> GetClients()
        {
            return _clients.ToList();
        }
    }
}
