﻿namespace Framework
{
    public class GamePlayer
    {
        public Unit.IUnitFactory Factory { get; set; }

        public string PlayerID { get; private set; }

        public PlayerColor.PlayerColorEnum PlayerColor { get; set; }

        public GamePlayer(string playerID)
        {
            PlayerID = playerID;
        }
    }
}