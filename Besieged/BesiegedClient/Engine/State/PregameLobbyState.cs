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
        private double m_MenuYOffset;
        private double m_MenuXOffset;

        private PregameLobbyState() { }

        private Button m_SendButton;
        private Image m_LeaveEnabledImage;
        private Image m_LeaveDisabledImage;
        private Image m_ReadyImage;
        private Image m_NotReadyImage;
        private Image m_StartEnabledImage;
        private Image m_StartDisabledImage;
        private TextBox m_ChatMessageBox;
        private ListBox m_PlayerListBox;
        private ListBox m_ChatMessagesListBox;

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

                string UIComponentPath = "resources\\UI\\PreGameLobby\\";
                string ratioPath = string.Empty;

                if (aspectRatio == 1.33)
                    ratioPath = "4x3\\";
                else
                    ratioPath = "16x9\\";

                //background
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + ratioPath + "Background.png", UriKind.RelativeOrAbsolute));
                m_BackgroundBrush = new ImageBrush(bitmapImage);

                //Chat Text Box
                m_ChatMessageBox = new TextBox();
                m_ChatMessageBox.Opacity = 0.75;
                m_ChatMessageBox.FontFamily = new FontFamily("Papyrus");
                m_ChatMessageBox.FontSize = 18;

                //Chat Message OK
                m_SendButton = new Button();
                m_SendButton.FontFamily = new FontFamily("Papyrus");
                m_SendButton.FontSize = 18;
                m_SendButton.Content = "Send";
                
                //Player List
                m_ChatMessagesListBox = new ListBox();
                m_ChatMessagesListBox.ItemsSource = ClientGameEngine.GameSpecificChatMessageCollection;
                m_ChatMessagesListBox.Opacity = 0.75;
                m_ChatMessagesListBox.FontFamily = new FontFamily("Papyrus");
                m_ChatMessagesListBox.FontSize = 14;

                //StartEnabled
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "Start.png", UriKind.RelativeOrAbsolute));
                m_StartEnabledImage = new Image();
                m_StartEnabledImage.Source = bitmapImage;
                m_StartEnabledImage.Width = bitmapImage.PixelWidth;
                m_StartEnabledImage.Height = bitmapImage.PixelHeight;
                m_StartEnabledImage.Visibility = System.Windows.Visibility.Visible;
                m_StartEnabledImage.Name = "StartEnabled";
                //m_StartEnabledImage.MouseEnter += MenuOptionHover;
                //m_StartEnabledImage.MouseLeave += MenuOptionHoverLost;
                //m_StartEnabledImage.MouseDown += MenuOptionMouseDown;
                //m_StartEnabledImage.MouseUp += MenuOptionMouseUp;

                //StartDisabled
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "StartFade.png", UriKind.RelativeOrAbsolute));
                m_StartDisabledImage = new Image();
                m_StartDisabledImage.Source = bitmapImage;
                m_StartDisabledImage.Width = bitmapImage.PixelWidth;
                m_StartDisabledImage.Height = bitmapImage.PixelHeight;
                m_StartDisabledImage.Visibility = System.Windows.Visibility.Hidden;
                m_StartDisabledImage.Name = "StartDisabled";
                //m_StartDisabledImage.MouseEnter += MenuOptionHover;
                //m_StartDisabledImage.MouseLeave += MenuOptionHoverLost;
                //m_StartDisabledImage.MouseDown += MenuOptionMouseDown;
                //m_StartDisabledImage.MouseUp += MenuOptionMouseUp;

                //LeaveEnabled
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "Leave.png", UriKind.RelativeOrAbsolute));
                m_LeaveEnabledImage = new Image();
                m_LeaveEnabledImage.Source = bitmapImage;
                m_LeaveEnabledImage.Width = bitmapImage.PixelWidth;
                m_LeaveEnabledImage.Height = bitmapImage.PixelHeight;
                m_LeaveEnabledImage.Visibility = System.Windows.Visibility.Visible;
                m_LeaveEnabledImage.Name = "LeaveEnabled";
                //m_LeaveEnabledImage.MouseEnter += MenuOptionHover;
                //m_LeaveEnabledImage.MouseLeave += MenuOptionHoverLost;
                //m_LeaveEnabledImage.MouseDown += MenuOptionMouseDown;
                //m_LeaveEnabledImage.MouseUp += MenuOptionMouseUp;

                //LeaveDisabled
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "LeaveFade.png", UriKind.RelativeOrAbsolute));
                m_LeaveDisabledImage = new Image();
                m_LeaveDisabledImage.Source = bitmapImage;
                m_LeaveDisabledImage.Width = bitmapImage.PixelWidth;
                m_LeaveDisabledImage.Height = bitmapImage.PixelHeight;
                m_LeaveDisabledImage.Visibility = System.Windows.Visibility.Hidden;
                m_LeaveDisabledImage.Name = "LeaveDisabled";
                //m_LeaveDisabledImage.MouseEnter += MenuOptionHover;
                //m_LeaveDisabledImage.MouseLeave += MenuOptionHoverLost;
                //m_LeaveDisabledImage.MouseDown += MenuOptionMouseDown;
                //m_LeaveDisabledImage.MouseUp += MenuOptionMouseUp;

                //Ready
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "Ready.png", UriKind.RelativeOrAbsolute));
                m_ReadyImage = new Image();
                m_ReadyImage.Source = bitmapImage;
                m_ReadyImage.Width = bitmapImage.PixelWidth;
                m_ReadyImage.Height = bitmapImage.PixelHeight;
                m_ReadyImage.Visibility = System.Windows.Visibility.Visible;
                m_ReadyImage.Name = "Ready";
                //m_ReadyImage.MouseEnter += MenuOptionHover;
                //m_ReadyImage.MouseLeave += MenuOptionHoverLost;
                //m_ReadyImage.MouseDown += MenuOptionMouseDown;
                //m_ReadyImage.MouseUp += MenuOptionMouseUp;

                //NotReady
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "NotReady.png", UriKind.RelativeOrAbsolute));
                m_NotReadyImage = new Image();
                m_NotReadyImage.Source = bitmapImage;
                m_NotReadyImage.Width = bitmapImage.PixelWidth;
                m_NotReadyImage.Height = bitmapImage.PixelHeight;
                m_NotReadyImage.Visibility = System.Windows.Visibility.Hidden;
                m_NotReadyImage.Name = "NotReady";
                //m_NotReadyImage.MouseEnter += MenuOptionHover;
                //m_NotReadyImage.MouseLeave += MenuOptionHoverLost;
                //m_NotReadyImage.MouseDown += MenuOptionMouseDown;
                //m_NotReadyImage.MouseUp += MenuOptionMouseUp;

                //Player list
                m_PlayerListBox = new ListBox();
                m_PlayerListBox.ItemsSource = ClientGameEngine.GameSpecificPlayerCollection;
                m_ChatMessagesListBox.Opacity = 0.75;
                m_ChatMessagesListBox.FontFamily = new FontFamily("Papyrus");
                m_ChatMessagesListBox.FontSize = 14;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Render()
        {
            //dimensions and offsets
            Dimensions dimensions = ClientGameEngine.Get().ClientDimensions;
            m_MenuYOffset = dimensions.Height * 0.85;
            m_MenuXOffset = dimensions.Width * 0.15;

            //chat text box
            Canvas.SetLeft(m_ChatMessageBox, dimensions.Width * 0.15);
            Canvas.SetBottom(m_ChatMessageBox, dimensions.Height * 0.025);
            ClientGameEngine.Get().Canvas.Children.Add(m_ChatMessageBox);
            m_ChatMessageBox.Width = dimensions.Width * 0.60;
            m_ChatMessageBox.Height = dimensions.Height * 0.05;

            //chat list box
            Canvas.SetLeft(m_ChatMessagesListBox, dimensions.Width * 0.15);
            Canvas.SetBottom(m_ChatMessagesListBox, dimensions.Height * 0.10);
            ClientGameEngine.Get().Canvas.Children.Add(m_ChatMessagesListBox);
            m_ChatMessagesListBox.Height = dimensions.Height * 0.25;
            m_ChatMessagesListBox.Width = dimensions.Width * 0.7;

            //chat send button
            Canvas.SetBottom(m_SendButton, dimensions.Height * 0.025);
            Canvas.SetLeft(m_SendButton, dimensions.Width * 0.77);
            m_SendButton.Height = dimensions.Height * 0.05;
            m_SendButton.Width = dimensions.Width * 0.08;
            ClientGameEngine.Get().Canvas.Children.Add(m_SendButton);   

            //start game enabled
            Canvas.SetLeft(m_StartEnabledImage, m_MenuXOffset);
            Canvas.SetBottom(m_StartEnabledImage, m_MenuYOffset);
            Canvas.SetZIndex(m_StartEnabledImage, 100);
            ClientGameEngine.Get().Canvas.Children.Add(m_StartEnabledImage);

            //start game disabled
            Canvas.SetLeft(m_StartDisabledImage, m_MenuXOffset);
            Canvas.SetBottom(m_StartDisabledImage, m_MenuYOffset);
            Canvas.SetZIndex(m_StartDisabledImage, 100);
            ClientGameEngine.Get().Canvas.Children.Add(m_StartDisabledImage);
            m_MenuYOffset -= m_StartDisabledImage.Height * 1.5;

            //leave game enabled
            Canvas.SetLeft(m_LeaveEnabledImage, m_MenuXOffset);
            Canvas.SetBottom(m_LeaveEnabledImage, m_MenuYOffset);
            Canvas.SetZIndex(m_LeaveEnabledImage, 100);
            ClientGameEngine.Get().Canvas.Children.Add(m_LeaveEnabledImage);

            //leave game disabled
            Canvas.SetLeft(m_LeaveDisabledImage, m_MenuXOffset);
            Canvas.SetBottom(m_LeaveDisabledImage, m_MenuYOffset);
            Canvas.SetZIndex(m_LeaveDisabledImage, 100);
            ClientGameEngine.Get().Canvas.Children.Add(m_LeaveDisabledImage);
            m_MenuYOffset -= m_LeaveDisabledImage.Height * 1.5;

            //ready
            Canvas.SetLeft(m_ReadyImage, m_MenuXOffset);
            Canvas.SetBottom(m_ReadyImage, m_MenuYOffset);
            Canvas.SetZIndex(m_ReadyImage, 100);
            ClientGameEngine.Get().Canvas.Children.Add(m_ReadyImage);

            //not ready
            Canvas.SetLeft(m_NotReadyImage, m_MenuXOffset);
            Canvas.SetBottom(m_NotReadyImage, m_MenuYOffset);
            Canvas.SetZIndex(m_NotReadyImage, 100);
            ClientGameEngine.Get().Canvas.Children.Add(m_NotReadyImage);

            //player list box
            Canvas.SetLeft(m_PlayerListBox, dimensions.Width * 0.60);
            Canvas.SetBottom(m_PlayerListBox, dimensions.Height * 0.45);
            ClientGameEngine.Get().Canvas.Children.Add(m_PlayerListBox);
            m_PlayerListBox.Height = dimensions.Height * 0.50;
            m_PlayerListBox.Width = dimensions.Width * 0.25;

        }
    }
}
