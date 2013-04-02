using Framework.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Unit
{
    public class Dwarf : DrawableObject, IUnit
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
            get { return 4; }
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
            get { return 2; }
            set { Cost = value; }
        }

        public int Movement
        {
            get { return 2; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Dwarf"; }
            set { Description = value; }
        }
    }

    public class DwarfChampion : DrawableObject, IUnit
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
            get { return 5; }
            set { Initiative = value; }
        }

        public int Skill
        {
            get { return 5; }
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
            get { return 2; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Gnoll Marauder"; }
            set { Description = value; }
        }
    }

    public class Archer : DrawableObject, IUnit
    {
        public int CurrentHealthPoints
        {
            get { return CurrentHealthPoints; }
            set { CurrentHealthPoints = value; }
        }

        public int MaxHealthPoints
        {
            get { return 2; }
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
            get { return 4; }
            set { Range = value; }
        }

        public int Cost
        {
            get { return 1; }
            set { Cost = value; }
        }

        public int Movement
        {
            get { return 4; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Archer"; }
            set { Description = value; }
        }
    }

    public class ElvenRanger : DrawableObject, IUnit
    {
        public int CurrentHealthPoints
        {
            get { return CurrentHealthPoints; }
            set { CurrentHealthPoints = value; }
        }

        public int MaxHealthPoints
        {
            get { return 2; }
            set { MaxHealthPoints = value; }
        }

        public int Initiative
        {
            get { return 5; }
            set { Initiative = value; }
        }

        public int Skill
        {
            get { return 4; }
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
            get { return 7; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Elven Ranger"; }
            set { Description = value; }
        }
    }

    public class Centaur : DrawableObject, IUnit
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
            get { return "Centaur"; }
            set { Description = value; }
        }
    }

    public class CentaurOutrider : DrawableObject, IUnit
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
            get { return 5; }
            set { Initiative = value; }
        }

        public int Skill
        {
            get { return 5; }
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
            get { return "Centaur Outrider"; }
            set { Description = value; }
        }
    }

    public class Wizard : DrawableObject, IUnit
    {
        public int CurrentHealthPoints
        {
            get { return CurrentHealthPoints; }
            set { CurrentHealthPoints = value; }
        }

        public int MaxHealthPoints
        {
            get { return 2; }
            set { MaxHealthPoints = value; }
        }

        public int Initiative
        {
            get { return 4; }
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
            get { return 5; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Wizard"; }
            set { Description = value; }
        }
    }

    public class AscendantWizard : DrawableObject, IUnit
    {
        public int CurrentHealthPoints
        {
            get { return CurrentHealthPoints; }
            set { CurrentHealthPoints = value; }
        }

        public int MaxHealthPoints
        {
            get { return 10; }
            set { MaxHealthPoints = value; }
        }

        public int Initiative
        {
            get { return 4; }
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
            get { return 10; }
            set { Cost = value; }
        }

        public int Movement
        {
            get { return 5; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Ascendant Wizard"; }
            set { Description = value; }
        }
    }

    public class AllianceCastle : DrawableObject, IUnit
    {

        public int CurrentHealthPoints { get; set; }

        public int MaxHealthPoints { get; set; }

        public int Initiative { get; set; }

        public int Skill { get; set; }

        public int Range { get; set; }

        public int Cost { get; set; }

        public int Movement { get; set; }

        public string Description { get; set; }
    }

}
