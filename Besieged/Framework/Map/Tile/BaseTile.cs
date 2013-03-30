using Framework.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace Framework.Map.Tile
{
    public abstract class BaseTile : DrawableObject
    {
        public virtual int CurrentHealthPointsModifier { get; set; }
        public virtual int MaxHealthPointsModifier { get; set; }
        public virtual int InitiativeModifier { get; set; }
        public virtual int SkillModifier { get; set; }
        public virtual int RangeModifier { get; set; }
        public virtual int MovementModifier { get; set; }
        public virtual bool IsPassable { get; set; } //code for client: public override bool IsPassable { get; set; }
    }
}
