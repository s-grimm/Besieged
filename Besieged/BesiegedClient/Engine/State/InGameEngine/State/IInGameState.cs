using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BesiegedClient.Engine.State.InGameEngine.State
{
    public interface IInGameState
    {
            void Initialize();
            void Render();
            void Dispose();
    }
}
