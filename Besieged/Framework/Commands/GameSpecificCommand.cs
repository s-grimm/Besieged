using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Commands
{
    public class GameSpecificCommand : Command
    {
    }

    public class PlayerReady : GameSpecificCommand
    {
        public PlayerReady() { }
    }

    public class PlayerNotReady : GameSpecificCommand
    {
        public PlayerNotReady() { }
    }
}
