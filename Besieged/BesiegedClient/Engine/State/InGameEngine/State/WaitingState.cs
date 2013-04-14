using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BesiegedClient.Engine.State.InGameEngine.State
{
    public class WaitingState : IInGameState
    {

        private static WaitingState m_Instance = null;

        public void Cleanup()
        {
        }

        private WaitingState()
        {
        }

        public void Initialize()
        {
         
        }

        public void Render()
        {
          
        }

        public void Dispose()
        {
       
        }

        public static IInGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new WaitingState();
                    m_Instance.Initialize();
                }
                return m_Instance;
            }
            catch (Exception ex)
            {
                // error handling
                throw;
            }
        }
    }
}
