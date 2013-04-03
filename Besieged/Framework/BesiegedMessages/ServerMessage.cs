using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Framework.BesiegedMessages
{
    public class ServerMessage : BesiegedMessage 
    { 
        public enum ServerMessageEnum 
        {
            [XmlEnum(Name = "StartServer")]
            StartServer,
            [XmlEnum(Name = "ServerStarted")]
            ServerStarted
        };
        public ServerMessage() { }
    }

    public class GenericServerMessage : ServerMessage
    {
        public ServerMessage.ServerMessageEnum MessageEnum { get; set; }
    }

    public class ConnectMessage : ServerMessage
    {
        public string Name { get; set; }
    }

    public class CreateGameMessage : ServerMessage
    {
        public string GameName { get; set; }
        public int MaxPlayers { get; set; }
        public bool IsGamePassworded { get; set; }
        public string Password { get; set; }

        public CreateGameMessage()
        {
        }

        public CreateGameMessage(string gameName, int maxPlayers)
        {
            GameName = gameName;
            MaxPlayers = maxPlayers;
            IsGamePassworded = false;
            Password = string.Empty;
        }

        public CreateGameMessage(string gameName, int maxPlayers, string password)
        {
            GameName = gameName;
            MaxPlayers = maxPlayers;
            Password = password;
            IsGamePassworded = true;
        }
    }
}
