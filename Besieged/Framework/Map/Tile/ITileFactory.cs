using Framework.Map.Tile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public interface ITileFactory
    {
        BaseTile GetBasicGround();
        BaseTile GetBasicRoad();
        BaseTile GetBasicBridge();
        BaseTile GetHardTerrain();
        BaseTile GetFluidTerrain();
        BaseTile GetSpecialTerrain();
        
    }
}
