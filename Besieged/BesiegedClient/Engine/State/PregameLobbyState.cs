using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BesiegedClient.Engine.State
{
    public class PregameLobbyState : IGameState
    {
        private static PregameLobbyState m_Instance;
        private Label m_PlaceHolderLabel;

        private PregameLobbyState() { }
        
        public static IGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new PregameLobbyState();
                    Action action = () => m_Instance.Initialize();
                    ClientGameEngine.Get().ExecuteOnUIThread(action);
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
            //m_LoadingAnimation = new LoadingAnimation();
            //m_LoadingAnimation.Width = 100;
            //m_LoadingAnimation.Height = 100;
            m_PlaceHolderLabel = new Label();
            m_PlaceHolderLabel.Content = "Pregame Lobby";
        }

        public void Render()
        {
            Dimensions dimensions = ClientGameEngine.Get().ClientDimensions;
            Canvas.SetLeft(m_PlaceHolderLabel, dimensions.Width / 2);
            Canvas.SetBottom(m_PlaceHolderLabel, dimensions.Height / 2);
            ClientGameEngine.Get().Canvas.Children.Add(m_PlaceHolderLabel);
        }
    }
}
