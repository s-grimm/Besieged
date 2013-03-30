using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class Border : BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }

        public override bool IsPassable { get; set; }
    }
}
