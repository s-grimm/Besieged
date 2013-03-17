using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BesiegedClient.Rendering
{
    public static class RenderMessageDialog
    {
        private static List<UIElement> DialogComponents;
        private static Dimensions dimensions;

        public static void RenderMessage(string message)
        {
            ClearDialog();
            DialogComponents = new List<UIElement>();
            foreach (UIElement el in GlobalResources.GameWindow.Children)
            {
                el.IsEnabled = false;
            }
            dimensions = new Dimensions() { Width = (int)GlobalResources.GameWindow.Width, Height = (int)GlobalResources.GameWindow.Height };
            string UIComponentPath = "resources\\UI\\";
            Image img;
            BitmapImage bimg = new BitmapImage();

            //"Modal" Background
            try
            {
                Rectangle rect = new Rectangle(); //create the rectangle
                rect.Width = dimensions.Width;
                rect.Height = dimensions.Height;
                rect.Opacity = 0.7;
                Canvas.SetLeft(rect,0);
                Canvas.SetTop(rect, 0);
                rect.Fill = RenderingUtilities.BlackBrush;
                DialogComponents.Add(rect);
            }
            catch (Exception)
            {
            }
            try
            {
                img = new Image();
                bimg = new BitmapImage(new Uri(UIComponentPath + "WoodenSign.png", UriKind.RelativeOrAbsolute));
                img.Source = bimg;
                img.Width = bimg.PixelWidth * 2;
                img.Height = bimg.PixelHeight * 2;
                Canvas.SetLeft(img, dimensions.Width / 2 - img.Width / 2);
                Canvas.SetBottom(img, dimensions.Height / 2 - img.Height / 2); //this should center this on the screen... I hope...
                Canvas.SetZIndex(img, 1100); //range 1100 - 1200 for error dialogs
                DialogComponents.Add(img);
            }
            catch (Exception)
            {
            }
            try
            {
                //Label
                TextBlock textLabel = new TextBlock();
                textLabel.Text = message;
                textLabel.Width = bimg.PixelWidth*2 * 0.8;
                textLabel.Height = bimg.PixelHeight*2 * 0.70;
                textLabel.TextWrapping = TextWrapping.Wrap;
                textLabel.FontFamily = new FontFamily("Papyrus");
                textLabel.FontSize = 18.0;
                Canvas.SetLeft(textLabel, dimensions.Width / 2 - textLabel.Width / 2);
                Canvas.SetBottom(textLabel, dimensions.Height / 2 - textLabel.Height / 2); //this should center this on the screen... I hope...
                Canvas.SetZIndex(textLabel, 1110); //range 1100 - 1200 for error dialogs
                DialogComponents.Add(textLabel);
            }
            catch (Exception)
            {
            }

            foreach (UIElement obj in DialogComponents)
            {
                GlobalResources.GameWindow.Children.Add(obj);
            }
        }

        public static void ClearDialog()
        {
            if (DialogComponents == null) return;
            foreach (UIElement obj in DialogComponents)
            {
                if (GlobalResources.GameWindow.Children.Contains(obj))
                {
                    GlobalResources.GameWindow.Children.Remove(obj);
                }
            }
            foreach (UIElement el in GlobalResources.GameWindow.Children)
            {
                el.IsEnabled = true;
            }
            DialogComponents.Clear();
        }
    }
}