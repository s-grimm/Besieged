using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BesiegedClient.Engine.State
{
    public interface IGameState
    {
        void Initialize();
        void Render();
    }
}
