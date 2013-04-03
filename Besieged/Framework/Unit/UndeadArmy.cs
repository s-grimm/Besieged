namespace Framework.Unit
{
    public class Skeleton : BaseUnit
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
            get { return 2; }
        }

        public override int Range
        {
            get { return 1; }
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
            get { return "Skeleton"; }
        }
    }

    public class SkeletonCaptain : BaseUnit
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
            get { return 3; }
        }

        public override int Movement
        {
            get { return 4; }
        }

        public override string Description
        {
            get { return "Skeleton Captain"; }
        }
    }

    public class Orc : BaseUnit
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
            get { return 2; }
        }

        public override int Range
        {
            get { return 3; }
        }

        public override int Cost
        {
            get { return 4; }
        }

        public override int Movement
        {
            get { return 4; }
        }

        public override string Description
        {
            get { return "Orc"; }
        }
    }

    public class OrcChief : BaseUnit
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
            get { return 3; }
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
            get { return 8; }
        }

        public override int Movement
        {
            get { return 4; }
        }

        public override string Description
        {
            get { return "Orc Chief"; }
        }
    }

    public class WolfRider : BaseUnit
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
            get { return 4; }
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
            get { return "Wolf Rider"; }
        }
    }

    public class WolfLord : BaseUnit
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
            get { return "Wolf Lord"; }
        }
    }

    public class Vampire : BaseUnit
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
            get { return 1; }
        }

        public override int Skill
        {
            get { return 3; }
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
            get { return 6; }
        }

        public override string Description
        {
            get { return "Vampire"; }
        }
    }

    public class VampireCount : BaseUnit
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
            get { return 3; }
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
            get { return 10; }
        }

        public override int Movement
        {
            get { return 8; }
        }

        public override string Description
        {
            get { return "Vampire Count"; }
        }
    }

    public class UndeadCastle : BaseUnit
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

        public override string Description { get { return "Undead Castle"; } }
    }
}