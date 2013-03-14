using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class DungeonTileFactory : ITileFactory
    {

        public Tiles.IPassable GetBasicGround()
        {
            return new StoneFloor();
        }

        public Tiles.IPassable GetBasicRoad()
        {
            return new DungeonRoad();
        }

        public Tiles.IPassable GetBasicBridge()
        {
            return new DungeonBridge();
        }

        public Tiles.IPassable GetHardTerrain()
        {
            return new Spikes();
        }

        public Tiles.IPassable GetFluidTerrain()
        {
            return new DungeonRiver();
        }

        public Tiles.IPassable GetSpecialTerrain()
        {
            return new Door();
        }
    }
}
