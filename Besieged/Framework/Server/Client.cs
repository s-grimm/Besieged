using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Server
{
    public class Client
    {
        public string Alias { get; set; }
        public int ClientID { get; set; }
        public System.Drawing.Color color { get; set; }

        public Client() { }

        public Client(string Alias)
        {
            this.Alias = Alias;
        }
    }
}
