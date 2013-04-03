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
        }

        public int Initiative
        {
            get { return 4; }
        }

        public int Skill
        {
            get { return 3; }
        }

        public int Range
        {
            get { return 1; }
        }

        public int Cost
        {
            get { return 2; }
        }

        public int Movement
        {
            get { return 2; }
        }

        public string Description
        {
            get { return "Dwarf"; }
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
        }

        public int Initiative
        {
            get { return 5; }
        }

        public int Skill
        {
            get { return 5; }
        }

        public int Range
        {
            get { return 1; }
        }

        public int Cost
        {
            get { return 5; }
        }

        public int Movement
        {
            get { return 2; }
        }

        public string Description
        {
            get { return "Gnoll Marauder"; }
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
        }

        public int Initiative
        {
            get { return 2; }
        }

        public int Skill
        {
            get { return 3; }
        }

        public int Range
        {
            get { return 4; }
        }

        public int Cost
        {
            get { return 1; }
        }

        public int Movement
        {
            get { return 4; }
        }

        public string Description
        {
            get { return "Archer"; }
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
        }

        public int Initiative
        {
            get { return 5; }
        }

        public int Skill
        {
            get { return 4; }
        }

        public int Range
        {
            get { return 5; }
        }

        public int Cost
        {
            get { return 5; }
        }

        public int Movement
        {
            get { return 7; }
        }

        public string Description
        {
            get { return "Elven Ranger"; }
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
        }

        public int Initiative
        {
            get { return 3; }
        }

        public int Skill
        {
            get { return 3; }
        }

        public int Range
        {
            get { return 1; }
        }

        public int Cost
        {
            get { return 5; }
        }

        public int Movement
        {
            get { return 8; }
        }

        public string Description
        {
            get { return "Centaur"; }
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
        }

        public int Initiative
        {
            get { return 5; }
        }

        public int Skill
        {
            get { return 5; }
        }

        public int Range
        {
            get { return 1; }
        }

        public int Cost
        {
            get { return 10; }
        }

        public int Movement
        {
            get { return 8; }
        }

        public string Description
        {
            get { return "Centaur Outrider"; }
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
        }

        public int Initiative
        {
            get { return 4; }
        }

        public int Skill
        {
            get { return 3; }
        }

        public int Range
        {
            get { return 5; }
        }

        public int Cost
        {
            get { return 5; }
        }

        public int Movement
        {
            get { return 5; }
        }

        public string Description
        {
            get { return "Wizard"; }
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
        }

        public int Initiative
        {
            get { return 4; }
        }

        public int Skill
        {
            get { return 3; }
        }

        public int Range
        {
            get { return 5; }
        }

        public int Cost
        {
            get { return 10; }
        }

        public int Movement
        {
            get { return 5; }
        }

        public string Description
        {
            get { return "Ascendant Wizard"; }
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