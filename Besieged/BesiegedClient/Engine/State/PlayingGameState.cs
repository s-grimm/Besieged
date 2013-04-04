using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
            //BitmapImage bimg;
            ////ehhh draw teh UI stuff here
            //Dimensions dimensions = ClientGameEngine.Get().ClientDimensions;
            //double aspectRatio = Math.Round((double)dimensions.Width / (double)dimensions.Height, 2, MidpointRounding.AwayFromZero);

            //string UIComponentPath = string.Empty;

            //if (aspectRatio == 1.33)
            //{
            //    //4:3
            //    UIComponentPath = "resources\\UI\\Game\\4x3\\";
            //}
            //else
            //{
            //    //16:9
            //    UIComponentPath = "resources\\UI\\Game\\16x9\\";
            //}
            //bimg = new BitmapImage(new Uri(UIComponentPath + "BottomLeftCorner.png", UriKind.RelativeOrAbsolute));
            //m_LeftCorner = new Image();
            //m_LeftCorner.Source = bimg;
            //m_LeftCorner.Width = bimg.PixelWidth;
            //m_LeftCorner.Height = bimg.PixelHeight;

            //bimg = new BitmapImage(new Uri(UIComponentPath + "BottomRightCorner.png", UriKind.RelativeOrAbsolute));
            //m_RightCorner = new Image();
            //m_RightCorner.Source = bimg;
            //m_RightCorner.Width = bimg.PixelWidth;
            //m_RightCorner.Height = bimg.PixelHeight;

            //bimg = new BitmapImage(new Uri(UIComponentPath + "TopBar.png", UriKind.RelativeOrAbsolute));
            //m_TopBar = new Image();
            //m_TopBar.Source = bimg;
            //m_TopBar.Width = bimg.PixelWidth;
            //m_TopBar.Height = bimg.PixelHeight;
            //jump start the ingame engine
            InGameEngine.InGameEngine.Get();
        }

        public void Render()
        {
            ClientGameEngine.Get().m_CurrentWindow.WindowState = WindowState.Maximized;
            ClientGameEngine.Get().m_CurrentWindow.WindowStyle = WindowStyle.ThreeDBorderWindow;
            ClientGameEngine.Get().m_CurrentWindow.ResizeMode = ResizeMode.NoResize;
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
