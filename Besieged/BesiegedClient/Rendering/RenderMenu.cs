using Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Framework.Utilities.Xml;

namespace BesiegedClient.Rendering
{
    public static class RenderMenu
    {
        private static Dimensions dimensions;
        private static double menuYOffset;
        private static double menuXOffset;
        private static object mousedownRef;

        #region Handlers
        
        public static void MenuOptionHover(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                Image img = sender as Image;
                Canvas.SetLeft(img, menuXOffset + 50);
            }
            catch (Exception)
            {
            }
        }
        public static void MenuOptionHoverLost(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                Image img = sender as Image;
                Canvas.SetLeft(img, menuXOffset);
            }
            catch (Exception)
            {
            }
        }
        public static void MenuOptionMouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                mousedownRef = sender;
            }
            catch (Exception)
            {
            }
        }
        public static void MenuOptionMouseUp(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                if (mousedownRef != sender) return;
                Image img = sender as Image;
                string selected = img.Name;
                if(selected == "Quit")
                {
                    Application.Current.MainWindow.Close();
                }
                else if(selected == "MultiPlayer")
                {
                    // Subscribe in a separate thread to preserve the UI thread
                    Task.Factory.StartNew(() =>
                    {
                        CommandConnect commandConnect = new CommandConnect(ClientSettings.Default.Alias);
                        try
                        {
                            GlobalResources.BesiegedServer.SendCommand(commandConnect.ToXml());
                        }
                        catch (Exception)
                        {
                            Task.Factory.StartNew(() =>
                            {
                                RenderMenu.RenderMainMenu();
                                RenderMessageDialog.RenderMessage("There was an error connecting to the server!");
                                GlobalResources.MenuStateChanged = null; //remove this
                            }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
                        }
                    });
                    if (GlobalResources.m_IsServerConnectionEstablished)
                    {
                        RenderMultiplayerMenu.RenderGameLobby();
                    }
                    else
                    {
                        GlobalResources.MenuStateChanged += (leSender, leArgs) => {
                            //make sure the UI thread calls this!
                            Task.Factory.StartNew(() => {
                                RenderMultiplayerMenu.RenderGameLobby();
                                GlobalResources.MenuStateChanged = null; //remove this
                            }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
                        };
                        RenderMultiplayerMenu.RenderLoadingScreen();
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

        #endregion Handlers

        public static void RenderMainMenu()
        {
            dimensions = new Dimensions() { Width = (int)GlobalResources.GameWindow.Width, Height = (int)GlobalResources.GameWindow.Height };
            menuYOffset = dimensions.Height / 2;
            menuXOffset = dimensions.Width * 0.65;
            GlobalResources.GameWindow.Children.Clear();

            double aspectRatio = Math.Round((double)dimensions.Width / (double)dimensions.Height, 2, MidpointRounding.AwayFromZero);

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
            Image img = new Image();
            BitmapImage bimg;
            try
            {
                bimg = new BitmapImage(new Uri(UIComponentPath + ratioPath + "MainMenuBackground.png", UriKind.RelativeOrAbsolute));
                img.Source = bimg;
                img.Width = bimg.PixelWidth;
                img.Height = bimg.PixelHeight;
                GlobalResources.GameWindow.Background = new ImageBrush(bimg);              
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : MainMenuBackground.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //Title
            try
            {
                bimg = new BitmapImage(new Uri(UIComponentPath + "Logo.png", UriKind.RelativeOrAbsolute));
                img = new Image();
                img.Source = bimg;
                img.Width = bimg.PixelWidth;
                img.Height = bimg.PixelHeight;
                Canvas.SetLeft(img, dimensions.Width * 0.05);
                Canvas.SetTop(img, dimensions.Height * 0.05);
                GlobalResources.GameWindow.Children.Add(img);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : Logo.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            /**********************************************Draw and Link menu options******************************************************/
            //Single Player
            try
            {
                img = new Image();
                bimg = new BitmapImage(new Uri(UIComponentPath + "SinglePlayer.png", UriKind.RelativeOrAbsolute));
                img.Source = bimg;
                img.Width = bimg.PixelWidth;
                img.Height = bimg.PixelHeight;
                Canvas.SetLeft(img, menuXOffset);
                Canvas.SetBottom(img, menuYOffset);
                Canvas.SetZIndex(img, 100);
                img.MouseEnter += MenuOptionHover;
                img.MouseLeave += MenuOptionHoverLost;
                img.MouseDown += MenuOptionMouseDown;
                img.MouseUp += MenuOptionMouseUp;
                img.Name = "SinglePlayer";
                GlobalResources.GameWindow.Children.Add(img);
                menuYOffset -= img.Height * 1.5;
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : SinglePlayer.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //multi player
            try
            {
                img = new Image();
                bimg = new BitmapImage(new Uri(UIComponentPath + "MultiPlayer.png", UriKind.RelativeOrAbsolute));
                img.Source = bimg;
                img.Width = bimg.PixelWidth;
                img.Height = bimg.PixelHeight;
                Canvas.SetLeft(img, menuXOffset);
                Canvas.SetBottom(img, menuYOffset);
                Canvas.SetZIndex(img, 100);
                img.MouseEnter += MenuOptionHover;
                img.MouseLeave += MenuOptionHoverLost;
                img.MouseDown += MenuOptionMouseDown;
                img.MouseUp += MenuOptionMouseUp;
                img.Name = "MultiPlayer";
                GlobalResources.GameWindow.Children.Add(img);
                menuYOffset -= img.Height * 1.5;
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : MultiPlayer.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //options
            try
            {
                img = new Image();
                bimg = new BitmapImage(new Uri(UIComponentPath + "Options.png", UriKind.RelativeOrAbsolute));
                img.Source = bimg;
                img.Width = bimg.PixelWidth;
                img.Height = bimg.PixelHeight;
                Canvas.SetLeft(img, menuXOffset);
                Canvas.SetBottom(img, menuYOffset);
                Canvas.SetZIndex(img, 100);
                img.MouseEnter += MenuOptionHover;
                img.MouseLeave += MenuOptionHoverLost;
                img.MouseDown += MenuOptionMouseDown;
                img.MouseUp += MenuOptionMouseUp;
                img.Name = "Options";
                GlobalResources.GameWindow.Children.Add(img);
                menuYOffset -= img.Height * 1.5;
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : Options.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //Quit
            try
            {
                img = new Image();
                bimg = new BitmapImage(new Uri(UIComponentPath + "Quit.png", UriKind.RelativeOrAbsolute));
                img.Source = bimg;
                img.Width = bimg.PixelWidth;
                img.Height = bimg.PixelHeight;
                Canvas.SetLeft(img, menuXOffset);
                Canvas.SetBottom(img, menuYOffset);
                Canvas.SetZIndex(img, 100);
                img.MouseEnter += MenuOptionHover;
                img.MouseLeave += MenuOptionHoverLost;
                img.MouseDown += MenuOptionMouseDown;
                img.MouseUp += MenuOptionMouseUp;
                img.Name = "Quit";
                GlobalResources.GameWindow.Children.Add(img);
                menuYOffset -= img.Height * 1.5;
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : Quit.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
