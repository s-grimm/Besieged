using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Framework.BesiegedMessages
{
    public class ClientMessage : BesiegedMessage
    {
        public enum ClientMessageEnum
        {
            [XmlEnum(Name = "ConnectSuccessful")]
            ConnectSuccessful,
            [XmlEnum(Name = "AllPlayersReady")]
            AllPlayersReady,
            [XmlEnum(Name = "PlayerNotReady")]
            PlayerNotReady,
            [XmlEnum(Name = "StartGame")]
            StartGame,
            [XmlEnum(Name = "GameDisbanded")]
            GameDisbanded,
            [XmlEnum(Name = "GameNotFound")]
            GameNotFound,
        };
    }

    public class GenericClientMessage : ClientMessage
    {
        public ClientMessage.ClientMessageEnum MessageEnum { get; set; }
    }

    public class ClientChatMessage : ClientMessage
    {
        public string Contents { get; set; }
    }

    public class PlayerInfoMessage : ClientMessage
    {
        public PlayerColor.PlayerColorEnum Color { get; set; }
        public bool IsReady { get; set; }
        public string Name { get; set; }
        public bool IsCreator { get; set; }
    }

    public class PlayerGameInfoMessage : ClientMessage
    {
        public bool IsCreator { get; set; }
    }

    public class ErrorDialogMessage : ClientMessage
    {
        public string Contents { get; set; }
    }

    public class GameInfoMessage : ClientMessage
    {
        public string Name { get; set; }
        public string Capacity { get; set; }
        public bool IsGameFull { get; set; }
        public bool HasPassword { get; set; }
        public GameInfoMessage()
        {
            IsGameFull = false;
            HasPassword = false;
        }

        public GameInfoMessage(string gameId, string name,  string capacity, bool isGameFull)
        {
            GameId = gameId;
            Name = name;
            Capacity = capacity;
            IsGameFull = isGameFull;
        }
        public GameInfoMessage(string gameId, string name, string capacity, bool isGameFull, bool hasPassword)
        {
            GameId = gameId;
            Name = name;
            Capacity = capacity;
            IsGameFull = isGameFull;
            HasPassword = hasPassword;
        }
    }
}
