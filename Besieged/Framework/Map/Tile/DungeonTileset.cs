using Framework.Map.Tile;
using Framework.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public class StoneFloor :  BaseTile
    {

        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class DungeonRoad :  BaseTile
    {

        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class DungeonBridge :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class Spikes :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class DungeonRiver :  BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }

    public class Door : BaseTile
    {
        public override int CurrentHealthPointsModifier { get; set; }

        public override int MaxHealthPointsModifier { get; set; }

        public override int InitiativeModifier { get; set; }

        public override int SkillModifier { get; set; }

        public override int RangeModifier { get; set; }

        public override int MovementModifier { get; set; }
    }
}