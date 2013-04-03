namespace Framework.Unit
{
    public class Gnoll : BaseUnit
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
            get { return 2; }
        }

        public override int Skill
        {
            get { return 2; }
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
            get { return 4; }
        }

        public override string Description
        {
            get { return "Gnoll"; }
        }
    }

    public class GnollMarauder : BaseUnit
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
            get { return 6; }
        }

        public override string Description
        {
            get { return "Gnoll Marauder"; }
        }
    }

    public class Lizardman : BaseUnit
    {
        public override string Owner { get; set; }

        public override int X_Position { get; set; }

        public override int Y_Position { get; set; }

        public override int CurrentHealthPoints { get; set; }

        public override int MaxHealthPoints
        {
            get { return 3; }
        }

        public override int Initiative
        {
            get { return 2; }
        }

        public override int Skill
        {
            get { return 2; }
        }

        public override int Range
        {
            get { return 3; }
        }

        public override int Cost
        {
            get { return 2; }
        }

        public override int Movement
        {
            get { return 4; }
        }

        public override string Description
        {
            get { return "Lizardman"; }
        }
    }

    public class LizardmanCaptain : BaseUnit
    {
        public override string Owner { get; set; }

        public override int X_Position { get; set; }

        public override int Y_Position { get; set; }

        public override int CurrentHealthPoints { get; set; }

        public override int MaxHealthPoints
        {
            get { return 5; }
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
            get { return 5; }
        }

        public override int Cost
        {
            get { return 5; }
        }

        public override int Movement
        {
            get { return 4; }
        }

        public override string Description
        {
            get { return "Lizardman Captain"; }
        }
    }

    public class Taurus : BaseUnit
    {
        public override string Owner { get; set; }

        public override int X_Position { get; set; }

        public override int Y_Position { get; set; }

        public override int CurrentHealthPoints { get; set; }

        public override int MaxHealthPoints
        {
            get { return 5; }
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
            get { return "Taurus"; }
        }
    }

    public class TaurusLord : BaseUnit
    {
        public override string Owner { get; set; }

        public override int X_Position { get; set; }

        public override int Y_Position { get; set; }

        public override int CurrentHealthPoints { get; set; }

        public override int MaxHealthPoints
        {
            get { return 8; }
        }

        public override int Initiative
        {
            get { return 4; }
        }

        public override int Skill
        {
            get { return 4; }
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
            get { return "Taurus Lord"; }
        }
    }

    public class Genie : BaseUnit
    {
        public override string Owner { get; set; }

        public override int X_Position { get; set; }

        public override int Y_Position { get; set; }

        public override int CurrentHealthPoints { get; set; }

        public override int MaxHealthPoints
        {
            get { return 3; }
        }

        public override int Initiative
        {
            get { return 2; }
        }

        public override int Skill
        {
            get { return 2; }
        }

        public override int Range
        {
            get { return 3; }
        }

        public override int Cost
        {
            get { return 5; }
        }

        public override int Movement
        {
            get { return 4; }
        }

        public override string Description
        {
            get { return "Genie"; }
        }
    }

    public class MasterGenie : BaseUnit
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
            get { return 3; }
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
            get { return 8; }
        }

        public override int Movement
        {
            get { return 4; }
        }

        public override string Description
        {
            get { return "Master Genie"; }
        }
    }

    public class BeastCastle : BaseUnit
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

        public override string Description { get { return "Beast Castle"; } }
    }
}