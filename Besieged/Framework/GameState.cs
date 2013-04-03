using Framework.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Utilities.Xml;
using Framework.Map;
using System.Collections.Concurrent;
using Framework.Utilities;
namespace Framework
{
    public class GameState
    {
        public List<IUnit> Units { get; set; }
        public GameMap GameBoard { get; set; }
        List<GamePlayer> GamePlayers { get; set; }
        private GameState()
        {
            Units = new List<IUnit>();
            GameBoard = new GameMap();
            GamePlayers = new List<GamePlayer>();
        }

        public GameState(List<string> playerIDs)
        {
            Units = new List<IUnit>();
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

                if (raceNum == 0)
                {
                    factory = new AllianceUnitFactory();
                }
                else if (raceNum == 1)
                {
                    factory = new BeastUnitFactory();
                }
                else
                {
                    factory = new UndeadUnitFactory();
                }
                tplayer.Factory = factory;
                GamePlayers.Add(tplayer);
                SetupInitialUnits(player,values[i++]);

            }

        }

        public void SetupInitialUnits(string playerID, int corner)
        {
            //corners (for default map ONLY)
            /****************************
             *  1) Upper Left   ( X 4    ,  Y 4  ) 
             *  2) Lower Right  ( X 53   ,  Y 53 )
             *  3) Upper Right  ( X 53   ,  Y 4  )
             *  4) Lower Left   ( X 4    ,  Y 53 )
             ****************************/
        }
    }
}
