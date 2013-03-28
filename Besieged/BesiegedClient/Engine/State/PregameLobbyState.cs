using Framework.Commands;
using Framework.Utilities.Xml;
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
    public class PregameLobbyState : IGameState
    {
        private static PregameLobbyState m_Instance;
        private Label m_PlaceHolderLabel;

        private PregameLobbyState() { }

        private Image m_OKImage;
        private TextBox m_ChatMessageBox;
        private string m_ChatMessage;
        private ListBox m_ListBoxChatMessages;

        private ImageBrush m_BackgroundBrush;
        
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
            try
            {
                double aspectRatio = Math.Round((double)ClientGameEngine.Get().ClientDimensions.Width / (double)ClientGameEngine.Get().ClientDimensions.Height, 2, MidpointRounding.AwayFromZero);

                string UIComponentPath = "resources\\UI\\Menu\\MultiplayerMenu\\";
                string ratioPath = string.Empty;

                if (aspectRatio == 1.33)
                    ratioPath = "4x3\\";
                else
                    ratioPath = "16x9\\";

                //background
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + ratioPath + "NewGameBackground.png", UriKind.RelativeOrAbsolute));
                m_BackgroundBrush = new ImageBrush(bitmapImage);

                //Chat Text Box
                m_ChatMessageBox = new TextBox();
                m_ChatMessageBox.Opacity = 0.75;
                m_ChatMessageBox.FontFamily = new FontFamily("Papyrus");
                m_ChatMessageBox.FontSize = 18;

                //Chat Message OK
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "OK.png", UriKind.RelativeOrAbsolute));
                m_OKImage = new Image();
                m_OKImage.Source = bitmapImage;
                m_OKImage.Width = bitmapImage.PixelWidth;
                m_OKImage.Height = bitmapImage.PixelHeight;
                m_OKImage.Name = "SendMessage";
                //m_OKImage.MouseEnter += MenuOptionHover;
                //m_OKImage.MouseLeave += MenuOptionHoverLost;
                //m_OKImage.MouseDown += MenuOptionMouseDown;
                //m_OKImage.MouseUp += MenuOptionMouseUp;
                
                //Player List
                m_ListBoxChatMessages = new ListBox();
                m_ListBoxChatMessages.ItemsSource = GlobalResources.GameSpecificChatMessages;
                m_ListBoxChatMessages.Opacity = 0.75;
                m_ListBoxChatMessages.FontFamily = new FontFamily("Papyrus");
                m_ListBoxChatMessages.FontSize = 14;
                
               

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        

        public void Render()
        {
            Dimensions dimensions = ClientGameEngine.Get().ClientDimensions;
            Canvas.SetLeft(m_ChatMessageBox, dimensions.Width * 0.15);
            Canvas.SetBottom(m_ChatMessageBox, dimensions.Height * 0.025);
            ClientGameEngine.Get().Canvas.Children.Add(m_ChatMessageBox);
            m_ChatMessageBox.Width = dimensions.Width * 0.60;
            m_ChatMessageBox.Height = dimensions.Height * 0.05;

            Canvas.SetLeft(m_ListBoxChatMessages, dimensions.Width * 0.15);
            Canvas.SetBottom(m_ListBoxChatMessages, dimensions.Height * 0.10);
            ClientGameEngine.Get().Canvas.Children.Add(m_ListBoxChatMessages);
            m_ListBoxChatMessages.Height = dimensions.Height * 0.25;
            m_ListBoxChatMessages.Width = dimensions.Width * 0.7;

            Canvas.SetLeft(m_OKImage, dimensions.Width * 0.9);
            Canvas.SetBottom(m_OKImage, dimensions.Width * 0.10);
            ClientGameEngine.Get().Canvas.Children.Add(m_OKImage);
            
            
            
        }

        



    }
}
