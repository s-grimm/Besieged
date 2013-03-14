using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class ArcticTileFactory : ITileFactory
    {
        public Tiles.IPassable GetBasicGround()
        {
            return new Snow();
        }

        public Tiles.IPassable GetBasicRoad()
        {
            return new ArcticRoad();
        }

        public Tiles.IPassable GetBasicBridge()
        {
            return new ArcticBridge();
        }

        public Tiles.IPassable GetHardTerrain()
        {
            return new Ice();
        }

        public Tiles.IPassable GetFluidTerrain()
        {
            return new ArcticRiver();
        }

        public Tiles.IPassable GetSpecialTerrain()
        {
            return new DeepSnow();
        }
    }
}
