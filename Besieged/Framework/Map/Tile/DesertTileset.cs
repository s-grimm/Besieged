using Framework.Map.Tile;
using Framework.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class Sand :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class DesertRoad :  BaseTile
    {

        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class DesertBridge :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class Quicksand :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class DesertRiver :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class Oasis :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }
}
