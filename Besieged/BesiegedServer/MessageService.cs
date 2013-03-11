using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Collections.Concurrent;
using Framework;
namespace BesiegedServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MessageService" in both code and config file together.
    public class MessageService : IMessageService
    {
        ConcurrentQueue<Framework.Utilities.Message> _messages = new ConcurrentQueue<Framework.Utilities.Message>();
        public void DoWork()
        {
        }
    }
}
