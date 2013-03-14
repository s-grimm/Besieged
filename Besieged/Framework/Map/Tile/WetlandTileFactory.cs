using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class WetlandTileFactory : ITileFactory
    {
        public Tile.ITile GetBasicGround()
        {
            return new Dirt();
        }

        public Tile.ITile GetBasicRoad()
        {
            return new WetlandRoad();
        }

        public Tile.ITile GetBasicBridge()
        {
            return new WetlandBridge();
        }

        public Tile.ITile GetHardTerrain()
        {
            return new Mud();
        }

        public Tile.ITile GetFluidTerrain()
        {
            return new WetlandRiver();
        }

        public Tile.ITile GetSpecialTerrain()
        {
            return new Bush();
        }
    }
}
