using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BesiegedClient.Engine.State.InGameEngine;

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
            InGameEngine.InGameEngine.Get();
        }

        public void Render()
        {
            ClientGameEngine.Get().m_CurrentWindow.WindowState = WindowState.Maximized;
            ClientGameEngine.Get().m_CurrentWindow.WindowStyle = WindowStyle.None;
            ClientGameEngine.Get().m_CurrentWindow.ResizeMode = ResizeMode.NoResize;
            ClientGameEngine.Get().m_CurrentWindow.Topmost = true;

            ClientGameEngine.Get().Canvas.Width = (int)ClientGameEngine.Get().m_CurrentWindow.Width;
            ClientGameEngine.Get().Canvas.Height = (int)ClientGameEngine.Get().m_CurrentWindow.Height;
            ClientGameEngine.Get().ClientDimensions.Width = (int)ClientGameEngine.Get().m_CurrentWindow.Width;
            ClientGameEngine.Get().ClientDimensions.Height = (int)ClientGameEngine.Get().m_CurrentWindow.Height;


            InGameEngine.InGameEngine.Get().VirtualGameCanvas.Width = (int)ClientGameEngine.Get().m_CurrentWindow.Width;
            InGameEngine.InGameEngine.Get().VirtualGameCanvas.Height = (int)ClientGameEngine.Get().m_CurrentWindow.Height;

            InGameEngine.InGameEngine.Get().GameCanvas.Width = (int)ClientGameEngine.Get().m_CurrentWindow.Width;
            InGameEngine.InGameEngine.Get().GameCanvas.Height = (int)ClientGameEngine.Get().m_CurrentWindow.Height;

            ClientGameEngine.Get().Canvas.Background = Utilities.Rendering.GrayBrush;

            //Canvas.SetLeft(m_LeftCorner, 0);
            //Canvas.SetBottom(m_LeftCorner, 0);
            //Canvas.SetZIndex(m_LeftCorner, 999);

            //ClientGameEngine.Get().Canvas.Children.Add(m_LeftCorner);

            //Canvas.SetRight(m_RightCorner, 0);
            //Canvas.SetBottom(m_RightCorner, 0);
            //Canvas.SetZIndex(m_RightCorner, 999);

            //ClientGameEngine.Get().Canvas.Children.Add(m_RightCorner);

            //Canvas.SetTop(m_TopBar, 0);
            //Canvas.SetZIndex(m_TopBar, 999);

            //ClientGameEngine.Get().Canvas.Children.Add(m_TopBar);
            
            //set the InGameState up
            InGameEngine.InGameEngine.Get().ChangeState(InGameEngine.State.InGameSetupState.Get());
            //add the Virtual Canvas
            ClientGameEngine.Get().Canvas.Children.Add(InGameEngine.InGameEngine.Get().VirtualGameCanvas);
        }
    }
}
