using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class GrasslandTileFactory : ITileFactory
    {
        public Tiles.IPassable GetBasicGround()
        {
            return new Grass();
        }

        public Tiles.IPassable GetBasicRoad()
        {
            return new GrasslandRoad();
        }

        public Tiles.IPassable GetBasicBridge()
        {
            return new GrasslandBridge();
        }

        public Tiles.IPassable GetHardTerrain()
        {
            return new Tallgrass();
        }

        public Tiles.IPassable GetFluidTerrain()
        {
            return new GrasslandRiver();
        }

        public Tiles.IPassable GetSpecialTerrain()
        {
            return new Tree();
        }
    }
}
