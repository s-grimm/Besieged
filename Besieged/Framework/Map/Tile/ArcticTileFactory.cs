using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class ArcticTileFactory : ITileFactory
    {
        public Tile.ITile GetBasicGround()
        {
            return new Snow();
        }

        public Tile.ITile GetBasicRoad()
        {
            return new ArcticRoad();
        }

        public Tile.ITile GetBasicBridge()
        {
            return new ArcticBridge();
        }

        public Tile.ITile GetHardTerrain()
        {
            return new Ice();
        }

        public Tile.ITile GetFluidTerrain()
        {
            return new ArcticRiver();
        }

        public Tile.ITile GetSpecialTerrain()
        {
            return new DeepSnow();
        }
    }
}
