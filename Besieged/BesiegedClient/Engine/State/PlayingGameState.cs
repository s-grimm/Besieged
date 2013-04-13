using Framework.BesiegedMessages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BesiegedClient.Engine.State
{
    public class PlayingGameState : IGameState
    {
        private static readonly Dimensions UI_Master = new Dimensions() { Height = 1080, Width = 1920 };
        private static PlayingGameState m_Instance = null;


        private PlayingGameState()
        {
        }

        private Image m_UITopImage;
        private Image m_UILeftImage;
        private Image m_UIRightImage;
        private Image m_UIBottomImage;

        private Button m_EndTurnButton;

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

            m_EndTurnButton = new Button { FontFamily = new FontFamily("Papyrus"), FontSize = 18, Content = "End Turn" };
            m_EndTurnButton.Click += (s, ev) =>
            {
                if (InGameEngine.InGameEngine.Get().MoveList.Count > 0)
                {
                    ClientGameEngine.Get().SendMessageToServer((new EndMoveTurnMessage() { Moves = InGameEngine.InGameEngine.Get().MoveList }));
                }
            };

            m_UITopImage = new Image();
            BitmapImage bimg = new BitmapImage(new Uri(@"resources\UI\Game\TopBar.png", UriKind.RelativeOrAbsolute));
            m_UITopImage.Source = bimg;
            m_UITopImage.Width = bimg.PixelWidth;
            m_UITopImage.Height = bimg.PixelHeight;

            m_UILeftImage = new Image();
            bimg = new BitmapImage(new Uri(@"resources\UI\Game\LeftBar.png", UriKind.RelativeOrAbsolute));
            m_UILeftImage.Source = bimg;
            m_UILeftImage.Width = bimg.PixelWidth;
            m_UILeftImage.Height = bimg.PixelHeight;

            m_UIRightImage = new Image();
            bimg = new BitmapImage(new Uri(@"resources\UI\Game\RightBar.png", UriKind.RelativeOrAbsolute));
            m_UIRightImage.Source = bimg;
            m_UIRightImage.Width = bimg.PixelWidth;
            m_UIRightImage.Height = bimg.PixelHeight;

            m_UIBottomImage = new Image();
            bimg = new BitmapImage(new Uri(@"resources\UI\Game\BottomBar.png", UriKind.RelativeOrAbsolute));
            m_UIBottomImage.Source = bimg;
            m_UIBottomImage.Width = bimg.PixelWidth;
            m_UIBottomImage.Height = bimg.PixelHeight;
        }

        public void Render()
        {
            ClientGameEngine.Get().m_CurrentWindow.WindowState = WindowState.Maximized;
            ClientGameEngine.Get().m_CurrentWindow.WindowStyle = WindowStyle.None;
            ClientGameEngine.Get().m_CurrentWindow.ResizeMode = ResizeMode.NoResize;

            ClientGameEngine.Get().Canvas.Width = (int)ClientGameEngine.Get().m_CurrentWindow.Width;
            ClientGameEngine.Get().Canvas.Height = (int)ClientGameEngine.Get().m_CurrentWindow.Height;
            ClientGameEngine.Get().ClientDimensions.Width = (int)ClientGameEngine.Get().m_CurrentWindow.Width;
            ClientGameEngine.Get().ClientDimensions.Height = (int)ClientGameEngine.Get().m_CurrentWindow.Height;

            InGameEngine.InGameEngine.Get().VirtualGameCanvas.Width = (int)ClientGameEngine.Get().m_CurrentWindow.Width;
            InGameEngine.InGameEngine.Get().VirtualGameCanvas.Height = (int)ClientGameEngine.Get().m_CurrentWindow.Height;

            InGameEngine.InGameEngine.Get().GameCanvas.Width = (int)ClientGameEngine.Get().m_CurrentWindow.Width;
            InGameEngine.InGameEngine.Get().GameCanvas.Height = (int)ClientGameEngine.Get().m_CurrentWindow.Height;

            ClientGameEngine.Get().Canvas.Background = Utilities.Rendering.GrayBrush;

            double wScale = ClientGameEngine.Get().ClientDimensions.Width / (double)UI_Master.Width;
            double hScale = ClientGameEngine.Get().ClientDimensions.Height / (double)UI_Master.Height;

            #region "UI Sizing"
            //top
            m_UITopImage.Width *= wScale;
            m_UITopImage.Height *= hScale;

            Canvas.SetTop(m_UITopImage, 0);
            Canvas.SetLeft(m_UITopImage, 0);
            Canvas.SetZIndex(m_UITopImage, 999);

            ClientGameEngine.Get().Canvas.Children.Add(m_UITopImage);
            //left
            m_UILeftImage.Width *= wScale;
            m_UILeftImage.Height *= hScale;

            Canvas.SetTop(m_UILeftImage, 0);
            Canvas.SetLeft(m_UILeftImage, 0);
            Canvas.SetZIndex(m_UILeftImage, 999);

            ClientGameEngine.Get().Canvas.Children.Add(m_UILeftImage);
            //right
            m_UIRightImage.Width *= wScale;
            m_UIRightImage.Height *= hScale;

            Canvas.SetTop(m_UIRightImage, 0);
            Canvas.SetRight(m_UIRightImage, 0);
            Canvas.SetZIndex(m_UIRightImage, 999);

            ClientGameEngine.Get().Canvas.Children.Add(m_UIRightImage);
            //bottom
            m_UIBottomImage.Width *= wScale;
            m_UIBottomImage.Height *= hScale;

            Canvas.SetBottom(m_UIBottomImage, 0);
            Canvas.SetLeft(m_UIBottomImage, 0);
            Canvas.SetZIndex(m_UIBottomImage, 999);

            ClientGameEngine.Get().Canvas.Children.Add(m_UIBottomImage);

            InGameEngine.InGameEngine.Get().UI_ScaleAdjustment = m_UIBottomImage.Height;

            #endregion "UI Sizing"

            //set the InGameState up
            InGameEngine.InGameEngine.Get().ChangeState(InGameEngine.State.InGameSetupState.Get());

            //add the Virtual Canvas
            ClientGameEngine.Get().Canvas.Children.Add(InGameEngine.InGameEngine.Get().VirtualGameCanvas);

            Dimensions dimensions = ClientGameEngine.Get().ClientDimensions;

            Canvas.SetBottom(m_EndTurnButton, dimensions.Height * 0.025);
            Canvas.SetLeft(m_EndTurnButton, dimensions.Width * 0.8225);
            Canvas.SetZIndex(m_EndTurnButton, 2100);
            m_EndTurnButton.Height = dimensions.Height * 0.05;
            m_EndTurnButton.Width = dimensions.Width * 0.10;
            ClientGameEngine.Get().Canvas.Children.Add(m_EndTurnButton);
        }
    }
}