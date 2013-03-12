namespace Framework.Commands
{
    public class Command
    {
    }

    public class ClientJoined : Command
    {
    }

    public class ConnectionSuccessful : Command
    {
    }

    public class ConnectionTerminated : Command
    {
    }

    public class ChatMessage : Command
    {
        public ChatMessage()
        {
        }

        public string Contents { get; set; }

        public ChatMessage(string contents)
        {
            Contents = contents;
        }
    }
}