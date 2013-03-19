using BesiegedClient.Control;
using BesiegedClient.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BesiegedClient.Engine.State
{
    public class LoadingState : IGameState
    {
        private static LoadingState m_Instance = null;
        private LoadingAnimation m_LoadingAnimation;

        private LoadingState() { }
        
        public static IGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new LoadingState();
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
            m_LoadingAnimation = new LoadingAnimation();
            m_LoadingAnimation.Width = 100;
            m_LoadingAnimation.Height = 100;
        }

        public void Render()
        {
            Dimensions dimensions = ClientGameEngine.Get().ClientDimensions;
            ClientGameEngine.Get().Canvas.Background = RenderingUtilities.BlackBrush;
            Canvas.SetLeft(m_LoadingAnimation, dimensions.Width / 2 - m_LoadingAnimation.Width / 2);
            Canvas.SetBottom(m_LoadingAnimation, dimensions.Height / 2 - m_LoadingAnimation.Height / 2);
            ClientGameEngine.Get().Canvas.Children.Add(m_LoadingAnimation);
        }
    }
}
