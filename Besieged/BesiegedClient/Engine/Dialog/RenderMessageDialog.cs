using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BesiegedClient.Engine.Dialog
{
    public static class RenderMessageDialog
    {
        private static List<UIElement> m_DialogComponents;
        private static Dimensions m_Dimensions;
        private static BitmapImage m_XImage = new BitmapImage(new Uri("resources\\UI\\CloseDialog.png", UriKind.RelativeOrAbsolute));
        private static BitmapImage m_ClickedXImage = new BitmapImage(new Uri("resources\\UI\\ClickedCloseDialog.png", UriKind.RelativeOrAbsolute));

        public static void RenderMessage(string message)
        {
            ClearDialog();
            m_DialogComponents = new List<UIElement>();
            foreach (UIElement el in ClientGameEngine.Get().Canvas.Children)
            {
                el.IsEnabled = false;
            }
            m_Dimensions = ClientGameEngine.Get().ClientDimensions;
            string UIComponentPath = "resources\\UI\\";
            Image img;
            BitmapImage bimg = new BitmapImage();
            Dimensions signDimensions = new Dimensions();

            //"Modal" Background
            try
            {
                Rectangle rect = new Rectangle(); //create the rectangle
                rect.Width = m_Dimensions.Width;
                rect.Height = m_Dimensions.Height;
                rect.Opacity = 0.7;
                Canvas.SetLeft(rect, 0);
                Canvas.SetTop(rect, 0);
                rect.Fill = Utilities.Rendering.BlackBrush;
                m_DialogComponents.Add(rect);
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
                Canvas.SetLeft(img, m_Dimensions.Width / 2.0 - img.Width / 2);
                Canvas.SetBottom(img, m_Dimensions.Height / 2.0 - img.Height / 2); //this should center this on the screen... I hope...
                Canvas.SetZIndex(img, 1100); //range 1100 - 1200 for error dialogs
                m_DialogComponents.Add(img);
            }
            catch (Exception)
            {
            }
            try
            {
                //Label
                TextBlock textLabel = new TextBlock();
                textLabel.Text = message;
                textLabel.Width = bimg.PixelWidth * 2 * 0.8;
                textLabel.Height = bimg.PixelHeight * 2 * 0.70;
                textLabel.TextWrapping = TextWrapping.Wrap;
                textLabel.FontFamily = new FontFamily("Papyrus");
                textLabel.FontSize = 16.0;
                Canvas.SetLeft(textLabel, (m_Dimensions.Width / 2) - (bimg.PixelWidth) + bimg.PixelWidth * 0.10);
                Canvas.SetBottom(textLabel, m_Dimensions.Height / 2 - bimg.PixelHeight); //this should center this on the screen... I hope...
                Canvas.SetZIndex(textLabel, 1110); //range 1100 - 1200 for error dialogs
                m_DialogComponents.Add(textLabel);
            }
            catch (Exception)
            {
            }

            //Render Close
            try
            {
                img = new Image();
                img.Source = m_XImage;
                img.Width = m_XImage.PixelWidth;
                img.Height = m_XImage.PixelHeight;
                Canvas.SetLeft(img, m_Dimensions.Width / 2 + signDimensions.Width * 0.30);
                Canvas.SetBottom(img, m_Dimensions.Height / 2 + signDimensions.Height * 0.30); //this should center this on the screen... I hope...
                Canvas.SetZIndex(img, 1200); //range 1100 - 1200 for error dialogs

                img.MouseDown += (s, e) =>
                {
                    Image scopedImg = s as Image;
                    scopedImg.Source = m_ClickedXImage;
                };

                img.MouseUp += (s, e) =>
                    {
                        Image scopedImg = s as Image;
                        scopedImg.Source = m_XImage;
                        ClearDialog();
                    };
                img.MouseLeave += (s, e) =>
                    {
                        Image scopedImg = s as Image;
                        if (scopedImg.Source == m_ClickedXImage)
                            scopedImg.Source = m_XImage;
                    };

                m_DialogComponents.Add(img);
            }
            catch (Exception)
            {
            }

            foreach (UIElement obj in m_DialogComponents)
            {
                ClientGameEngine.Get().Canvas.Children.Add(obj);
            }
        }

        public static void RenderInput(string display, EventHandler textChangedHandler)
        {
            ClearDialog();
            m_DialogComponents = new List<UIElement>();
            foreach (UIElement el in ClientGameEngine.Get().Canvas.Children)
            {
                el.IsEnabled = false;
            }
            m_Dimensions = ClientGameEngine.Get().ClientDimensions;
            string UIComponentPath = "resources\\UI\\";
            Image img;
            BitmapImage bimg = new BitmapImage();
            Dimensions signDimensions = new Dimensions();

            //"Modal" Background
            try
            {
                Rectangle rect = new Rectangle(); //create the rectangle
                rect.Width = m_Dimensions.Width;
                rect.Height = m_Dimensions.Height;
                rect.Opacity = 0.7;
                Canvas.SetLeft(rect, 0);
                Canvas.SetTop(rect, 0);
                rect.Fill = Utilities.Rendering.BlackBrush;
                m_DialogComponents.Add(rect);
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
                Canvas.SetLeft(img, m_Dimensions.Width / 2 - img.Width / 2);
                Canvas.SetBottom(img, m_Dimensions.Height / 2 - img.Height / 2); //this should center this on the screen... I hope...
                Canvas.SetZIndex(img, 1100); //range 1100 - 1200 for error dialogs
                m_DialogComponents.Add(img);
            }
            catch (Exception)
            {
            }
            try
            {
                //Label
                TextBlock textLabel = new TextBlock();
                textLabel.Text = display;
                textLabel.Width = (bimg.PixelWidth * 2) * 0.9;
                textLabel.Height = 50;
                textLabel.TextWrapping = TextWrapping.NoWrap;
                textLabel.FontFamily = new FontFamily("Papyrus");
                textLabel.FontSize = 20.0;
                textLabel.MouseUp += (s, e) =>
                {
                    textChangedHandler("The Input Text", new EventArgs());
                    ClearDialog();
                };
                Canvas.SetLeft(textLabel, (m_Dimensions.Width / 2) - (bimg.PixelWidth) + 10);
                Canvas.SetTop(textLabel, m_Dimensions.Height / 2 - bimg.PixelHeight * 0.85);
                Canvas.SetZIndex(textLabel, 1110); //range 1100 - 1200 for error dialogs
                m_DialogComponents.Add(textLabel);
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
                Canvas.SetLeft(tbox, (m_Dimensions.Width / 2) - (bimg.PixelWidth) + 25);
                Canvas.SetTop(tbox, m_Dimensions.Height / 2 - tbox.Height + 15);
                Canvas.SetZIndex(tbox, 1110); //range 1100 - 1200 for error dialogs

                m_DialogComponents.Add(tbox);
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
                textLabel.MouseEnter += (s, e) =>
                {
                    textLabel.Foreground = Utilities.Rendering.RedBrush;
                };
                textLabel.MouseLeave += (s, e) =>
                {
                    textLabel.Foreground = Utilities.Rendering.BlackBrush;
                };
                textLabel.MouseUp += (s, e) =>
                {
                    textChangedHandler(tbox.Text.Trim() != "" ? tbox.Text.Trim() : null, new EventArgs());
                    ClearDialog();
                };
                Canvas.SetLeft(textLabel, (m_Dimensions.Width / 2) - (bimg.PixelWidth * 0.25));
                Canvas.SetBottom(textLabel, m_Dimensions.Height / 2 - bimg.PixelHeight); //this should center this on the screen... I hope...
                Canvas.SetZIndex(textLabel, 1110); //range 1100 - 1200 for error dialogs
                m_DialogComponents.Add(textLabel);
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
                    textLabel.Foreground = Utilities.Rendering.RedBrush;
                };
                textLabel.MouseLeave += (s, e) =>
                {
                    textLabel.Foreground = Utilities.Rendering.BlackBrush;
                };
                textLabel.MouseUp += (s, e) =>
                {
                    textChangedHandler(null, new EventArgs());
                    ClearDialog();
                };
                Canvas.SetLeft(textLabel, (m_Dimensions.Width / 2) + (bimg.PixelWidth * 0.25));
                Canvas.SetBottom(textLabel, m_Dimensions.Height / 2 - bimg.PixelHeight); //this should center this on the screen... I hope...
                Canvas.SetZIndex(textLabel, 1110); //range 1100 - 1200 for error dialogs
                m_DialogComponents.Add(textLabel);
            }
            catch (Exception)
            {
            }

            foreach (UIElement obj in m_DialogComponents)
            {
                ClientGameEngine.Get().Canvas.Children.Add(obj);
            }
        }

        public static void RenderButtons(string display, string[] buttonText, EventHandler textChangedHandler)
        {
            ClearDialog();
            m_DialogComponents = new List<UIElement>();
            foreach (UIElement el in ClientGameEngine.Get().Canvas.Children)
            {
                el.IsEnabled = false;
            }
            m_Dimensions = ClientGameEngine.Get().ClientDimensions;
            const string UIComponentPath = "resources\\UI\\";
            BitmapImage bimg = new BitmapImage();
            Dimensions signDimensions = new Dimensions();

            //"Modal" Background

            Rectangle rect = new Rectangle { Width = m_Dimensions.Width, Height = m_Dimensions.Height, Opacity = 0.7 }; //create the rectangle
            Canvas.SetLeft(rect, 0);
            Canvas.SetTop(rect, 0);
            rect.Fill = Utilities.Rendering.BlackBrush;
            m_DialogComponents.Add(rect);

            Image img = new Image();
            bimg = new BitmapImage(new Uri(UIComponentPath + "WoodenSign.png", UriKind.RelativeOrAbsolute));
            img.Source = bimg;
            img.Width = bimg.PixelWidth * 2;
            img.Height = bimg.PixelHeight * 2;
            signDimensions.Width = bimg.PixelWidth * 2;
            signDimensions.Height = bimg.PixelHeight * 2;
            Canvas.SetLeft(img, m_Dimensions.Width / 2.0 - img.Width / 2);
            Canvas.SetBottom(img, m_Dimensions.Height / 2.0 - img.Height / 2); //this should center this on the screen... I hope...
            Canvas.SetZIndex(img, 1100); //range 1100 - 1200 for error dialogs
            m_DialogComponents.Add(img);

            //Label
            TextBlock textLabel = new TextBlock
                {
                    Text = display,
                    Width = bimg.PixelWidth * 2 * 0.8,
                    Height = bimg.PixelHeight * 2 * 0.70,
                    TextWrapping = TextWrapping.Wrap,
                    FontFamily = new FontFamily("Papyrus"),
                    FontSize = 16.0
                };
            Canvas.SetLeft(textLabel, (m_Dimensions.Width / 2.0) - (bimg.PixelWidth) + bimg.PixelWidth * 0.10);
            Canvas.SetBottom(textLabel, m_Dimensions.Height / 2.0 - bimg.PixelHeight); //this should center this on the screen... I hope...
            Canvas.SetZIndex(textLabel, 1110); //range 1100 - 1200 for error dialogs
            m_DialogComponents.Add(textLabel);

            //Label
            TextBlock btn1Label = new TextBlock
                {
                    Text = buttonText[0],
                    Width = 50,
                    Height = 50,
                    TextWrapping = TextWrapping.Wrap,
                    FontFamily = new FontFamily("Papyrus"),
                    FontSize = 18.0
                };
            btn1Label.MouseEnter += (s, e) =>
            {
                btn1Label.Foreground = Utilities.Rendering.RedBrush;
            };
            btn1Label.MouseLeave += (s, e) =>
            {
                btn1Label.Foreground = Utilities.Rendering.BlackBrush;
            };
            btn1Label.MouseUp += (s, e) =>
            {
                textChangedHandler(buttonText[0], new EventArgs());
                ClearDialog();
            };
            Canvas.SetLeft(btn1Label, (m_Dimensions.Width / 2.0) - (bimg.PixelWidth * 0.25));
            Canvas.SetBottom(btn1Label, m_Dimensions.Height / 2 - bimg.PixelHeight); //this should center this on the screen... I hope...
            Canvas.SetZIndex(btn1Label, 1110); //range 1100 - 1200 for error dialogs
            m_DialogComponents.Add(btn1Label);

            //Label
            TextBlock btn2Label = new TextBlock
                {
                    Text = buttonText[1],
                    Width = 75,
                    Height = 50,
                    TextWrapping = TextWrapping.Wrap,
                    FontFamily = new FontFamily("Papyrus"),
                    FontSize = 18.0
                };
            btn2Label.MouseEnter += (s, e) =>
            {
                btn2Label.Foreground = Utilities.Rendering.RedBrush;
            };
            btn2Label.MouseLeave += (s, e) =>
            {
                btn2Label.Foreground = Utilities.Rendering.BlackBrush;
            };
            btn2Label.MouseUp += (s, e) =>
            {
                textChangedHandler(buttonText[1], new EventArgs());
                ClearDialog();
            };
            Canvas.SetLeft(btn2Label, (m_Dimensions.Width / 2.0) + (bimg.PixelWidth * 0.25));
            Canvas.SetBottom(btn2Label, m_Dimensions.Height / 2.0 - bimg.PixelHeight); //this should center this on the screen... I hope...
            Canvas.SetZIndex(btn2Label, 1110); //range 1100 - 1200 for error dialogs
            m_DialogComponents.Add(btn2Label);

            foreach (UIElement obj in m_DialogComponents)
            {
                ClientGameEngine.Get().Canvas.Children.Add(obj);
            }
        }

        public static void ClearDialog()
        {
            if (m_DialogComponents == null) return;
            foreach (UIElement obj in m_DialogComponents.Where(obj => ClientGameEngine.Get().Canvas.Children.Contains(obj)))
            {
                ClientGameEngine.Get().Canvas.Children.Remove(obj);
            }
            foreach (UIElement el in ClientGameEngine.Get().Canvas.Children)
            {
                el.IsEnabled = true;
            }
            m_DialogComponents.Clear();
        }
    }
}