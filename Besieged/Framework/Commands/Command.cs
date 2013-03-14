using System.Collections.Generic;
namespace Framework.Commands
{
    public class Command
    {
    }

    public class CommandAggregate : Command
    {
        public List<Command> Commands { get; set; }

        public CommandAggregate()
        {
            Commands = new List<Command>();
        }
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

    public class CommandClientJoined : Command
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

    public class CommandNotifyGame : Command
    {
        public string UniqueIdentifier { get; set; }
        public string Name { get; set; }
        public string Capacity { get; set; }
        public bool IsGameFull { get; set; }

        public CommandNotifyGame()
        {
            IsGameFull = false;
        }

        public CommandNotifyGame(string uniqueIdentifier, string name,  string capacity, bool isGameFull)
        {
            UniqueIdentifier = uniqueIdentifier;
            Name = name;
            Capacity = capacity;
            IsGameFull = isGameFull;
        }
    }

    public class CommandJoinGame : Command
    {
        public string ClientIdentifier { get; set; }
        public string GameIdentifier { get; set; }

        public CommandJoinGame()
        {
        }

        public CommandJoinGame(string clientIdentifier, string gameIdentifier)
        {
            ClientIdentifier = clientIdentifier;
            GameIdentifier = gameIdentifier;
        }
    }

    public class CommandServerError : Command
    {
        public string ErrorMessage { get; set; }

        public CommandServerError()
        {
        }

        public CommandServerError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}