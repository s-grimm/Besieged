using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class DesertTileFactory : ITileFactory
    {
        public Tile.ITile GetBasicGround()
        {
            return new Sand();
        }

        public Tile.ITile GetBasicRoad()
        {
            return new DesertRoad();
        }

        public Tile.ITile GetBasicBridge()
        {
            return new DesertBridge();
        }

        public Tile.ITile GetHardTerrain()
        {
            return new Quicksand();
        }

        public Tile.ITile GetFluidTerrain()
        {
            return new DesertRiver();
        }

        public Tile.ITile GetSpecialTerrain()
        {
            return new Oasis();
        }
    }
}
