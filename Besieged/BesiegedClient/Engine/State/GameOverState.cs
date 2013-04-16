using BesiegedClient.Engine.Dialog;
using Framework.BesiegedMessages;
using Framework.Utilities.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BesiegedClient.Engine.State
{
    public class GameOverState : IGameState
    {
        private static GameOverState m_Instance = null;
        private Image m_LogoImage;
        private Label m_WinnerLabel;
        private Button m_ExitGameButton;
        private ImageBrush m_BackgroundBrush;


        private GameOverState() { }
        
        public static IGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new GameOverState();
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
            double aspectRatio = Math.Round((double)ClientGameEngine.Get().ClientDimensions.Width / (double)ClientGameEngine.Get().ClientDimensions.Height, 2, MidpointRounding.AwayFromZero);
            string UIComponentPath = "resources\\UI\\Menu\\MainMenu\\";
            string ratioPath = string.Empty;

            if (aspectRatio == 1.33)
            {
                //4:3
                ratioPath = "4x3\\";
            }
            else
            {
                //16:9
                ratioPath = "16x9\\";
            }

            BitmapImage bimg;
            try                 // background image
            {
                bimg = new BitmapImage(new Uri(UIComponentPath + ratioPath + "MainMenuBackground.png", UriKind.RelativeOrAbsolute));
                m_BackgroundBrush = new ImageBrush(bimg);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : MainMenuBackground.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try                 // logo image
            {
                bimg = new BitmapImage(new Uri(UIComponentPath + "Logo.png", UriKind.RelativeOrAbsolute));
                m_LogoImage = new Image();
                m_LogoImage.Source = bimg;
                m_LogoImage.Width = bimg.PixelWidth;
                m_LogoImage.Height = bimg.PixelHeight;
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : Logo.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            m_WinnerLabel = new Label();
            m_WinnerLabel.Content = "Winner: " + ClientGameEngine.Get().WinnerName;

            m_ExitGameButton = new Button();
            m_ExitGameButton.FontFamily = new FontFamily("Papyrus");
            m_ExitGameButton.FontSize = 18;
            m_ExitGameButton.Content = "OK";
            m_ExitGameButton.Click += (s, ev) =>
            {
                
            };

        }

        public void Render()
        {
            try
            {
                Dimensions dimensions = ClientGameEngine.Get().ClientDimensions;

                ClientGameEngine.Get().Canvas.Background = m_BackgroundBrush;   // set the background brush

                Canvas.SetLeft(m_LogoImage, dimensions.Width * 0.05);           // resize and add the logo to the canvas
                Canvas.SetTop(m_LogoImage, dimensions.Height * 0.05);
                ClientGameEngine.Get().Canvas.Children.Add(m_LogoImage);

                //undead army button
                Canvas.SetBottom(m_ExitGameButton, dimensions.Height * 0.625);
                Canvas.SetLeft(m_ExitGameButton, dimensions.Width * 0.3775);
                m_ExitGameButton.Height = dimensions.Height * 0.05;
                m_ExitGameButton.Width = dimensions.Width * 0.20;
                ClientGameEngine.Get().Canvas.Children.Add(m_ExitGameButton);

                //Placeholder label
                Canvas.SetBottom(m_WinnerLabel, dimensions.Height * 0.950);
                Canvas.SetLeft(m_WinnerLabel, dimensions.Width * 0.3275);
                m_WinnerLabel.Height = dimensions.Height * 0.05;
                m_WinnerLabel.Width = dimensions.Width * 0.30;
                ClientGameEngine.Get().Canvas.Children.Add(m_WinnerLabel);
            
            }
            catch (Exception ex)
            {
                // error handling
                throw ex;
            }
        }

    }
}
