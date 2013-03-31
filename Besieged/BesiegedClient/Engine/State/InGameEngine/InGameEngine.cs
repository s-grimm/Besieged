using BesiegedClient.Engine.State.InGameEngine.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BesiegedClient.Engine.State.InGameEngine
{
    public class InGameEngine
    {
        IInGameState m_CurrentGameState;
        IInGameState m_PreviousGameState;

        public static InGameEngine m_Instance = null;

        public Canvas GameCanvas { get; private set; }

        private InGameEngine() { }

        public static InGameEngine Get()
        {
            if (m_Instance == null)
            {
                m_Instance = new InGameEngine();
            }
            return m_Instance;
        }
    }
}
