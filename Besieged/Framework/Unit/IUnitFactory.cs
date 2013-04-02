using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Unit
{
    public interface IUnitFactory
    {
        IUnit GetBasicInfantry();
        IUnit GetAdvancedInfantry();
        IUnit GetBasicRanged();
        IUnit GetAdvancedRanged();
        IUnit GetBasicMounted();
        IUnit GetAdvancedMounted();
        IUnit GetBasicSpellCaster();
        IUnit GetAdvancedSpellCaster();
        IUnit GetCastle();
        // something for the hero
    }
}
