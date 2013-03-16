using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Sprite
{
    public class DrawableObject
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
                throw ex;
            }
            return sprite;
        }


    }
}
