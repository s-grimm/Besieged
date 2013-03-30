using System.Collections.Generic;
namespace Framework.Commands
{
    public class Command
    {
        public string GameId { get; set; }
        public string ClientId { get; set; }
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
        public string Name { get; set; }
        
        public CommandConnect()
        {
        }

        public CommandConnect(string name)
        {
            Name = name;
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
        public bool IsGamePassworded { get; set; }
        public string Password { get; set; }

        public CommandCreateGame()
        {
        }

        public CommandCreateGame(string gameName, int maxPlayers)
        {
            GameName = gameName;
            MaxPlayers = maxPlayers;
            IsGamePassworded = false;
            Password = string.Empty;
        }

        public CommandCreateGame(string gameName, int maxPlayers, string password)
        {
            GameName = gameName;
            MaxPlayers = maxPlayers;
            Password = password;
            IsGamePassworded = true;
        }
    }

    public class CommandJoinGame : Command
    {
        public string Password { get; set; }
        
        public CommandJoinGame()
        {
        }

        public CommandJoinGame(string gameId)
        {
            GameId = gameId;
            Password = string.Empty;
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
        public bool HasPassword { get; set; }
        public CommandNotifyGame()
        {
            IsGameFull = false;
            HasPassword = false;
        }

        public CommandNotifyGame(string gameId, string name,  string capacity, bool isGameFull)
        {
            GameId = gameId;
            Name = name;
            Capacity = capacity;
            IsGameFull = isGameFull;
        }
        public CommandNotifyGame(string gameId, string name, string capacity, bool isGameFull, bool hasPassword)
        {
            GameId = gameId;
            Name = name;
            Capacity = capacity;
            IsGameFull = isGameFull;
            HasPassword = hasPassword;
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

    public class CommandServerStarted : Command
    {
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

    public class PlayerGameInfo : Command
    {
        public PlayerColor.PlayerColorEnum Color { get; set; }
        public bool IsReady { get; set; }
        public bool IsCreator { get; set; }

        public PlayerGameInfo() { }
    }

    public class PlayerChangedInfo : Command
    {
        public PlayerColor.PlayerColorEnum Color { get; set; }
        public bool IsReady { get; set; }
        public string Name { get; set; }

        public PlayerChangedInfo() { }
    }
}