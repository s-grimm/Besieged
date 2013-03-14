using Framework.Map.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public interface ITileFactory
    {
        IPassable GetBasicGround();
        IPassable GetBasicRoad();
        IPassable GetBasicBridge();
        IPassable GetHardTerrain();
        IPassable GetFluidTerrain();
        IPassable GetSpecialTerrain();
        
    }
}
