using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class DesolateTileFactory : ITileFactory
    {
        public Tiles.IPassable GetBasicGround()
        {
            return new Ash();
        }

        public Tiles.IPassable GetBasicRoad()
        {
            return new DesolateRoad();
        }

        public Tiles.IPassable GetBasicBridge()
        {
            return new DesolateBridge();
        }

        public Tiles.IPassable GetHardTerrain()
        {
            return new Onyx();
        }

        public Tiles.IPassable GetFluidTerrain()
        {
            return new DesolateRiver();
        }

        public Tiles.IPassable GetSpecialTerrain()
        {
            return new Lava();
        }
    }
}
