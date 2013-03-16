using System.Collections.Generic;
namespace Framework.Commands
{
    public class Command
    {
        public string GameId { get; set; }
        public string ClientId { get; set; }
        public string Password { get; set; }
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
        public CommandConnectionSuccessful()
        { 
        }

        public CommandConnectionSuccessful(string clientId)
        {
            ClientId = clientId;
        }
    }

    public class CommandConnectionTerminated : Command
    {
        public CommandConnectionTerminated()
        {
        }

        public CommandConnectionTerminated(string clientId, string gameId)
        {
            ClientId = clientId;
            GameId = gameId;
        }
    }

    public class CommandCreateGame : Command
    {
        public string GameName { get; set; }
        public int MaxPlayers { get; set; }

        public CommandCreateGame()
        {
        }

        public CommandCreateGame(string gameName, int maxPlayers)
        {
            GameName = gameName;
            MaxPlayers = maxPlayers;
        }

        public CommandCreateGame(string gameName, int maxPlayers, string password)
        {
            GameName = gameName;
            MaxPlayers = maxPlayers;
            Password = password;
        }
    }

    public class CommandJoinGame : Command
    {
        public CommandJoinGame()
        {
        }

        public CommandJoinGame(string gameId)
        {
            GameId = gameId;
        }

        public CommandJoinGame(string gameId, string password)
        {
            GameId = gameId;
            Password = password;
        }
    }

    public class CommandJoinGameSuccessful : Command
    {
        public CommandJoinGameSuccessful()
        {
        }

        public CommandJoinGameSuccessful(string gameId)
        {
            GameId = gameId;
        }
    }

    public class CommandNotifyGame : Command
    {
        public string Name { get; set; }
        public string Capacity { get; set; }
        public bool IsGameFull { get; set; }

        public CommandNotifyGame()
        {
            IsGameFull = false;
        }

        public CommandNotifyGame(string gameId, string name,  string capacity, bool isGameFull)
        {
            GameId = gameId;
            Name = name;
            Capacity = capacity;
            IsGameFull = isGameFull;
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

    public class CommandSendGameMap : Command
    {
        public string SerializedMap { get; set; }

        public CommandSendGameMap()
        {
            
        }
        public CommandSendGameMap(string serializedMap)
        {
            SerializedMap = serializedMap;
        }
    }
	
	public class CommandStartServer : Command
    {
    }
	
	
}