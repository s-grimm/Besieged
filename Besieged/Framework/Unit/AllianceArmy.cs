using Framework.Sprite;

namespace Framework.Unit
{
    public class Dwarf : DrawableObject, IUnit
    {
        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public string Owner { get; set; }

        public int CurrentHealthPoints { get; set; }

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
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

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
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

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
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

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
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

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
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

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
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

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
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

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
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

        public int MaxHealthPoints { get { return 25; } }

        public int Initiative { get { return 0; } }

        public int Skill { get { return 0; } }

        public int Range { get { return 0; } }

        public int Cost { get { return 0; } }

        public int Movement { get { return 0; } }

        public string Description { get { return "Alliance Castle"; } }
    }
}