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
namespace TestClient
{
    public class Client : IClient
    {
        public BlockingCollection<Command> MessageQueue = new BlockingCollection<Command>();

        public void SendMessage(string serializedMessage)
        {
            try
            {
                Command command = serializedMessage.FromXml<Command>();
                if (command is CommandAggregate)
                {
                    CommandAggregate commandAggregate = command as CommandAggregate;
                    commandAggregate.Commands.ForEach(x => MessageQueue.Add(x));
                }
                else
                {
                    MessageQueue.Add(command);
                }
            }
            catch (Exception)
            {
                // custom error handling	
            }
        }
    }
}
