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
        private static BlockingCollection<Objects.Client> _clients = new BlockingCollection<Objects.Client>();

        public static Objects.Client AddNewClient(string Alias)
        {
            try
            {
                var newClient = new Objects.Client(Alias);
                newClient.ClientID = _clients.Count > 0 ? (_clients.Max(x => x.ClientID) + 1) : 0;
                _clients.Add(newClient);
                return newClient;
            }
            catch(Exception)
            {
                //log ex
                return null;
            }
        }

        public static bool ClientDisconnect(int clientID)
        {
            return false;
        }

        public static List<Objects.Client> GetClients()
        {
            return _clients.ToList();
        }
    }
}
