using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Unit
{
    public class AllianceUnitFactory : IUnitFactory
    {
        public IUnit GetBasicInfantry()
        {
            return new Dwarf();
        }

        public IUnit GetAdvancedInfantry()
        {
            return new DwarfChampion();
        }

        public IUnit GetBasicRanged()
        {
            return new Archer();
        }

        public IUnit GetAdvancedRanged()
        {
            return new ElvenRanger();
        }

        public IUnit GetBasicMounted()
        {
            return new Centaur();
        }

        public IUnit GetAdvancedMounted()
        {
            return new CentaurOutrider();
        }

        public IUnit GetBasicSpellCaster()
        {
            return new Wizard();
        }

        public IUnit GetAdvancedSpellCaster()
        {
            return new AscendantWizard();
        }

        public IUnit GetCastle()
        {
            return new AllianceCastle();
        }
    }
}
