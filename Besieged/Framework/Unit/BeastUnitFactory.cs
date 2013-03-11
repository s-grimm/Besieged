using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Unit
{
    public class BeastUnitFactory : IUnitFactory
    {
        public IUnit GetBasicInfantry()
        {
            return new Gnoll();
        }

        public IUnit GetAdvancedInfantry()
        {
            throw new NotImplementedException();
        }

        public IUnit GetBasicRanged()
        {
            throw new NotImplementedException();
        }

        public IUnit GetAdvancedRanged()
        {
            throw new NotImplementedException();
        }

        public IUnit GetBasicMounted()
        {
            throw new NotImplementedException();
        }

        public IUnit GetAdvancedMounted()
        {
            throw new NotImplementedException();
        }

        public IUnit GetBasicSpellCater()
        {
            throw new NotImplementedException();
        }

        public IUnit GetAdvancedSpellCaster()
        {
            throw new NotImplementedException();
        }
    }
}
