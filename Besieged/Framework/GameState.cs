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

            int x_movement, y_movement, x_start, y_start, x_pivot, y_pivot;
            switch (corner)
            {
                case 1:
                    x_movement = 1;
                    x_start = 5;
                    x_pivot = 9;

                    y_movement = 1;
                    y_start = 9;
                    y_pivot = 7;
                    break;

                case 2:
                    x_movement = -1;
                    x_start = 50;
                    x_pivot = 54;

                    y_movement = -1;
                    y_start = 46;
                    y_pivot = 44;
                    break;

                case 3:
                    x_movement = -1;
                    x_start = 50;
                    x_pivot = 54;

                    y_movement = 1;
                    y_start = 9;
                    y_pivot = 7;
                    break;

                case 4:
                    x_movement = 1;
                    x_start = 5;
                    x_pivot = 9;

                    y_movement = -1;
                    y_start = 46;
                    y_pivot = 44;
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
            int y = y_start;
            for (int i = 0; i < 2; i++)
            {
                int x = x_start;
                //basic infantry
                for (int j = 0; j < 4; j++)
                {
                    IUnit tUnit = factory.GetBasicInfantry();
                    tUnit.Owner = playerID;
                    tUnit.X_Position = x;
                    tUnit.Y_Position = y;
                    tUnit.MovementLeft = tUnit.Movement;
                    Units.Add((BaseUnit)tUnit);

                    x += x_movement;
                }
                //basic ranged
                for (int j = 0; j < 2; j++)
                {
                    IUnit tUnit = factory.GetBasicRanged();
                    tUnit.Owner = playerID;
                    tUnit.X_Position = x;
                    tUnit.Y_Position = y;
                    tUnit.MovementLeft = tUnit.Movement;
                    Units.Add((BaseUnit)tUnit);

                    x += x_movement;
                }
                y += y_movement;
            }
                      
            for (int i = 0; i < 2; i++)
            {
                
                //basic ranged
                for (int j = 0; j < 2; j++)
                {
                    IUnit tUnit = factory.GetBasicRanged();
                    tUnit.Owner = playerID;
                    tUnit.X_Position = x;
                    tUnit.Y_Position = y;
                    tUnit.MovementLeft = tUnit.Movement;
                    Units.Add((BaseUnit)tUnit);

                    x += x_movement;
                }
                //basic armoured
                for (int j = 0; j < 2; j++)
                {
                    IUnit tUnit = factory.GetBasicMounted();
                    tUnit.Owner = playerID;
                    tUnit.X_Position = x;
                    tUnit.Y_Position = y;
                    tUnit.MovementLeft = tUnit.Movement;
                    Units.Add((BaseUnit)tUnit);

                    x += x_movement;
                }
                y += y_movement;
            }
            

        }
    }
}