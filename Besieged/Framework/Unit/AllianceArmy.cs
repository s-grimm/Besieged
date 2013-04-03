namespace Framework.Unit
{
    public class Dwarf : BaseUnit
    {
        public override int X_Position { get; set; }

        public override int Y_Position { get; set; }

        public override string Owner { get; set; }

        public override int CurrentHealthPoints { get; set; }

        public override int MaxHealthPoints
        {
            get { return 4; }
        }

        public override int Initiative
        {
            get { return 4; }
        }

        public override int Skill
        {
            get { return 3; }
        }

        public override int Range
        {
            get { return 1; }
        }

        public override int Cost
        {
            get { return 2; }
        }

        public override int Movement
        {
            get { return 2; }
        }

        public override string Description
        {
            get { return "Dwarf"; }
        }
    }

    public class DwarfChampion : BaseUnit
    {
        public override string Owner { get; set; }

        public override int X_Position { get; set; }

        public override int Y_Position { get; set; }

        public override int CurrentHealthPoints { get; set; }

        public override int MaxHealthPoints
        {
            get { return 6; }
        }

        public override int Initiative
        {
            get { return 5; }
        }

        public override int Skill
        {
            get { return 5; }
        }

        public override int Range
        {
            get { return 1; }
        }

        public override int Cost
        {
            get { return 5; }
        }

        public override int Movement
        {
            get { return 2; }
        }

        public override string Description
        {
            get { return "Gnoll Marauder"; }
        }
    }

    public class Archer : BaseUnit
    {
        public override string Owner { get; set; }

        public override int X_Position { get; set; }

        public override int Y_Position { get; set; }

        public override int CurrentHealthPoints { get; set; }

        public override int MaxHealthPoints
        {
            get { return 2; }
        }

        public override int Initiative
        {
            get { return 2; }
        }

        public override int Skill
        {
            get { return 3; }
        }

        public override int Range
        {
            get { return 4; }
        }

        public override int Cost
        {
            get { return 1; }
        }

        public override int Movement
        {
            get { return 4; }
        }

        public override string Description
        {
            get { return "Archer"; }
        }
    }

    public class ElvenRanger : BaseUnit
    {
        public override string Owner { get; set; }

        public override int X_Position { get; set; }

        public override int Y_Position { get; set; }

        public override int CurrentHealthPoints { get; set; }

        public override int MaxHealthPoints
        {
            get { return 2; }
        }

        public override int Initiative
        {
            get { return 5; }
        }

        public override int Skill
        {
            get { return 4; }
        }

        public override int Range
        {
            get { return 5; }
        }

        public override int Cost
        {
            get { return 5; }
        }

        public override int Movement
        {
            get { return 7; }
        }

        public override string Description
        {
            get { return "Elven Ranger"; }
        }
    }

    public class Centaur : BaseUnit
    {
        public override string Owner { get; set; }

        public override int X_Position { get; set; }

        public override int Y_Position { get; set; }

        public override int CurrentHealthPoints { get; set; }

        public override int MaxHealthPoints
        {
            get { return 4; }
        }

        public override int Initiative
        {
            get { return 3; }
        }

        public override int Skill
        {
            get { return 3; }
        }

        public override int Range
        {
            get { return 1; }
        }

        public override int Cost
        {
            get { return 5; }
        }

        public override int Movement
        {
            get { return 8; }
        }

        public override string Description
        {
            get { return "Centaur"; }
        }
    }

    public class CentaurOutrider : BaseUnit
    {
        public override string Owner { get; set; }

        public override int X_Position { get; set; }

        public override int Y_Position { get; set; }

        public override int CurrentHealthPoints { get; set; }

        public override int MaxHealthPoints
        {
            get { return 6; }
        }

        public override int Initiative
        {
            get { return 5; }
        }

        public override int Skill
        {
            get { return 5; }
        }

        public override int Range
        {
            get { return 1; }
        }

        public override int Cost
        {
            get { return 10; }
        }

        public override int Movement
        {
            get { return 8; }
        }

        public override string Description
        {
            get { return "Centaur Outrider"; }
        }
    }

    public class Wizard : BaseUnit
    {
        public override string Owner { get; set; }

        public override int X_Position { get; set; }

        public override int Y_Position { get; set; }

        public override int CurrentHealthPoints { get; set; }

        public override int MaxHealthPoints
        {
            get { return 2; }
        }

        public override int Initiative
        {
            get { return 4; }
        }

        public override int Skill
        {
            get { return 3; }
        }

        public override int Range
        {
            get { return 5; }
        }

        public override int Cost
        {
            get { return 5; }
        }

        public override int Movement
        {
            get { return 5; }
        }

        public override string Description
        {
            get { return "Wizard"; }
        }
    }

    public class AscendantWizard : BaseUnit
    {
        public override string Owner { get; set; }

        public override int X_Position { get; set; }

        public override int Y_Position { get; set; }

        public override int CurrentHealthPoints { get; set; }

        public override int MaxHealthPoints
        {
            get { return 10; }
        }

        public override int Initiative
        {
            get { return 4; }
        }

        public override int Skill
        {
            get { return 3; }
        }

        public override int Range
        {
            get { return 5; }
        }

        public override int Cost
        {
            get { return 10; }
        }

        public override int Movement
        {
            get { return 5; }
        }

        public override string Description
        {
            get { return "Ascendant Wizard"; }
        }
    }

    public class AllianceCastle : BaseUnit
    {
        public override string Owner { get; set; }

        public override int X_Position { get; set; }

        public override int Y_Position { get; set; }

        public override int CurrentHealthPoints { get; set; }

        public override int MaxHealthPoints { get { return 25; } }

        public override int Initiative { get { return 0; } }

        public override int Skill { get { return 0; } }

        public override int Range { get { return 0; } }

        public override int Cost { get { return 0; } }

        public override int Movement { get { return 0; } }

        public override string Description { get { return "Alliance Castle"; } }
    }
}