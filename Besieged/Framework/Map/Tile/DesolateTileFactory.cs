using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class DesolateTileFactory : ITileFactory
    {
        public Tile.BaseTile GetBasicGround()
        {
            return new Ash();
        }

        public Tile.BaseTile GetBasicRoad()
        {
            return new DesolateRoad();
        }

        public Tile.BaseTile GetBasicBridge()
        {
            return new DesolateBridge();
        }

        public Tile.BaseTile GetHardTerrain()
        {
            return new Onyx();
        }

        public Tile.BaseTile GetFluidTerrain()
        {
            return new DesolateRiver();
        }

        public Tile.BaseTile GetSpecialTerrain()
        {
            return new Lava();
        }
    }
}
