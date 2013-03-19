using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BesiegedClient.Engine.State
{
    public interface IGameState
    {
        //IGameState GetInstance();
        void Initialize();
        void Render();
        //void Cleanup();
    }
}
