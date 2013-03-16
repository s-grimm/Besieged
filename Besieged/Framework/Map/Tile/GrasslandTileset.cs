using Framework.Map.Tile;
using Framework.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class Grass : BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class GrasslandRoad :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class GrasslandBridge :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class Tallgrass :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class GrasslandRiver :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class Tree :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }
}
