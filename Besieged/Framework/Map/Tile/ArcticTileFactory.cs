using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class ArcticTileFactory : ITileFactory
    {
        public Tile.BaseTile GetBasicGround()
        {
            return new Snow();
        }

        public Tile.BaseTile GetBasicRoad()
        {
            return new ArcticRoad();
        }

        public Tile.BaseTile GetBasicBridge()
        {
            return new ArcticBridge();
        }

        public Tile.BaseTile GetHardTerrain()
        {
            return new Ice();
        }

        public Tile.BaseTile GetFluidTerrain()
        {
            return new ArcticRiver();
        }

        public Tile.BaseTile GetSpecialTerrain()
        {
            return new DeepSnow();
        }
    }
}
