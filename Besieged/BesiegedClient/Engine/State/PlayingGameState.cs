using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BesiegedClient.Engine.State
{
    public class PlayingGameState : IGameState
    {
        private static PlayingGameState m_Instance = null;

        private PlayingGameState() { }

        private Image m_TopBar;
        private Image m_LeftCorner;
        private Image m_RightCorner;

        public static IGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new PlayingGameState();
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

        public void Initialize()
        {
            throw new NotImplementedException(); //ehhh draw teh UI stuff here
        }

        public void Render()
        {
            throw new NotImplementedException(); //render the UI stuff and add the Virtual Canvas
        }
    }
}
