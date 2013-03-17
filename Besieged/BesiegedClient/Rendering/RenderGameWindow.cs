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
    public static class RenderGameWindow
    {
        private static Dimensions dimensions;
        public static void RenderUI (Canvas canvas)
        {
            dimensions = new Dimensions() { Width = (int)canvas.Width, Height = (int)canvas.Height };
            canvas.Children.Clear();
            canvas.Background = RenderingUtilities.SeaGreenBrush;

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
            Image img = new Image();
            BitmapImage bimg;
            try
            {
                bimg = new BitmapImage(new Uri(UIComponentPath + "BottomLeftCorner.png", UriKind.RelativeOrAbsolute));
                img.Source = bimg;
                img.Width = bimg.PixelWidth;
                img.Height = bimg.PixelHeight;
                Canvas.SetLeft(img, 0);
                Canvas.SetBottom(img, 0);
                Canvas.SetZIndex(img, 999);
                canvas.Children.Add(img);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : BottomLeftCorner.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                img = new Image();
                bimg = new BitmapImage(new Uri(UIComponentPath + "BottomRightCorner.png", UriKind.RelativeOrAbsolute));
                img.Source = bimg;
                img.Width = bimg.PixelWidth;
                img.Height = bimg.PixelHeight;
                Canvas.SetRight(img, 0);
                Canvas.SetBottom(img, 0);
                Canvas.SetZIndex(img, 999);
                canvas.Children.Add(img);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : BottomRightCorner.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                img = new Image();
                bimg = new BitmapImage(new Uri(UIComponentPath + "TopBar.png", UriKind.RelativeOrAbsolute));
                img.Source = bimg;
                img.Width = bimg.PixelWidth;
                img.Height = bimg.PixelHeight;
                //img.RenderTransform = new TranslateTransform(-(img.Width - canvas.Width),0);
                Canvas.SetTop(img, 0);
                Canvas.SetZIndex(img, 999);
                canvas.Children.Add(img);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading UI Component : TopBar.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
