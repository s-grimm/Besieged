using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class WetlandTileFactory : ITileFactory
    {
        public Tile.BaseTile GetBasicGround()
        {
            return new Dirt();
        }

        public Tile.BaseTile GetBasicRoad()
        {
            return new WetlandRoad();
        }

        public Tile.BaseTile GetBasicBridge()
        {
            return new WetlandBridge();
        }

        public Tile.BaseTile GetHardTerrain()
        {
            return new Mud();
        }

        public Tile.BaseTile GetFluidTerrain()
        {
            return new WetlandRiver();
        }

        public Tile.BaseTile GetSpecialTerrain()
        {
            return new Bush();
        }
    }
}
