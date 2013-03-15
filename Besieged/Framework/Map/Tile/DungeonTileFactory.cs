using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class DungeonTileFactory : ITileFactory
    {

        public Tile.BaseTile GetBasicGround()
        {
            return new StoneFloor();
        }

        public Tile.BaseTile GetBasicRoad()
        {
            return new DungeonRoad();
        }

        public Tile.BaseTile GetBasicBridge()
        {
            return new DungeonBridge();
        }

        public Tile.BaseTile GetHardTerrain()
        {
            return new Spikes();
        }

        public Tile.BaseTile GetFluidTerrain()
        {
            return new DungeonRiver();
        }

        public Tile.BaseTile GetSpecialTerrain()
        {
            return new Door();
        }
    }
}
