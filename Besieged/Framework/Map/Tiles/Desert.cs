using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tiles
{
    public class SandPassable : Tile, ITile
    {

        public int currentHealthPointsModifier
        {
            get
            {
                return currentHealthPointsModifier;
            }
            set
            {
                currentHealthPointsModifier = value;
            }
        }

        public int maxHealthPointsModifier
        {
            get
            {
                return maxHealthPointsModifier;
            }
            set
            {
                maxHealthPointsModifier = value;
            }
        }

        public int initiativeModifier
        {
            get
            {
                return initiativeModifier;
            }
            set
            {
                initiativeModifier = value;
            }
        }

        public int skillModifier
        {
            get
            {
                return skillModifier;
            }
            set
            {
                skillModifier = value;
            }
        }

        public int rangeModifier
        {
            get
            {
                return rangeModifier;
            }
            set
            {
                rangeModifier = value;
            }
        }

        public int movementModifier
        {
            get
            {
                return movementModifier;
            }
            set
            {
                movementModifier = value;
            }
        }

        public bool isPassable
        {
            get
            {
                return isPassable;
            }
            set
            {
                isPassable = true;
            }
        }
    }

    public class SandImpassable : Tile, ITile
    {

        public int currentHealthPointsModifier
        {
            get
            {
                return currentHealthPointsModifier;
            }
            set
            {
                currentHealthPointsModifier = value;
            }
        }

        public int maxHealthPointsModifier
        {
            get
            {
                return maxHealthPointsModifier;
            }
            set
            {
                maxHealthPointsModifier = value;
            }
        }

        public int initiativeModifier
        {
            get
            {
                return initiativeModifier;
            }
            set
            {
                initiativeModifier = value;
            }
        }

        public int skillModifier
        {
            get
            {
                return skillModifier;
            }
            set
            {
                skillModifier = value;
            }
        }

        public int rangeModifier
        {
            get
            {
                return rangeModifier;
            }
            set
            {
                rangeModifier = value;
            }
        }

        public int movementModifier
        {
            get
            {
                return movementModifier;
            }
            set
            {
                movementModifier = value;
            }
        }

        public bool isPassable
        {
            get
            {
                return isPassable;
            }
            set
            {
                isPassable = false;
            }
        }
    }

    public class OasisPassable : Tile, ITile
    {

        public int currentHealthPointsModifier
        {
            get
            {
                return currentHealthPointsModifier;
            }
            set
            {
                currentHealthPointsModifier = value;
            }
        }

        public int maxHealthPointsModifier
        {
            get
            {
                return maxHealthPointsModifier;
            }
            set
            {
                maxHealthPointsModifier = value;
            }
        }

        public int initiativeModifier
        {
            get
            {
                return initiativeModifier;
            }
            set
            {
                initiativeModifier = value;
            }
        }

        public int skillModifier
        {
            get
            {
                return skillModifier;
            }
            set
            {
                skillModifier = value;
            }
        }

        public int rangeModifier
        {
            get
            {
                return rangeModifier;
            }
            set
            {
                rangeModifier = value;
            }
        }

        public int movementModifier
        {
            get
            {
                return movementModifier;
            }
            set
            {
                movementModifier = value;
            }
        }

        public bool isPassable
        {
            get
            {
                return isPassable;
            }
            set
            {
                isPassable = true;
            }
        }
    }

    public class OasisImpassable : Tile, ITile
    {

        public int currentHealthPointsModifier
        {
            get
            {
                return currentHealthPointsModifier;
            }
            set
            {
                currentHealthPointsModifier = value;
            }
        }

        public int maxHealthPointsModifier
        {
            get
            {
                return maxHealthPointsModifier;
            }
            set
            {
                maxHealthPointsModifier = value;
            }
        }

        public int initiativeModifier
        {
            get
            {
                return initiativeModifier;
            }
            set
            {
                initiativeModifier = value;
            }
        }

        public int skillModifier
        {
            get
            {
                return skillModifier;
            }
            set
            {
                skillModifier = value;
            }
        }

        public int rangeModifier
        {
            get
            {
                return rangeModifier;
            }
            set
            {
                rangeModifier = value;
            }
        }

        public int movementModifier
        {
            get
            {
                return movementModifier;
            }
            set
            {
                movementModifier = value;
            }
        }

        public bool isPassable
        {
            get
            {
                return isPassable;
            }
            set
            {
                isPassable = false;
            }
        }
    }

}
