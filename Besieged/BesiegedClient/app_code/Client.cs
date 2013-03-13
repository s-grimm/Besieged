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
namespace BesiegedClient.app_code
{
    public class Client : IClient
    {
        public BlockingCollection<Command> MessageQueue = new BlockingCollection<Command>();

        public void Notify(string serializedMessage)
        {
            try
            {
                Command command = serializedMessage.FromXml<Command>();
                MessageQueue.Add(command);
            }
            catch (Exception ex)
            {
                // custom error handling	
            }
        }
    }
}
