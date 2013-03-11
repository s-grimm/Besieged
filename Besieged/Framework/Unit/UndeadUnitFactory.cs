using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Unit
{
    public class UndeadUnitFactory : IUnitFactory
    {
        public IUnit GetBasicInfantry()
        {
            return new Skeleton();
        }

        public IUnit GetAdvancedInfantry()
        {
            return new SkeletonCaptain();
        }

        public IUnit GetBasicRanged()
        {
            return new Orc();
        }

        public IUnit GetAdvancedRanged()
        {
            return new OrcChief();
        }

        public IUnit GetBasicMounted()
        {
            return new WolfRider();
        }

        public IUnit GetAdvancedMounted()
        {
            return new WolfLord();
        }

        public IUnit GetBasicSpellCaster()
        {
            return new Vampire();
        }

        public IUnit GetAdvancedSpellCaster()
        {
            return new VampireCount();
        }
    }
}
