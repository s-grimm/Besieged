﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tiles
{
    public class MudPassable : Tile, ITile
    {

        public int CurrentHealthPointsModifier
        {
            get
            {
                return CurrentHealthPointsModifier;
            }
            set
            {
                CurrentHealthPointsModifier = value;
            }
        }

        public int MaxHealthPointsModifier
        {
            get
            {
                return MaxHealthPointsModifier;
            }
            set
            {
                MaxHealthPointsModifier = value;
            }
        }

        public int InitiativeModifier
        {
            get
            {
                return InitiativeModifier;
            }
            set
            {
                InitiativeModifier = value;
            }
        }

        public int SkillModifier
        {
            get
            {
                return SkillModifier;
            }
            set
            {
                SkillModifier = value;
            }
        }

        public int RangeModifier
        {
            get
            {
                return RangeModifier;
            }
            set
            {
                RangeModifier = value;
            }
        }

        public int MovementModifier
        {
            get
            {
                return MovementModifier;
            }
            set
            {
                MovementModifier = value;
            }
        }

        public bool IsPassable
        {
            get
            {
                return IsPassable;
            }
            set
            {
                IsPassable = true;
            }
        }
    }

    public class MudImpassable : Tile, ITile
    {

        public int CurrentHealthPointsModifier
        {
            get
            {
                return CurrentHealthPointsModifier;
            }
            set
            {
                CurrentHealthPointsModifier = value;
            }
        }

        public int MaxHealthPointsModifier
        {
            get
            {
                return MaxHealthPointsModifier;
            }
            set
            {
                MaxHealthPointsModifier = value;
            }
        }

        public int InitiativeModifier
        {
            get
            {
                return InitiativeModifier;
            }
            set
            {
                InitiativeModifier = value;
            }
        }

        public int SkillModifier
        {
            get
            {
                return SkillModifier;
            }
            set
            {
                SkillModifier = value;
            }
        }

        public int RangeModifier
        {
            get
            {
                return RangeModifier;
            }
            set
            {
                RangeModifier = value;
            }
        }

        public int MovementModifier
        {
            get
            {
                return MovementModifier;
            }
            set
            {
                MovementModifier = value;
            }
        }

        public bool IsPassable
        {
            get
            {
                return IsPassable;
            }
            set
            {
                IsPassable = false;
            }
        }
    }

    public class SwampPassable : Tile, ITile
    {

        public int CurrentHealthPointsModifier
        {
            get
            {
                return CurrentHealthPointsModifier;
            }
            set
            {
                CurrentHealthPointsModifier = value;
            }
        }

        public int MaxHealthPointsModifier
        {
            get
            {
                return MaxHealthPointsModifier;
            }
            set
            {
                MaxHealthPointsModifier = value;
            }
        }

        public int InitiativeModifier
        {
            get
            {
                return InitiativeModifier;
            }
            set
            {
                InitiativeModifier = value;
            }
        }

        public int SkillModifier
        {
            get
            {
                return SkillModifier;
            }
            set
            {
                SkillModifier = value;
            }
        }

        public int RangeModifier
        {
            get
            {
                return RangeModifier;
            }
            set
            {
                RangeModifier = value;
            }
        }

        public int MovementModifier
        {
            get
            {
                return MovementModifier;
            }
            set
            {
                MovementModifier = value;
            }
        }

        public bool IsPassable
        {
            get
            {
                return IsPassable;
            }
            set
            {
                IsPassable = true;
            }
        }
    }

    public class SwampImpassable : Tile, ITile
    {

        public int CurrentHealthPointsModifier
        {
            get
            {
                return CurrentHealthPointsModifier;
            }
            set
            {
                CurrentHealthPointsModifier = value;
            }
        }

        public int MaxHealthPointsModifier
        {
            get
            {
                return MaxHealthPointsModifier;
            }
            set
            {
                MaxHealthPointsModifier = value;
            }
        }

        public int InitiativeModifier
        {
            get
            {
                return InitiativeModifier;
            }
            set
            {
                InitiativeModifier = value;
            }
        }

        public int SkillModifier
        {
            get
            {
                return SkillModifier;
            }
            set
            {
                SkillModifier = value;
            }
        }

        public int RangeModifier
        {
            get
            {
                return RangeModifier;
            }
            set
            {
                RangeModifier = value;
            }
        }

        public int MovementModifier
        {
            get
            {
                return MovementModifier;
            }
            set
            {
                MovementModifier = value;
            }
        }

        public bool IsPassable
        {
            get
            {
                return IsPassable;
            }
            set
            {
                IsPassable = false;
            }
        }
    }
}
