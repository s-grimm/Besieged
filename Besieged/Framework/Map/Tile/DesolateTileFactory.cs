using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class DesolateTileFactory : ITileFactory
    {
        public Tile.ITile GetBasicGround()
        {
            return new Ash();
        }

        public Tile.ITile GetBasicRoad()
        {
            return new DesolateRoad();
        }

        public Tile.ITile GetBasicBridge()
        {
            return new DesolateBridge();
        }

        public Tile.ITile GetHardTerrain()
        {
            return new Onyx();
        }

        public Tile.ITile GetFluidTerrain()
        {
            return new DesolateRiver();
        }

        public Tile.ITile GetSpecialTerrain()
        {
            return new Lava();
        }
    }
}
