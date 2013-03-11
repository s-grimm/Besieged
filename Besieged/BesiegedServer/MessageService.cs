using System.Collections.Concurrent;

namespace BesiegedServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MessageService" in both code and config file together.
    public class MessageService : IMessageService
    {
        public void SendCommand(Framework.Command.ICommand cmd)
        {
            if (cmd == null) return;
            cmd.Execute();
        }
    }
}