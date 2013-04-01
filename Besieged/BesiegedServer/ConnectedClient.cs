using Framework.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BesiegedServer
{
    public class ConnectedClient
    {
        public string Name { get; set; }
        public string ClientId { get; set; }
        public IClient Callback { get; set; }

        public ConnectedClient(string name, string clientId, IClient callback)
        {
            Name = name;
            ClientId = clientId;
            Callback = callback;
        }
    }
}
