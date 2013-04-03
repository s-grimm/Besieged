using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.BesiegedMessages
{
    public class BesiegedMessage
    {
        public string ClientId { get; set; }
        public string GameId { get; set; }
    }

    public class AggregateMessage : BesiegedMessage
    {
        public List<BesiegedMessage> MessageList = new List<BesiegedMessage>();
    }
}
