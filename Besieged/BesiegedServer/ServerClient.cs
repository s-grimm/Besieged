using Framework.BesiegedMessages;
using Framework.ServiceContracts;
using Framework.Utilities.Xml;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BesiegedServer
{
    public class ServerClient : IClient
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
            catch (Exception ex)
            {
                ErrorLogger.Push(ex);
            }
        }
    }
}
