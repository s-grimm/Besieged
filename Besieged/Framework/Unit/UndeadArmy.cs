using Framework.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Unit
{
    public class Skeleton : DrawableObject, IUnit
    {
        public int X_Position { get; set; }
        public int Y_Position { get; set; }

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
            get { return "Skeleton"; }
            set { Description = value; }
        }
    }

    public class SkeletonCaptain : DrawableObject, IUnit
    {
        public int X_Position { get; set; }
        public int Y_Position { get; set; }

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
            get { return 3; }
            set { Cost = value; }
        }

        public int Movement
        {
            get { return 4; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Skeleton Captain"; }
            set { Description = value; }
        }
    }

    public class Orc : DrawableObject, IUnit
    {
        public int X_Position { get; set; }
        public int Y_Position { get; set; }

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
            get { return 4; }
            set { Cost = value; }
        }

        public int Movement
        {
            get { return 4; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Orc"; }
            set { Description = value; }
        }
    }

    public class OrcChief : DrawableObject, IUnit
    {
        public int X_Position { get; set; }
        public int Y_Position { get; set; }

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
            get { return 4; }
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
            get { return "Orc Chief"; }
            set { Description = value; }
        }
    }

    public class WolfRider : DrawableObject, IUnit
    {
        public int X_Position { get; set; }
        public int Y_Position { get; set; }

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
            get { return "Wolf Rider"; }
            set { Description = value; }
        }
    }

    public class WolfLord : DrawableObject, IUnit
    {
        public int X_Position { get; set; }
        public int Y_Position { get; set; }

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
            get { return "Wolf Lord"; }
            set { Description = value; }
        }
    }

    public class Vampire : DrawableObject, IUnit
    {
        public int X_Position { get; set; }
        public int Y_Position { get; set; }

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
            get { return 1; }
            set { Initiative = value; }
        }

        public int Skill
        {
            get { return 3; }
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
            get { return 6; }
            set { Movement = value; }
        }

        public string Description
        {
            get { return "Vampire"; }
            set { Description = value; }
        }
    }

    public class VampireCount : DrawableObject, IUnit
    {
        public int X_Position { get; set; }
        public int Y_Position { get; set; }

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
            get { return 3; }
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
            get { return "Vampire Count"; }
            set { Description = value; }
        }
    }

    public class UndeadCastle : DrawableObject, IUnit
    {
        public int X_Position { get; set; }
        public int Y_Position { get; set; }

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
