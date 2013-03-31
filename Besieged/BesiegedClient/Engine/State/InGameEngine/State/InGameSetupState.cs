using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BesiegedClient.Engine.State.InGameEngine.State
{
    public class InGameSetupState : IInGameState
    {
        private static InGameSetupState m_Instance = null;
        private InGameSetupState() { }

        public void Initialize()
        {
            //throw new NotImplementedException();
        }

        public void Render()
        {
            //throw new NotImplementedException();
        }

        public void Dispose()
        {
           // throw new NotImplementedException();
        }

        public static IInGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new InGameSetupState();
                    m_Instance.Initialize();
                }
                return m_Instance;
            }
            catch (Exception ex)
            {
                // error handling
                throw ex;
            }
        }
    }
}
