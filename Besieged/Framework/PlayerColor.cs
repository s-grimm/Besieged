using Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public static class PlayerColor
    {
        public enum PlayerColorEnum { Red, Green, Blue, Yellow, Orange, Purple };

        public static Stack<PlayerColorEnum> GetColors()
        {
            Stack<PlayerColorEnum> colors = new Stack<PlayerColorEnum>();
            List<PlayerColorEnum> shuffleList = new List<PlayerColorEnum>();
            foreach (PlayerColorEnum color in Enum.GetValues(typeof(PlayerColorEnum)))
            {
                shuffleList.Add(color);
            }
            shuffleList.Shuffle();
            shuffleList.ForEach(color => colors.Push(color));
            return colors;
        }
    }
}
