using Framework.Map;
using Framework.Sprite;
using Framework.Unit;
using Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework
{
    public class GameState
    {
        public List<BaseUnit> Units { get; set; }

        public GameMap GameBoard { get; set; }

        private List<GamePlayer> GamePlayers { get; set; }

        private GameState()
        {
            Units = new List<BaseUnit>();
            GameBoard = new GameMap();
            GamePlayers = new List<GamePlayer>();
        }

        public GameState(IReadOnlyCollection<string> playerIDs)
        {
            Units = new List<BaseUnit>();
            GameBoard = new GameMap();
            GamePlayers = new List<GamePlayer>();

            Random random = new Random();
            int[] values = Enumerable.Range(1, playerIDs.Count).ToArray();
            values.ArrayShuffle();
            int i = 0;
            foreach (string player in playerIDs)
            {
                GamePlayer tplayer = new GamePlayer(player);

                int raceNum = random.Next(0, 3);

                IUnitFactory factory;

                switch (raceNum)
                {
                    //case 0:
                    //    factory = new AllianceUnitFactory();
                    //    break;

                    //case 1:
                    //    factory = new BeastUnitFactory();
                    //    break;

                    default:
                        factory = new UndeadUnitFactory();
                        break;
                }
                tplayer.Factory = factory;
                GamePlayers.Add(tplayer);
                SetupInitialUnits(player, values[i++]);
            }
        }

        public void SetupInitialUnits(string playerID, int corner)
        {
            //corners (for default map ONLY) 0-55
            /****************************
             *  1) Upper Left   ( X 4    ,  Y 4  )
             *  2) Lower Right  ( X 52   ,  Y 52 )
             *  3) Upper Right  ( X 52   ,  Y 4  )
             *  4) Lower Left   ( X 4    ,  Y 52 )
             * Accounting for a 4x4 castle in the
             * corner, you need to start placing
             * units at X+-4 and Y+-4
             * ****************************/

            int x_movement, y_movement, x_start, y_start;
            switch (corner)
            {
                case 1:
                    x_movement = 1;
                    x_start = 5;

                    y_movement = 1;
                    y_start = 5;
                    break;

                case 2:
                    x_movement = -1;
                    x_start = 51;

                    y_movement = -1;
                    y_start = 51;
                    break;

                case 3:
                    x_movement = -1;
                    x_start = 51;

                    y_movement = 1;
                    y_start = 6;
                    break;

                case 4:
                    x_movement = 1;
                    x_start = 6;

                    y_movement = -1;
                    y_start = 51;
                    break;

                default:
                    //should never hit this line
                    x_movement = 1;
                    x_start = 6;

                    y_movement = 1;
                    y_start = 6;
                    break;
            }

            GamePlayer player = GamePlayers.First(p => p.PlayerID == playerID);
            IUnitFactory factory = player.Factory;
            int x = x_start;
            for (int i = 0; i < 4; i ++)
            {
                int y = y_start;
                for (int j = 0; j < 4; j ++)
                {
                    IUnit tUnit = factory.GetBasicInfantry();
                    tUnit.Owner = playerID;
                    tUnit.X_Position = x;
                    tUnit.Y_Position = y;

                    Units.Add((BaseUnit)tUnit);

                    y += y_movement;
                }
                x += x_movement;
            }
        }
    }
}