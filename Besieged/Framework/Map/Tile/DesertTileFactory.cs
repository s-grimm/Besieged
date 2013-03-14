using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class DesertTileFactory : ITileFactory
    {
        public Tiles.IPassable GetBasicGround()
        {
            return new Sand();
        }

        public Tiles.IPassable GetBasicRoad()
        {
            return new DesertRoad();
        }

        public Tiles.IPassable GetBasicBridge()
        {
            return new DesertBridge();
        }

        public Tiles.IPassable GetHardTerrain()
        {
            return new Quicksand();
        }

        public Tiles.IPassable GetFluidTerrain()
        {
            return new DesertRiver();
        }

        public Tiles.IPassable GetSpecialTerrain()
        {
            return new Oasis();
        }
    }
}
