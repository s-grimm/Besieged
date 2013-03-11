using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Unit
{
    // ------------------------------------
    //  Concrete implementations of Beast unit types go here. All are subclassed from Unit and must fufill the IUnit interface
    // ------------------------------------

    public class Gnoll : Unit, IUnit
    {
        public int CurrentHealthPoints
        {
            get
            {
                return CurrentHealthPoints;
            }
            set
            {
                CurrentHealthPoints = value;
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
                MaxHealthPoints = value;
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
                Initiative = value;
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
                Skill = value;
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
                Range = value;
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
                Cost = value;
            }
        }

        public int Movement
        {
            get
            {
                return 4;
            }
            set
            {
                Movement = value;
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
                Description = value;
            }
        }
    }

    public class GnollMarauder : Unit, IUnit
    {
        public int CurrentHealthPoints
        {
            get
            {
                return  CurrentHealthPoints;
            }
            set
            {
                CurrentHealthPoints = value;
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
                MaxHealthPoints = value;
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
                Initiative = value;
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
                Skill = value;
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
                Range = value;
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
                Cost = value;
            }
        }

        public int Movement
        {
            get
            {
                return 5;
            }
            set
            {
                Movement = value;
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
                Description = value;
            }
        }
    }
}
