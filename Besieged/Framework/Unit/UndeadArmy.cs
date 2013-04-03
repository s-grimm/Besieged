using Framework.Sprite;

namespace Framework.Unit
{
    public class Skeleton : DrawableObject, IUnit
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
            get { return 2; }
        }

        public int Range
        {
            get { return 1; }
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
            get { return "Skeleton"; }
        }
    }

    public class SkeletonCaptain : DrawableObject, IUnit
    {
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

        public int MaxHealthPoints
        {
            get { return 5; }
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
            get { return 3; }
        }

        public int Movement
        {
            get { return 4; }
        }

        public string Description
        {
            get { return "Skeleton Captain"; }
        }
    }

    public class Orc : DrawableObject, IUnit
    {
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

        public int MaxHealthPoints
        {
            get { return 5; }
        }

        public int Initiative
        {
            get { return 3; }
        }

        public int Skill
        {
            get { return 2; }
        }

        public int Range
        {
            get { return 3; }
        }

        public int Cost
        {
            get { return 4; }
        }

        public int Movement
        {
            get { return 4; }
        }

        public string Description
        {
            get { return "Orc"; }
        }
    }

    public class OrcChief : DrawableObject, IUnit
    {
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

        public int MaxHealthPoints
        {
            get { return 8; }
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
            get { return 4; }
        }

        public int Cost
        {
            get { return 8; }
        }

        public int Movement
        {
            get { return 4; }
        }

        public string Description
        {
            get { return "Orc Chief"; }
        }
    }

    public class WolfRider : DrawableObject, IUnit
    {
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

        public int MaxHealthPoints
        {
            get { return 5; }
        }

        public int Initiative
        {
            get { return 3; }
        }

        public int Skill
        {
            get { return 4; }
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
            get { return "Wolf Rider"; }
        }
    }

    public class WolfLord : DrawableObject, IUnit
    {
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

        public int MaxHealthPoints
        {
            get { return 8; }
        }

        public int Initiative
        {
            get { return 4; }
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
            get { return "Wolf Lord"; }
        }
    }

    public class Vampire : DrawableObject, IUnit
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
            get { return 1; }
        }

        public int Skill
        {
            get { return 3; }
        }

        public int Range
        {
            get { return 3; }
        }

        public int Cost
        {
            get { return 5; }
        }

        public int Movement
        {
            get { return 6; }
        }

        public string Description
        {
            get { return "Vampire"; }
        }
    }

    public class VampireCount : DrawableObject, IUnit
    {
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

        public int MaxHealthPoints
        {
            get { return 8; }
        }

        public int Initiative
        {
            get { return 3; }
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
            get { return 10; }
        }

        public int Movement
        {
            get { return 8; }
        }

        public string Description
        {
            get { return "Vampire Count"; }
        }
    }

    public class UndeadCastle : DrawableObject, IUnit
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

        public string Description { get { return "Undead Castle"; } }
    }
}