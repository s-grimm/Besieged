using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class DesertTileFactory : ITileFactory
    {
        public Tile.BaseTile GetBasicGround()
        {
            return new Sand();
        }

        public Tile.BaseTile GetBasicRoad()
        {
            return new DesertRoad();
        }

        public Tile.BaseTile GetBasicBridge()
        {
            return new DesertBridge();
        }

        public Tile.BaseTile GetHardTerrain()
        {
            return new Quicksand();
        }

        public Tile.BaseTile GetFluidTerrain()
        {
            return new DesertRiver();
        }

        public Tile.BaseTile GetSpecialTerrain()
        {
            return new Oasis();
        }
    }
}
