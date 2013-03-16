using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class GrasslandTileFactory : ITileFactory
    {
        public Tile.BaseTile GetBasicGround()
        {
            return new Grass();
        }

        public Tile.BaseTile GetBasicRoad()
        {
            return new GrasslandRoad();
        }

        public Tile.BaseTile GetBasicBridge()
        {
            return new GrasslandBridge();
        }

        public Tile.BaseTile GetHardTerrain()
        {
            return new Tallgrass();
        }

        public Tile.BaseTile GetFluidTerrain()
        {
            return new GrasslandRiver();
        }

        public Tile.BaseTile GetSpecialTerrain()
        {
            return new Tree();
        }
    }
}
