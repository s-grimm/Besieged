using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Framework.BesiegedMessages
{
    public class GameMessage : BesiegedMessage
    {
        public enum GameMessageEnum
        {
            [XmlEnum(Name = "PlayerReady")]
            PlayerReady,
            [XmlEnum(Name = "PlayerNotReady")]
            PlayerNotReady,
            [XmlEnum(Name = "Start")]
            Start,
            [XmlEnum(Name = "PlayerLeft")]
            PlayerLeft,
        };
    }

    public class GenericGameMessage : GameMessage
    {
        public GameMessage.GameMessageEnum MessageEnum { get; set; }
    }

    public class JoinGameMessage : GameMessage
    {
        public string Password { get; set; }
    }

    public class GameChatMessage : GameMessage
    {
        public string Contents { get; set; }
    }
}
