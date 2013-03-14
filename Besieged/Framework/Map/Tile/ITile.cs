using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tile
{
    public interface ITile
    {
        int CurrentHealthPointsModifier { get; set; }
        int MaxHealthPointsModifier { get; set; }
        int InitiativeModifier { get; set; }
        int SkillModifier { get; set; }
        int RangeModifier { get; set; }
        int MovementModifier { get; set; }
        //bool IsPassable { get; set; } //code for client: public bool IsPassable { get; set; }
    }
}
