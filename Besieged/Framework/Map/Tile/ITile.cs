using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tiles
{
    public interface ITile
    {
        public int CurrentHealthPointsModifier { get; set; }
        public int MaxHealthPointsModifier { get; set; }
        public int InitiativeModifier { get; set; }
        public int SkillModifier { get; set; }
        public int RangeModifier { get; set; }
        public int MovementModifier { get; set; }
        public bool IsPassable { get; set; }
    }
}
