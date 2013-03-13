namespace Framework.Commands
{
    public class Command
    {
    }

    public class CommandConnect : Command
    {
        public string Alias { get; set; }
        
        public CommandConnect()
        {
        }

        public CommandConnect(string alias)
        {
            Alias = alias;
        }
    }

    public class CommandClientJoined : Command
    {
    }

    public class CommandConnectionSuccessful : Command
    {
        public string UniqueIdentifier { get; set; }

        public CommandConnectionSuccessful()
        { 
        }

        public CommandConnectionSuccessful(string uniqueIdentifier)
        {
            UniqueIdentifier = uniqueIdentifier;
        }
    }

    public class CommandConnectionTerminated : Command
    {
    }

    public class CommandChatMessage : Command
    {
        public string Contents { get; set; }

        public CommandChatMessage()
        {
        }

        public CommandChatMessage(string contents)
        {
            Contents = contents;
        }
    }
}