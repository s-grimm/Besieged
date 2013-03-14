using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class GrasslandTileFactory : ITileFactory
    {
        public Tile.ITile GetBasicGround()
        {
            return new Grass();
        }

        public Tile.ITile GetBasicRoad()
        {
            return new GrasslandRoad();
        }

        public Tile.ITile GetBasicBridge()
        {
            return new GrasslandBridge();
        }

        public Tile.ITile GetHardTerrain()
        {
            return new Tallgrass();
        }

        public Tile.ITile GetFluidTerrain()
        {
            return new GrasslandRiver();
        }

        public Tile.ITile GetSpecialTerrain()
        {
            return new Tree();
        }
    }
}
