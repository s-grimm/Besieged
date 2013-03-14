using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class ArcticTileFactory : ITileFactory
    {
        public Tiles.ITile GetBasicGround()
        {
            return new Snow();
        }

        public Tiles.ITile GetBasicRoad()
        {
            return new ArcticRoad();
        }

        public Tiles.ITile GetBasicBridge()
        {
            return new ArcticBridge();
        }

        public Tiles.ITile GetHardTerrain()
        {
            return new Ice();
        }

        public Tiles.ITile GetFluidTerrain()
        {
            return new ArcticRiver();
        }

        public Tiles.ITile GetSpecialTerrain()
        {
            return new DeepSnow();
        }
    }
}
