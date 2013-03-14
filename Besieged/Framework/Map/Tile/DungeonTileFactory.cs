using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class DungeonTileFactory : ITileFactory
    {

        public Tile.ITile GetBasicGround()
        {
            return new StoneFloor();
        }

        public Tile.ITile GetBasicRoad()
        {
            return new DungeonRoad();
        }

        public Tile.ITile GetBasicBridge()
        {
            return new DungeonBridge();
        }

        public Tile.ITile GetHardTerrain()
        {
            return new Spikes();
        }

        public Tile.ITile GetFluidTerrain()
        {
            return new DungeonRiver();
        }

        public Tile.ITile GetSpecialTerrain()
        {
            return new Door();
        }
    }
}
