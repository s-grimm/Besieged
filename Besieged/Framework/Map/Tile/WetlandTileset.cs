using Framework.Map.Tile;
using Framework.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class Dirt :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class WetlandRoad :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class WetlandBridge :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class Mud :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class WetlandRiver :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class Bush :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }
}
