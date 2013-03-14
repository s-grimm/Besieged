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
        public string Alias { get; set; }
        public string UniqueIdentifier { get; set; }
        public IClient ClientCallBack { get; set; }
        public Color PlayerColor { get; set; }

        public ConnectedClient(string alias, string uniqueIdenifier, IClient clientCallBack)
        {
            Alias = alias;
            UniqueIdentifier = uniqueIdenifier;
            ClientCallBack = clientCallBack;
        }
    }
}
