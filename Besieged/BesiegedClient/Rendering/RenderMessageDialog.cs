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
        private static BitmapImage XImg = new BitmapImage(new Uri("resources\\UI\\CloseDialog.png", UriKind.RelativeOrAbsolute));
        private static BitmapImage clickedXImg = new BitmapImage(new Uri("resources\\UI\\ClickedCloseDialog.png", UriKind.RelativeOrAbsolute));

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
            Dimensions signDimensions = new Dimensions();
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
                signDimensions.Width= bimg.PixelWidth * 2;
                signDimensions.Height = bimg.PixelHeight * 2;
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
                textLabel.FontSize = 16.0;
                Canvas.SetLeft(textLabel, (dimensions.Width / 2) - (bimg.PixelWidth) + bimg.PixelWidth*0.10);
                Canvas.SetBottom(textLabel, dimensions.Height / 2 - bimg.PixelHeight ); //this should center this on the screen... I hope...
                Canvas.SetZIndex(textLabel, 1110); //range 1100 - 1200 for error dialogs
                DialogComponents.Add(textLabel);
            }
            catch (Exception)
            {
            }

            //Render Close
            try
            {
                img = new Image();
                img.Source = XImg;
                img.Width = XImg.PixelWidth;
                img.Height = XImg.PixelHeight;
                Canvas.SetLeft(img, dimensions.Width / 2 + signDimensions.Width * 0.30);
                Canvas.SetBottom(img, dimensions.Height / 2 +signDimensions.Height * 0.30); //this should center this on the screen... I hope...
                Canvas.SetZIndex(img, 1200); //range 1100 - 1200 for error dialogs

                img.MouseDown += (s, e) =>
                {
                    Image scopedImg = s as Image;
                    scopedImg.Source = clickedXImg;
                };

                img.MouseUp += (s, e) =>
                    {
                        Image scopedImg = s as Image;
                        scopedImg.Source = XImg;
                        ClearDialog();
                    };
                img.MouseLeave += (s, e) =>
                    {
                        Image scopedImg = s as Image;
                        if (scopedImg.Source == clickedXImg)
                            scopedImg.Source = XImg;
                    };

                DialogComponents.Add(img);
            }
            catch (Exception)
            {
            }

            foreach (UIElement obj in DialogComponents)
            {
                GlobalResources.GameWindow.Children.Add(obj);
            }
        }

        public static void RenderInput(string Display, EventHandler TextChangedHandler)
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
            Dimensions signDimensions = new Dimensions();
            //"Modal" Background
            try
            {
                Rectangle rect = new Rectangle(); //create the rectangle
                rect.Width = dimensions.Width;
                rect.Height = dimensions.Height;
                rect.Opacity = 0.7;
                Canvas.SetLeft(rect, 0);
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
                signDimensions.Width = bimg.PixelWidth * 2;
                signDimensions.Height = bimg.PixelHeight * 2;
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
                textLabel.Text = Display;
                textLabel.Width = (bimg.PixelWidth * 2) * 0.9;
                textLabel.Height = 50;
                textLabel.TextWrapping = TextWrapping.NoWrap;
                textLabel.FontFamily = new FontFamily("Papyrus");
                textLabel.FontSize = 20.0;
                textLabel.MouseUp += (s, e) =>
                {
                    TextChangedHandler("The Input Text", new EventArgs());
                    ClearDialog();
                };
                Canvas.SetLeft(textLabel, (dimensions.Width / 2) - (bimg.PixelWidth) + 10);
                Canvas.SetTop(textLabel, dimensions.Height / 2 - bimg.PixelHeight * 0.85); 
                Canvas.SetZIndex(textLabel, 1110); //range 1100 - 1200 for error dialogs
                DialogComponents.Add(textLabel);
            }
            catch (Exception)
            {
            }
            //text box
            TextBox tbox = new TextBox();
            try
            {
                tbox.Width = bimg.PixelWidth * 1.5;
                tbox.Height = 50;
                tbox.FontFamily = new FontFamily("Papyrus");
                tbox.FontSize = 20.0;
                tbox.Opacity = 0.6;
                Canvas.SetLeft(tbox, (dimensions.Width / 2) - (bimg.PixelWidth) + 25);
                Canvas.SetTop(tbox, dimensions.Height / 2 - tbox.Height + 15);
                Canvas.SetZIndex(tbox, 1110); //range 1100 - 1200 for error dialogs

                DialogComponents.Add(tbox);
            }
            catch (Exception)
            {
            }

            try
            {
                //Label
                TextBlock textLabel = new TextBlock();
                textLabel.Text = "OK";
                textLabel.Width = 50;
                textLabel.Height = 50;
                textLabel.TextWrapping = TextWrapping.Wrap;
                textLabel.FontFamily = new FontFamily("Papyrus");
                textLabel.FontSize = 18.0;
                textLabel.MouseEnter += (s, e) => {
                    textLabel.Foreground = RenderingUtilities.RedBrush;
                };
                textLabel.MouseLeave += (s, e) =>
                {
                    textLabel.Foreground = RenderingUtilities.BlackBrush;
                };
                textLabel.MouseUp += (s, e) =>
                {
                    TextChangedHandler(tbox.Text.Trim() != "" ? tbox.Text.Trim() : null, new EventArgs());
                    ClearDialog();
                }; 
                Canvas.SetLeft(textLabel, (dimensions.Width / 2) - (bimg.PixelWidth * 0.25) );
                Canvas.SetBottom(textLabel, dimensions.Height / 2 - bimg.PixelHeight); //this should center this on the screen... I hope...
                Canvas.SetZIndex(textLabel, 1110); //range 1100 - 1200 for error dialogs
                DialogComponents.Add(textLabel);
            }
            catch (Exception)
            {
            }
            try
            {
                //Label
                TextBlock textLabel = new TextBlock();
                textLabel.Text = "Cancel";
                textLabel.Width = 75;
                textLabel.Height = 50;
                textLabel.TextWrapping = TextWrapping.Wrap;
                textLabel.FontFamily = new FontFamily("Papyrus");
                textLabel.FontSize = 18.0;
                textLabel.MouseEnter += (s, e) =>
                {
                    textLabel.Foreground = RenderingUtilities.RedBrush;
                };
                textLabel.MouseLeave += (s, e) =>
                {
                    textLabel.Foreground = RenderingUtilities.BlackBrush;
                };
                textLabel.MouseUp += (s, e) =>
                {
                    TextChangedHandler(null, new EventArgs());
                    ClearDialog();
                }; 
                Canvas.SetLeft(textLabel, (dimensions.Width / 2) + (bimg.PixelWidth * 0.25));
                Canvas.SetBottom(textLabel, dimensions.Height / 2 - bimg.PixelHeight); //this should center this on the screen... I hope...
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