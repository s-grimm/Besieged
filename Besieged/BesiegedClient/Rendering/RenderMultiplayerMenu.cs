using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BesiegedClient.Rendering
{
    public static class RenderMultiplayerMenu
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
                if (selected == "MainMenu")
                {
                    RenderMenu.RenderMainMenu();
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


        public static void RenderGameLobby()
        {
            dimensions = new Dimensions() { Width = (int)GlobalVariables.GameWindow.Width, Height = (int)GlobalVariables.GameWindow.Height };
            menuYOffset = dimensions.Height / 2;
            menuXOffset = dimensions.Width * 0.65;
            GlobalVariables.GameWindow.Children.Clear();

            double aspectRatio = Math.Round((double)dimensions.Width / (double)dimensions.Height, 2, MidpointRounding.AwayFromZero);

            string UIComponentPath = "resources\\UI\\Menu\\MultiplayerMenu\\";
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
                bimg = new BitmapImage(new Uri(UIComponentPath + ratioPath + "Background.png", UriKind.RelativeOrAbsolute));
                img.Source = bimg;
                img.Width = bimg.PixelWidth;
                img.Height = bimg.PixelHeight;
                GlobalVariables.GameWindow.Background = new ImageBrush(bimg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading UI Component : Background.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            /***************************List Box of Games************************************/
            try
            {
                ListBox lbCurrentGames = new ListBox();
                lbCurrentGames.Width = dimensions.Width / 2;
                lbCurrentGames.Height = dimensions.Height / 2;
                lbCurrentGames.Opacity = 0.75;
                //positioning
                Canvas.SetLeft(lbCurrentGames,dimensions.Width / 8);
                Canvas.SetBottom(lbCurrentGames, dimensions.Height / 4);
                //add to canvas
                GlobalVariables.GameWindow.Children.Add(lbCurrentGames);
            }
            catch(Exception ex)
            {
                
            }
            //Draw Menu Options
            try
            {
                img = new Image();
                bimg = new BitmapImage(new Uri(UIComponentPath + "JoinGame.png", UriKind.RelativeOrAbsolute));
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
                img.Name = "JoinGame";
                GlobalVariables.GameWindow.Children.Add(img);
                menuYOffset -= img.Height * 1.5;
            }
            catch(Exception ex)
            {
                
            }
            try
            {
                img = new Image();
                bimg = new BitmapImage(new Uri(UIComponentPath + "CreateGame.png", UriKind.RelativeOrAbsolute));
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
                img.Name = "CreateGame";
                GlobalVariables.GameWindow.Children.Add(img);
                menuYOffset -= img.Height * 1.5;
            }
            catch (Exception ex)
            {

            }
            try
            {
                img = new Image();
                bimg = new BitmapImage(new Uri(UIComponentPath + "MainMenu.png", UriKind.RelativeOrAbsolute));
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
                img.Name = "MainMenu";
                GlobalVariables.GameWindow.Children.Add(img);
                menuYOffset -= img.Height * 1.5;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
