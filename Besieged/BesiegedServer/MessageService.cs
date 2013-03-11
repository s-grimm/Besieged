using System.Collections.Concurrent;

namespace BesiegedServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MessageService" in both code and config file together.
    public class MessageService : IMessageService
    {
        private ConcurrentQueue<Framework.Command.ICommand> _messages = new ConcurrentQueue<Framework.Command.ICommand>();

        public void Process(Framework.Command.ICommand cmd)
        {
            if (cmd == null) return;
            //check for new messages
            //process messages
        }
    }
}