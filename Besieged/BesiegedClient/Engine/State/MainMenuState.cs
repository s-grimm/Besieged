using BesiegedClient.Engine.Dialog;
using Framework.Commands;
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
    public class MainMenuState : IGameState
    {
        private static MainMenuState m_Instance = null;
        private double m_MenuYOffset;
        private double m_MenuXOffset;
        private object m_MouseDownSender;

        private Image m_LogoImage;
        private Image m_SinglePlayerImage;
        private Image m_MultiPlayerImage;
        private Image m_OptionsImage;
        private Image m_QuitImage;
        private ImageBrush m_BackgroundBrush;

        private MainMenuState() { }
        
        public static IGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new MainMenuState();
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

        private void MenuOptionHover(object sender, MouseEventArgs e)
        {
            try
            {
                Image img = sender as Image;
                Canvas.SetLeft(img, m_MenuXOffset + 50);
            }
            catch (Exception)
            {
            }
        }

        private void MenuOptionHoverLost(object sender, MouseEventArgs e)
        {
            try
            {
                Image img = sender as Image;
                Canvas.SetLeft(img, m_MenuXOffset);
            }
            catch (Exception)
            {
            }
        }

        private void MenuOptionMouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                m_MouseDownSender = sender;
            }
            catch (Exception)
            {
            }
        }

        private void MenuOptionMouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (m_MouseDownSender != sender) return;
                Image img = sender as Image;
                string selected = img.Name;
                if (selected == "Quit")
                {
                    Application.Current.MainWindow.Close();
                }
                //else if (selected == "SinglePlayer")
                //{
                //    RenderMessageDialog.RenderMessage("Single Player Coming Soon!");
                //}
                else if (selected == "MultiPlayer")
                {
                    if (ClientGameEngine.Get().IsServerConnected.Value)
                    {
                        ClientGameEngine.Get().ChangeState(MultiplayerMenuState.Get());
                    }
                    else
                    {
                        RenderMessageDialog.RenderInput("Please enter a player name: ", (se, ev) =>
                        {
                            if (se == null)
                            {
                                Action action = () => RenderMessageDialog.RenderMessage("You have to pick a name to play Besieged!");
                                ClientGameEngine.Get().ExecuteOnUIThread(action);
                            }
                            else
                            {
                                ClientGameEngine.Get().ChangeState(LoadingState.Get());
                                CommandConnect commandConnect = new CommandConnect(se as string);
                                ClientGameEngine.Get().SendMessageToServer(commandConnect);
                            }
                        });
                    }
                }
                else
                {
                    MessageBox.Show(selected);
                }
            }
            catch (Exception)
            {
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

            try                 // single player menu item image
            {
                bimg = new BitmapImage(new Uri(UIComponentPath + "SinglePlayer.png", UriKind.RelativeOrAbsolute));
                m_SinglePlayerImage = new Image();
                m_SinglePlayerImage.Source = bimg;
                m_SinglePlayerImage.Width = bimg.PixelWidth;
                m_SinglePlayerImage.Height = bimg.PixelHeight;
                m_SinglePlayerImage.Name = "SinglePlayer";
                m_SinglePlayerImage.MouseEnter += MenuOptionHover;
                m_SinglePlayerImage.MouseLeave += MenuOptionHoverLost;
                m_SinglePlayerImage.MouseDown += MenuOptionMouseDown;
                m_SinglePlayerImage.MouseUp += MenuOptionMouseUp;
                
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : SinglePlayer.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try                 // multi player menu item image
            {
                m_MultiPlayerImage = new Image();
                bimg = new BitmapImage(new Uri(UIComponentPath + "MultiPlayer.png", UriKind.RelativeOrAbsolute));
                m_MultiPlayerImage.Source = bimg;
                m_MultiPlayerImage.Width = bimg.PixelWidth;
                m_MultiPlayerImage.Height = bimg.PixelHeight;
                m_MultiPlayerImage.Name = "MultiPlayer";
                m_MultiPlayerImage.MouseEnter += MenuOptionHover;
                m_MultiPlayerImage.MouseLeave += MenuOptionHoverLost;
                m_MultiPlayerImage.MouseDown += MenuOptionMouseDown;
                m_MultiPlayerImage.MouseUp += MenuOptionMouseUp;
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : MultiPlayer.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try                 // options menu item image
            {
                m_OptionsImage = new Image();
                bimg = new BitmapImage(new Uri(UIComponentPath + "Options.png", UriKind.RelativeOrAbsolute));
                m_OptionsImage.Source = bimg;
                m_OptionsImage.Width = bimg.PixelWidth;
                m_OptionsImage.Height = bimg.PixelHeight;
                m_OptionsImage.Name = "Options";
                m_OptionsImage.MouseEnter += MenuOptionHover;
                m_OptionsImage.MouseLeave += MenuOptionHoverLost;
                m_OptionsImage.MouseDown += MenuOptionMouseDown;
                m_OptionsImage.MouseUp += MenuOptionMouseUp;
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : Options.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try                 // quit menu item image
            {
                m_QuitImage = new Image();
                bimg = new BitmapImage(new Uri(UIComponentPath + "Quit.png", UriKind.RelativeOrAbsolute));
                m_QuitImage.Source = bimg;
                m_QuitImage.Width = bimg.PixelWidth;
                m_QuitImage.Height = bimg.PixelHeight;
                m_QuitImage.Name = "Quit";
                m_QuitImage.MouseEnter += MenuOptionHover;
                m_QuitImage.MouseLeave += MenuOptionHoverLost;
                m_QuitImage.MouseDown += MenuOptionMouseDown;
                m_QuitImage.MouseUp += MenuOptionMouseUp;
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : Quit.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Render()
        {
            try
            {
                Dimensions dimensions = ClientGameEngine.Get().ClientDimensions;
                m_MenuYOffset = dimensions.Height / 2;
                m_MenuXOffset = dimensions.Width * 0.65;

                ClientGameEngine.Get().Canvas.Background = m_BackgroundBrush;   // set the background brush

                Canvas.SetLeft(m_LogoImage, dimensions.Width * 0.05);           // resize and add the logo to the canvas
                Canvas.SetTop(m_LogoImage, dimensions.Height * 0.05);
                ClientGameEngine.Get().Canvas.Children.Add(m_LogoImage);

                Canvas.SetLeft(m_SinglePlayerImage, m_MenuXOffset);             // resize and add the single player menu item to the canvas
                Canvas.SetBottom(m_SinglePlayerImage, m_MenuYOffset);
                Canvas.SetZIndex(m_SinglePlayerImage, 100);
                ClientGameEngine.Get().Canvas.Children.Add(m_SinglePlayerImage);
                m_MenuYOffset -= m_SinglePlayerImage.Height * 1.5;


                Canvas.SetLeft(m_MultiPlayerImage, m_MenuXOffset);              // resize and add the multiplayer menu item to the canvas
                Canvas.SetBottom(m_MultiPlayerImage, m_MenuYOffset);
                Canvas.SetZIndex(m_MultiPlayerImage, 100);
                ClientGameEngine.Get().Canvas.Children.Add(m_MultiPlayerImage);
                m_MenuYOffset -= m_MultiPlayerImage.Height * 1.5;

                Canvas.SetLeft(m_OptionsImage, m_MenuXOffset);                  // resize and add the options menu item to the canvas
                Canvas.SetBottom(m_OptionsImage, m_MenuYOffset);
                Canvas.SetZIndex(m_OptionsImage, 100);
                ClientGameEngine.Get().Canvas.Children.Add(m_OptionsImage);
                m_MenuYOffset -= m_OptionsImage.Height * 1.5;


                Canvas.SetLeft(m_QuitImage, m_MenuXOffset);                     // resize and add the quit menu item to the canvas
                Canvas.SetBottom(m_QuitImage, m_MenuYOffset);
                Canvas.SetZIndex(m_QuitImage, 100);
                ClientGameEngine.Get().Canvas.Children.Add(m_QuitImage);
                m_MenuYOffset -= m_QuitImage.Height * 1.5;
            }
            catch (Exception ex)
            {
                // error handling
                throw ex;
            }
        }

        
    }
}
