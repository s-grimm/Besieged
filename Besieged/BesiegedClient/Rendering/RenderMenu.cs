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
    public static class RenderMenu
    {
        private static Dimensions dimensions;
        private static double menuYOffset;
        private static double menuXOffset;
        public static void MenuOptionHover(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                Image img = (Image)sender;
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
                Image img = (Image)sender;
                Canvas.SetLeft(img, menuXOffset);
            }
            catch (Exception)
            {
            }
        }
        

        public static void RenderMainMenu(Canvas canvas)
        {
            dimensions = new Dimensions() { Width = (int)canvas.Width, Height = (int)canvas.Height };
            menuYOffset = dimensions.Height / 2;
            menuXOffset = dimensions.Width * 0.65;
            canvas.Children.Clear();

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
                canvas.Background = new ImageBrush(bimg);              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading UI Component : MainMenuBackground.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
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
                canvas.Children.Add(img);
                menuYOffset -= img.Height * 1.5;
            }
            catch (Exception ex)
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
                canvas.Children.Add(img);
                menuYOffset -= img.Height * 1.5;
            }
            catch (Exception ex)
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
                canvas.Children.Add(img);
                menuYOffset -= img.Height * 1.5;
            }
            catch (Exception ex)
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
                canvas.Children.Add(img);
                menuYOffset -= img.Height * 1.5;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading UI Component : Quit.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void RenderOptionsMenu(Canvas canvas)
        {
            dimensions = new Dimensions() { Width = (int)canvas.Width, Height = (int)canvas.Height };
        }
    }
}
