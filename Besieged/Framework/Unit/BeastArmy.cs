using Framework.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Unit
{
    public class Gnoll : DrawableObject, IUnit
    {
        public int CurrentHealthPoints
        {
            get { return CurrentHealthPoints; }
            set { CurrentHealthPoints = value; }
        }

        public int MaxHealthPoints
        {
            get { return 4; }
            set { MaxHealthPoints = value; }
        }

        public int Initiative
        {
            get { return 2; }
            set { Initiative = value; }
        }

        public int Skill
        {
            get { return 2; }
            set { Skill = value; }
        }

        public int Range
        {
            get { return 1; }
            set { Range = value; }
        }

        public int Cost
        {
            get { return 2; }
            set { Cost = value; }
        }

        public int Movement
        {
            get { return 4; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Gnoll"; }
            set { Description = value; }
        }
    }

    public class GnollMarauder : DrawableObject, IUnit
    {
        public int CurrentHealthPoints
        {
            get { return CurrentHealthPoints; }
            set { CurrentHealthPoints = value; }
        }

        public int MaxHealthPoints
        {
            get { return 6; }
            set { MaxHealthPoints = value; }
        }

        public int Initiative
        {
            get { return 3; }
            set { Initiative = value; }
        }

        public int Skill
        {
            get { return 3; }
            set { Skill = value; }
        }

        public int Range
        {
            get { return 1; }
            set { Range = value; }
        }

        public int Cost
        {
            get { return 5; }
            set { Cost = value; }
        }

        public int Movement
        {
            get { return 6; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Gnoll Marauder"; }
            set { Description = value; }
        }
    }

    public class Lizardman : DrawableObject, IUnit
    {
        public int CurrentHealthPoints
        {
            get { return CurrentHealthPoints; }
            set { CurrentHealthPoints = value; }
        }

        public int MaxHealthPoints
        {
            get { return 3; }
            set { MaxHealthPoints = value; }
        }

        public int Initiative
        {
            get { return 2; }
            set { Initiative = value; }
        }

        public int Skill
        {
            get { return 2; }
            set { Skill = value; }
        }

        public int Range
        {
            get { return 3; }
            set { Range = value; }
        }

        public int Cost
        {
            get { return 2; }
            set { Cost = value; }
        }

        public int Movement
        {
            get { return 4; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Lizardman"; }
            set { Description = value; }
        }
    }

    public class LizardmanCaptain : DrawableObject, IUnit
    {
        public int CurrentHealthPoints
        {
            get { return CurrentHealthPoints; }
            set { CurrentHealthPoints = value; }
        }

        public int MaxHealthPoints
        {
            get { return 5; }
            set { MaxHealthPoints = value; }
        }

        public int Initiative
        {
            get { return 2; }
            set { Initiative = value; }
        }

        public int Skill
        {
            get { return 3; }
            set { Skill = value; }
        }

        public int Range
        {
            get { return 5; }
            set { Range = value; }
        }

        public int Cost
        {
            get { return 5; }
            set { Cost = value; }
        }

        public int Movement
        {
            get { return 4; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Lizardman Captain"; }
            set { Description = value; }
        }
    }

    public class Taurus : DrawableObject, IUnit
    {
        public int CurrentHealthPoints
        {
            get { return CurrentHealthPoints; }
            set { CurrentHealthPoints = value; }
        }

        public int MaxHealthPoints
        {
            get { return 5; }
            set { MaxHealthPoints = value; }
        }

        public int Initiative
        {
            get { return 3; }
            set { Initiative = value; }
        }

        public int Skill
        {
            get { return 3; }
            set { Skill = value; }
        }

        public int Range
        {
            get { return 1; }
            set { Range = value; }
        }

        public int Cost
        {
            get { return 5; }
            set { Cost = value; }
        }

        public int Movement
        {
            get { return 8; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Taurus"; }
            set { Description = value; }
        }
    }

    public class TaurusLord : DrawableObject, IUnit
    {
        public int CurrentHealthPoints
        {
            get { return CurrentHealthPoints; }
            set { CurrentHealthPoints = value; }
        }

        public int MaxHealthPoints
        {
            get { return 8; }
            set { MaxHealthPoints = value; }
        }

        public int Initiative
        {
            get { return 4; }
            set { Initiative = value; }
        }

        public int Skill
        {
            get { return 4; }
            set { Skill = value; }
        }

        public int Range
        {
            get { return 1; }
            set { Range = value; }
        }

        public int Cost
        {
            get { return 10; }
            set { Cost = value; }
        }

        public int Movement
        {
            get { return 8; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Taurus Lord"; }
            set { Description = value; }
        }
    }

    public class Genie : DrawableObject, IUnit
    {
        public int CurrentHealthPoints
        {
            get { return CurrentHealthPoints; }
            set { CurrentHealthPoints = value; }
        }

        public int MaxHealthPoints
        {
            get { return 3; }
            set { MaxHealthPoints = value; }
        }

        public int Initiative
        {
            get { return 2; }
            set { Initiative = value; }
        }

        public int Skill
        {
            get { return 2; }
            set { Skill = value; }
        }

        public int Range
        {
            get { return 3; }
            set { Range = value; }
        }

        public int Cost
        {
            get { return 5; }
            set { Cost = value; }
        }

        public int Movement
        {
            get { return 4; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Genie"; }
            set { Description = value; }
        }
    }

    public class MasterGenie : DrawableObject, IUnit
    {
        public int CurrentHealthPoints
        {
            get { return CurrentHealthPoints;    }
            set { CurrentHealthPoints = value; }
        }

        public int MaxHealthPoints
        {
            get { return 6; }
            set { MaxHealthPoints = value; }
        }

        public int Initiative
        {
            get { return 3; }
            set { Initiative = value; }
        }

        public int Skill
        {
            get { return 3; }
            set { Skill = value; }
        }

        public int Range
        {
            get { return 5; }
            set { Range = value; }
        }

        public int Cost
        {
            get { return 8; }
            set { Cost = value; }
        }

        public int Movement
        {
            get { return 4; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Master Genie"; } 
            set { Description = value; }
        }
    }


}
