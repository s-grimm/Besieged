using Framework.Commands;
using Framework.ServiceContracts;
using Framework.Utilities.Xml;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BesiegedServer
{
    public class ServerClient : IClient
    {
        public BlockingCollection<Command> MessageQueue = new BlockingCollection<Command>();

        public void Notify(string serializedMessage)
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
            catch (Exception ex)
            {
                // custom error handling	
            }
        }
    }
}
