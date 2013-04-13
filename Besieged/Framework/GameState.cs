using Framework.Map;
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

        public GameState(List<KeyValuePair<string, Army.ArmyTypeEnum>> playerInfos)
        {
            Units = new List<BaseUnit>();
            GameBoard = new GameMap();
            GamePlayers = new List<GamePlayer>();

            Random random = new Random();
            int[] values = Enumerable.Range(1, playerInfos.Count).ToArray();
            values.ArrayShuffle();
            int i = 0;
            foreach (var info in playerInfos)
            {
                GamePlayer player = new GamePlayer(info.Key, info.Value);
                GamePlayers.Add(player);
                SetupInitialUnits(player.PlayerID, values[i++]);
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
                    x_start = 3;

                    y_movement = 1;
                    y_start = 3;
                    break;

                case 2:
                    x_movement = -1;
                    x_start = 53;

                    y_movement = -1;
                    y_start = 53;
                    break;

                case 3:
                    x_movement = -1;
                    x_start = 53;

                    y_movement = 1;
                    y_start = 3;
                    break;

                case 4:
                    x_movement = 1;
                    x_start = 3;

                    y_movement = -1;
                    y_start = 53;
                    break;

                default:

                    //should never hit this line
                    x_movement = 1;
                    x_start = 3;

                    y_movement = 1;
                    y_start = 3;
                    break;
            }

            GamePlayer player = GamePlayers.First(p => p.PlayerID == playerID);
            IUnitFactory factory = player.Factory;
            int x = x_start;
            for (int i = 0; i < 4; i++)
            {
                int y = y_start;
                for (int j = 0; j < 4; j++)
                {
                    IUnit tUnit = factory.GetAdvancedInfantry();
                    tUnit.Owner = playerID;
                    tUnit.X_Position = x;
                    tUnit.Y_Position = y;
                    tUnit.MovementLeft = tUnit.Movement;
                    Units.Add((BaseUnit)tUnit);

                    y += y_movement;
                }
                x += x_movement;
            }
        }
    }
}