using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;
using System.Collections.Concurrent;
using Framework.Commands;
using Framework.Utilities.Xml;
using Framework.ServiceContracts;
using Framework.BesiegedMessages;

namespace BesiegedClient
{
    public class Client : IClient
    {
        public BlockingCollection<BesiegedMessage> MessageQueue = new BlockingCollection<BesiegedMessage>();

        public void SendMessage(string serializedMessage)
        {
            try
            {
                BesiegedMessage message = serializedMessage.FromXml<BesiegedMessage>();
                if (message is AggregateMessage)
                {
                    (message as AggregateMessage).MessageList.ForEach(x => MessageQueue.Add(x));
                }
                else
                {
                    MessageQueue.Add(message);
                }
            }
            catch (Exception)
            {
                // custom error handling	
            }
        }
    }
}
