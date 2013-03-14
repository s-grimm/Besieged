using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class DesertTileFactory : ITileFactory
    {
        public Tiles.ITile GetBasicGround()
        {
            return new Sand();
        }

        public Tiles.ITile GetBasicRoad()
        {
            return new DesertRoad();
        }

        public Tiles.ITile GetBasicBridge()
        {
            return new DesertBridge();
        }

        public Tiles.ITile GetHardTerrain()
        {
            return new Quicksand();
        }

        public Tiles.ITile GetFluidTerrain()
        {
            return new DesertRiver();
        }

        public Tiles.ITile GetSpecialTerrain()
        {
            return new Oasis();
        }
    }
}
