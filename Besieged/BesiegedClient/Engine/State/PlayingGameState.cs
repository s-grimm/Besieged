using Framework.BesiegedMessages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BesiegedClient.Engine.State.InGameEngine;

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
    	private TextBox m_ChatMessageBox;
        private Button m_MenuButton;
        private ListBox m_ChatMessagesListBox;
        private GroupBox m_UnitStatsGroupBox;

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

            //Chat Text Box
            m_ChatMessageBox = new TextBox();
            m_ChatMessageBox.Opacity = 0.75;
            m_ChatMessageBox.FontFamily = new FontFamily("Papyrus");
            m_ChatMessageBox.FontSize = 12;
            m_ChatMessageBox.KeyDown += (s, ev) =>
            {
                if (ev.Key == Key.Enter)
                {
                    if (m_ChatMessageBox.Text.Trim() != string.Empty)
                    {
                        if (m_ChatMessageBox.Text.Trim() == "All your base are belong to us" 
                            || m_ChatMessageBox.Text.Trim() == "There is no cow level" )
                        {
                            EndGameMessage endGame = new EndGameMessage();
                            ClientGameEngine.Get().SendMessageToServer(endGame);
                        }
                        else
                        {
                            GameChatMessage chat = new GameChatMessage() { Contents = m_ChatMessageBox.Text.Trim() };
                            m_ChatMessageBox.Text = "";
                            ClientGameEngine.Get().SendMessageToServer(chat);
                        }
                    }
                }
            };

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
			
			//Menu Button
            m_MenuButton = new Button();
            m_MenuButton.FontFamily = new FontFamily("Papyrus");
            m_MenuButton.FontSize = 18;
            m_MenuButton.Content = "Menu";
            m_MenuButton.Click += (s, ev) =>
            {
                
            };

            //Chat messages
            m_ChatMessagesListBox = new ListBox();
            m_ChatMessagesListBox.ItemsSource = ClientGameEngine.Get().ChatMessageCollection;
            m_ChatMessagesListBox.Opacity = 0.75;
            m_ChatMessagesListBox.FontFamily = new FontFamily("Papyrus");
            m_ChatMessagesListBox.FontSize = 12;
            ClientGameEngine.Get().ChatMessageCollection.CollectionChanged += (s, ev) =>
            {
                if (ev.NewItems != null)
                {
                    m_ChatMessagesListBox.ScrollIntoView(ev.NewItems[0]);
                }
            };

            //unit stats
            m_UnitStatsGroupBox = new GroupBox();
            m_UnitStatsGroupBox.Opacity = 0.75;
            m_UnitStatsGroupBox.FontFamily = new FontFamily("Papyrus");
            m_UnitStatsGroupBox.FontSize = 12;
            m_UnitStatsGroupBox.Header = "Selected Unit Stats";

        }

        public void Render()
        {
            ClientGameEngine.Get().MediaPlayer.Stop();
            ClientGameEngine.Get().m_CurrentWindow.WindowState = WindowState.Maximized;
            //ClientGameEngine.Get().m_CurrentWindow.WindowStyle = WindowStyle.None;
            //ClientGameEngine.Get().m_CurrentWindow.ResizeMode = ResizeMode.NoResize;

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

            //chat text box
            Canvas.SetLeft(m_ChatMessageBox, dimensions.Width * 0.04);
            Canvas.SetBottom(m_ChatMessageBox, dimensions.Height * 0.07);
            Canvas.SetZIndex(m_ChatMessageBox, 1200);
            ClientGameEngine.Get().Canvas.Children.Add(m_ChatMessageBox);
            m_ChatMessageBox.Width = dimensions.Width * 0.35;
            m_ChatMessageBox.Height = dimensions.Height * 0.03;

            //chat list box
            Canvas.SetLeft(m_ChatMessagesListBox, dimensions.Width * 0.04);
            Canvas.SetBottom(m_ChatMessagesListBox, dimensions.Height * 0.11);
            Canvas.SetZIndex(m_ChatMessagesListBox, 1200);
            ClientGameEngine.Get().Canvas.Children.Add(m_ChatMessagesListBox);
            m_ChatMessagesListBox.Height = dimensions.Height * 0.10;
            m_ChatMessagesListBox.Width = dimensions.Width * 0.35;

            //unit stats box
            Canvas.SetLeft(m_UnitStatsGroupBox, dimensions.Width * 0.41);
            Canvas.SetBottom(m_UnitStatsGroupBox, dimensions.Height * 0.07);
            Canvas.SetZIndex(m_UnitStatsGroupBox, 1200);
            ClientGameEngine.Get().Canvas.Children.Add(m_UnitStatsGroupBox);
            m_UnitStatsGroupBox.Height = dimensions.Height * 0.14;
            m_UnitStatsGroupBox.Width = dimensions.Width * 0.35;

            //menu button
            Canvas.SetBottom(m_MenuButton, dimensions.Height * 0.14);
            Canvas.SetLeft(m_MenuButton, dimensions.Width * 0.8225);
            Canvas.SetZIndex(m_MenuButton, 1200);
            m_MenuButton.Height = dimensions.Height * 0.05;
            m_MenuButton.Width = dimensions.Width * 0.10;
            ClientGameEngine.Get().Canvas.Children.Add(m_MenuButton);
            
            //end turn button
            Canvas.SetBottom(m_EndTurnButton, dimensions.Height * 0.07);
            Canvas.SetLeft(m_EndTurnButton, dimensions.Width * 0.8225);
            Canvas.SetZIndex(m_EndTurnButton, 2100);
            m_EndTurnButton.Height = dimensions.Height * 0.05;
            m_EndTurnButton.Width = dimensions.Width * 0.10;
            ClientGameEngine.Get().Canvas.Children.Add(m_EndTurnButton);
        }
    }
}
