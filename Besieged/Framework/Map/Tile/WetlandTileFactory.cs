using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class WetlandTileFactory : ITileFactory
    {
        public Tiles.IPassable GetBasicGround()
        {
            return new Dirt();
        }

        public Tiles.IPassable GetBasicRoad()
        {
            return new WetlandRoad();
        }

        public Tiles.IPassable GetBasicBridge()
        {
            return new WetlandBridge();
        }

        public Tiles.IPassable GetHardTerrain()
        {
            return new Mud();
        }

        public Tiles.IPassable GetFluidTerrain()
        {
            return new WetlandRiver();
        }

        public Tiles.IPassable GetSpecialTerrain()
        {
            return new Bush();
        }
    }
}
