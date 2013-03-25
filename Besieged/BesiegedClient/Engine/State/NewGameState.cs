using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BesiegedClient.Engine.State
{
    public class NewGameState : IGameState
    {
        private static NewGameState m_Instance = null;

        private NewGameState()
        {
        }

        private double m_MenuYOffset;
        private double m_MenuXOffset;

        private Image m_GameNameImage;
        private Image m_PasswordImage;
        private Image m_OKImage;
        private Image m_CancelImage;
        private TextBox m_GameNameBox;
        private TextBox m_PasswordBox;

        private string m_GameName;
        private string m_Password;

        private ImageBrush m_BackgroundBrush;

        public static IGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new NewGameState();
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

                //GameName
                m_GameNameImage = new Image();
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "GameName.png", UriKind.RelativeOrAbsolute));
                m_GameNameImage.Source = bitmapImage;
                m_GameNameImage.Width = bitmapImage.PixelWidth;
                m_GameNameImage.Height = bitmapImage.PixelHeight;

                //game name text box
                m_GameNameBox = new TextBox();
                m_GameNameBox.FontFamily = new FontFamily("Papyrus");
                m_GameNameBox.FontSize = 24;
                m_GameNameBox.TextChanged += (s, ev) =>
                {
                    m_GameName = ((TextBox)s).Text;
                };

                //password image
                m_PasswordImage = new Image();
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "Password.png", UriKind.RelativeOrAbsolute));
                m_PasswordImage.Source = bitmapImage;
                m_PasswordImage.Width = bitmapImage.PixelWidth;
                m_PasswordImage.Height = bitmapImage.PixelHeight;

                //password box
                m_PasswordBox = new TextBox();
                m_PasswordBox.FontFamily = new FontFamily("Papyrus");
                m_PasswordBox.FontSize = 24;
                m_PasswordBox.Text = "Optional";
                m_PasswordBox.TextChanged += (s, ev) =>
                {
                    m_Password = ((TextBox)s).Text;
                };
                m_PasswordBox.GotFocus += (s, ev) =>
                {
                    if (m_PasswordBox.Text == "Optional")
                    {
                        m_PasswordBox.Text = string.Empty;
                    }
                };
                m_PasswordBox.LostFocus += (s, ev) =>
                {
                    if (m_PasswordBox.Text.Trim() == "")
                    {
                        m_PasswordBox.Text = "Optional";
                    }
                };

                //ok button
                m_OKImage = new Image();
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "OK.png", UriKind.RelativeOrAbsolute));
                m_OKImage.Source = bitmapImage;
                m_OKImage.Width = bitmapImage.PixelWidth;
                m_OKImage.Height = bitmapImage.PixelHeight;
                //commented out due to handlers not existing - s_grimm 23/03/2013 - 22:07
                //m_OKImage.MouseEnter += MenuOptionHover;
                //m_OKImage.MouseLeave += MenuOptionHoverLost;
                //m_OKImage.MouseDown += MenuOptionMouseDown;
                //m_OKImage.MouseUp += MenuOptionMouseUp;
                m_OKImage.Name = "OK";

                //cancel button
                m_CancelImage = new Image();
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "Cancel.png", UriKind.RelativeOrAbsolute));
                m_CancelImage.Source = bitmapImage;
                m_CancelImage.Width = bitmapImage.PixelWidth;
                m_CancelImage.Height = bitmapImage.PixelHeight;
                //commented out due to handlers not existing - s_grimm 23/03/2013 - 22:07
                //m_CancelImage.MouseEnter += MenuOptionHover;
                //m_CancelImage.MouseLeave += MenuOptionHoverLost;
                //m_CancelImage.MouseDown += MenuOptionMouseDown;
                //m_CancelImage.MouseUp += MenuOptionMouseUp;
                m_CancelImage.Name = "Cancel";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void Render()
        {
            dimensions = new Dimensions() { Width = (int)GlobalResources.GameWindow.Width, Height = (int)GlobalResources.GameWindow.Height };
            menuYOffset = dimensions.Height * 0.75;
            menuXOffset = dimensions.Width * 0.65;
            ClientGameEngine.Get().Canvas.Background = m_BackgroundBrush;

            Canvas.SetLeft(m_GameNameImage, dimensions.Width * 0.10);
            Canvas.SetBottom(m_GameNameImage, menuYOffset);
            Canvas.SetZIndex(m_GameNameImage, 100);


            throw new NotImplementedException();
        }
    }
}