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
            return new GnollMarauder();
        }

        public IUnit GetBasicRanged()
        {
            return new Lizardman();
        }

        public IUnit GetAdvancedRanged()
        {
            return new LizardmanCaptain();
        }

        public IUnit GetBasicMounted()
        {
            return new Taurus();
        }

        public IUnit GetAdvancedMounted()
        {
            return new TaurusLord();
        }

        public IUnit GetBasicSpellCaster()
        {
            return new Genie();
        }

        public IUnit GetAdvancedSpellCaster()
        {
            return new MasterGenie();
        }

        public IUnit GetCastle()
        {
            return new BeastCastle();
        }
    }
}
