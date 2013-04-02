using Framework.Sprite;

namespace Framework.Unit
{
    public class Gnoll : DrawableObject, IUnit
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
            get { return 2; }
        }

        public int Movement
        {
            get { return 4; }
        }

        public string Description
        {
            get { return "Gnoll"; }
        }
    }

    public class GnollMarauder : DrawableObject, IUnit
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
            get { return 6; }
        }

        public string Description
        {
            get { return "Gnoll Marauder"; }
        }
    }

    public class Lizardman : DrawableObject, IUnit
    {
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

        public int MaxHealthPoints
        {
            get { return 3; }
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
            get { return 3; }
        }

        public int Cost
        {
            get { return 2; }
        }

        public int Movement
        {
            get { return 4; }
        }

        public string Description
        {
            get { return "Lizardman"; }
        }
    }

    public class LizardmanCaptain : DrawableObject, IUnit
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
            get { return 2; }
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
            get { return 4; }
        }

        public string Description
        {
            get { return "Lizardman Captain"; }
        }
    }

    public class Taurus : DrawableObject, IUnit
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
            get { return 5; }
        }

        public int Movement
        {
            get { return 8; }
        }

        public string Description
        {
            get { return "Taurus"; }
        }
    }

    public class TaurusLord : DrawableObject, IUnit
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
            get { return 4; }
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
            get { return "Taurus Lord"; }
        }
    }

    public class Genie : DrawableObject, IUnit
    {
        public string Owner { get; set; }

        public int X_Position { get; set; }

        public int Y_Position { get; set; }

        public int CurrentHealthPoints { get; set; }

        public int MaxHealthPoints
        {
            get { return 3; }
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
            get { return 3; }
        }

        public int Cost
        {
            get { return 5; }
        }

        public int Movement
        {
            get { return 4; }
        }

        public string Description
        {
            get { return "Genie"; }
        }
    }

    public class MasterGenie : DrawableObject, IUnit
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
            get { return 3; }
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
            get { return 8; }
        }

        public int Movement
        {
            get { return 4; }
        }

        public string Description
        {
            get { return "Master Genie"; }
        }
    }

    public class BeastCastle : DrawableObject, IUnit
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

        public string Description { get { return "Beast Castle"; } }
    }
}