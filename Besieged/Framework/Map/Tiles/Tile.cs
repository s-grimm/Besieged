using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tiles
{
    class Tile
    {
        public virtual object GetSprite()
        {
            object sprite = null;
            try
            {
                //sprite = SpriteFactory.GetSprite(this);
            }
            catch (Exception ex)
            {

            }
            return sprite;
        }
    }
}
