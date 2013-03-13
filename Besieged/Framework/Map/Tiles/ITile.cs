using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map.Tiles
{
    interface ITile
    {
        public int currentHealthPointsModifier { get; set; }
        public int maxHealthPointsModifier { get; set; }
        public int initiativeModifier { get; set; }
        public int skillModifier { get; set; }
        public int rangeModifier { get; set; }
        public int movementModifier { get; set; }
        public bool isPassable { get; set; }
    }
}
