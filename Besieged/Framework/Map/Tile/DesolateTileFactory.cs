using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class DesolateTileFactory : ITileFactory
    {
        public Tiles.ITile GetBasicGround()
        {
            return new Ash();
        }

        public Tiles.ITile GetBasicRoad()
        {
            return new DesolateRoad();
        }

        public Tiles.ITile GetBasicBridge()
        {
            return new DesolateBridge();
        }

        public Tiles.ITile GetHardTerrain()
        {
            return new Onyx();
        }

        public Tiles.ITile GetFluidTerrain()
        {
            return new DesolateRiver();
        }

        public Tiles.ITile GetSpecialTerrain()
        {
            return new Lava();
        }
    }
}
