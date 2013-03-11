using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Unit
{
    // ------------------------------------
    //  Concrete implementations of Beast unit types go here. All are subclassed from Unit and must fufill the IUnit interface
    // ------------------------------------

    /*
     * Class: Gnoll
     * Author: Adam Boyce
     * Date: 2013-03-11
     * Description: This is the Beasts' Basic Melee unit
     */
    public class Gnoll : Unit, IUnit
    {
        public int CurrentHealthPoints
        {
            get
            {
                return this.CurrentHealthPoints;
            }
            set
            {
                this.CurrentHealthPoints = value;
            }
        }

        public int MaxHealthPoints
        {
            get
            {
                return 4;
            }
            set
            {
                this.MaxHealthPoints = value;
            }
        }

        public int Initiative
        {
            get
            {
                return 5;
            }
            set
            {
                this.Initiative = value;
            }
        }

        public int Skill
        {
            get
            {
                return 2;
            }
            set
            {
                this.Skill = value;
            }
        }

        public int Range
        {
            get
            {
                return 1;
            }
            set
            {
                this.Range = value;
            }
        }

        public int Cost
        {
            get
            {
                return 2;
            }
            set
            {
                this.Cost = value;
            }
        }

        public string Description
        {
            get
            {
                return "Gnoll";
            }
            set
            {
                this.Description = value;
            }
        }
    }//end Gnoll

    /*
     * Class: GnollMarauder
     * Author: Adam Boyce
     * Date: 2013-03-11
     * Description: This is the Beasts' Advanced Melee unit
     */
    public class GnollMarauder : Unit, IUnit
    {
        public int CurrentHealthPoints
        {
            get
            {
                return this.CurrentHealthPoints;
            }
            set
            {
                this.CurrentHealthPoints = value;
            }
        }

        public int MaxHealthPoints
        {
            get
            {
                return 7;
            }
            set
            {
                this.MaxHealthPoints = value;
            }
        }

        public int Initiative
        {
            get
            {
                return 7;
            }
            set
            {
                this.Initiative = value;
            }
        }

        public int Skill
        {
            get
            {
                return 4;
            }
            set
            {
                this.Skill = value;
            }
        }

        public int Range
        {
            get
            {
                return 1;
            }
            set
            {
                this.Range = value;
            }
        }

        public int Cost
        {
            get
            {
                return 5;
            }
            set
            {
                this.Cost = value;
            }
        }

        public string Description
        {
            get
            {
                return "Gnoll Marauder";
            }
            set
            {
                this.Description = value;
            }
        }
    }//end GnollMarauder
}
