using Framework.BesiegedMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        private TextBox m_ChatMessageBox;
        private Button m_SendButton;
        private Button m_MenuButton;
        private ListBox m_ChatMessagesListBox;

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
            BitmapImage bimg;
            //ehhh draw teh UI stuff here
            Dimensions dimensions = ClientGameEngine.Get().ClientDimensions;
            double aspectRatio = Math.Round((double)dimensions.Width / (double)dimensions.Height, 2, MidpointRounding.AwayFromZero);

            string UIComponentPath = string.Empty;

            if (aspectRatio == 1.33)
            {
                //4:3
                UIComponentPath = "resources\\UI\\Game\\4x3\\";
            }
            else
            {
                //16:9
                UIComponentPath = "resources\\UI\\Game\\16x9\\";
            }
            bimg = new BitmapImage(new Uri(UIComponentPath + "BottomLeftCorner.png", UriKind.RelativeOrAbsolute));
            m_LeftCorner = new Image();
            m_LeftCorner.Source = bimg;
            m_LeftCorner.Width = bimg.PixelWidth;
            m_LeftCorner.Height = bimg.PixelHeight;

            bimg = new BitmapImage(new Uri(UIComponentPath + "BottomRightCorner.png", UriKind.RelativeOrAbsolute));
            m_RightCorner = new Image();
            m_RightCorner.Source = bimg;
            m_RightCorner.Width = bimg.PixelWidth;
            m_RightCorner.Height = bimg.PixelHeight;

            bimg = new BitmapImage(new Uri(UIComponentPath + "TopBar.png", UriKind.RelativeOrAbsolute));
            m_TopBar = new Image();
            m_TopBar.Source = bimg;
            m_TopBar.Width = bimg.PixelWidth;
            m_TopBar.Height = bimg.PixelHeight;
            //jump start the ingame engine
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
                        GameChatMessage chat = new GameChatMessage() { Contents = m_ChatMessageBox.Text.Trim() };
                        m_ChatMessageBox.Text = "";
                        ClientGameEngine.Get().SendMessageToServer(chat);
                    }
                }
            };

            //Chat Message OK
            m_SendButton = new Button();
            m_SendButton.FontFamily = new FontFamily("Papyrus");
            m_SendButton.FontSize = 18;
            m_SendButton.Content = "Send";
            m_SendButton.Click += (s, ev) =>
            {
                if (m_ChatMessageBox.Text.Trim() != string.Empty)
                {
                    GameChatMessage chat = new GameChatMessage() { Contents = m_ChatMessageBox.Text.Trim() };
                    m_ChatMessageBox.Text = "";
                    ClientGameEngine.Get().SendMessageToServer(chat);
                }
            };

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
        }

        public void Render()
        {
            ClientGameEngine.Get().Canvas.Background = Utilities.Rendering.GrayBrush;

            //dimensions and offsets
            Dimensions dimensions = ClientGameEngine.Get().ClientDimensions;


            Canvas.SetLeft(m_LeftCorner, 0);
            Canvas.SetBottom(m_LeftCorner, 0);
            Canvas.SetZIndex(m_LeftCorner, 999);

            ClientGameEngine.Get().Canvas.Children.Add(m_LeftCorner);

            Canvas.SetRight(m_RightCorner, 0);
            Canvas.SetBottom(m_RightCorner, 0);
            Canvas.SetZIndex(m_RightCorner, 999);

            ClientGameEngine.Get().Canvas.Children.Add(m_RightCorner);

            Canvas.SetTop(m_TopBar, 0);
            Canvas.SetZIndex(m_TopBar, 999);

            ClientGameEngine.Get().Canvas.Children.Add(m_TopBar);

            //chat text box
            Canvas.SetLeft(m_ChatMessageBox, dimensions.Width * 0.01);
            Canvas.SetBottom(m_ChatMessageBox, dimensions.Height * 0.01);
            Canvas.SetZIndex(m_ChatMessageBox, 1200);
            ClientGameEngine.Get().Canvas.Children.Add(m_ChatMessageBox);
            m_ChatMessageBox.Width = dimensions.Width * 0.35;
            m_ChatMessageBox.Height = dimensions.Height * 0.05;

            //chat send button
            Canvas.SetBottom(m_SendButton, dimensions.Height * 0.01);
            Canvas.SetLeft(m_SendButton, dimensions.Width * 0.36);
            Canvas.SetZIndex(m_SendButton, 1200);
            m_SendButton.Height = dimensions.Height * 0.05;
            m_SendButton.Width = dimensions.Width * 0.10;
            ClientGameEngine.Get().Canvas.Children.Add(m_SendButton);

            //chat list box
            Canvas.SetLeft(m_ChatMessagesListBox, dimensions.Width * 0.01);
            Canvas.SetBottom(m_ChatMessagesListBox, dimensions.Height * 0.07);
            Canvas.SetZIndex(m_ChatMessagesListBox, 1200);
            ClientGameEngine.Get().Canvas.Children.Add(m_ChatMessagesListBox);
            m_ChatMessagesListBox.Height = dimensions.Height * 0.14;
            m_ChatMessagesListBox.Width = dimensions.Width * 0.35;

            //menu button
            Canvas.SetBottom(m_MenuButton, dimensions.Height * 0.94);
            Canvas.SetLeft(m_MenuButton, dimensions.Width * 0.89);
            Canvas.SetZIndex(m_MenuButton, 1200);
            m_MenuButton.Height = dimensions.Height * 0.05;
            m_MenuButton.Width = dimensions.Width * 0.10;
            ClientGameEngine.Get().Canvas.Children.Add(m_MenuButton);
            
            //set the InGameState up
            InGameEngine.InGameEngine.Get().ChangeState(InGameEngine.State.InGameSetupState.Get());
            //add the Virtual Canvas
            ClientGameEngine.Get().Canvas.Children.Add(InGameEngine.InGameEngine.Get().VirtualGameCanvas);
        }
    }
}
