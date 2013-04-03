using Framework.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Unit
{
    public abstract class BaseUnit : DrawableObject, IUnit
    {
        public abstract int X_Position { get; set; }

        public abstract int Y_Position { get; set; }

        public abstract int CurrentHealthPoints { get; set; }

        public abstract int MaxHealthPoints { get; }

        public abstract int Initiative { get; }

        public abstract int Skill { get; }

        public abstract int Range { get; }

        public abstract int Cost { get; }

        public abstract int Movement { get; }

        public abstract string Description { get; }

        public abstract string Owner { get; set; }

        public new object GetSprite()
        {
            return SpriteFactory.GetSprite(this);
        }
    }
}
