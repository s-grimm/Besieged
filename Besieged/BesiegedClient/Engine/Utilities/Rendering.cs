using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BesiegedClient.Engine.Utilities
{
    public static class Rendering
    {
        public static SolidColorBrush BlackBrush = new SolidColorBrush(Colors.Black);
        public static SolidColorBrush RedBrush = new SolidColorBrush(Colors.Red);
        public static SolidColorBrush GreenBrush = new SolidColorBrush(Colors.Green);
        public static SolidColorBrush BlueBrush = new SolidColorBrush(Colors.Blue);
        public static SolidColorBrush SeaGreenBrush = new SolidColorBrush(Colors.LightSeaGreen);
        public static SolidColorBrush GrayBrush = new SolidColorBrush(Colors.Gray);

        public static Dictionary<string, ImageBrush> TileBrushes;

        static Rendering()
        {
            TileBrushes = new Dictionary<string, ImageBrush>();
        }

        public static ImageBrush GetBrushForTile(string TilePNGName)
        {
            try
            {
                if (!TileBrushes.ContainsKey(TilePNGName))
                {
                    BitmapImage bimg = new BitmapImage(new Uri(@"resources\sprites\tiles\" + TilePNGName, UriKind.RelativeOrAbsolute));
                    TileBrushes.Add(TilePNGName, new ImageBrush(bimg));
                }
                return TileBrushes[TilePNGName];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Image GetImageForUnit(string ImagePNGName)
        {
            try
            {

                    BitmapImage bimg = new BitmapImage(new Uri(@"resources\sprites\units\" + ImagePNGName, UriKind.RelativeOrAbsolute));
                    Image res = new Image() {
                    Source = bimg,
                    Width = bimg.PixelWidth,
                    Height = bimg.PixelHeight
                    };
                    return res;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}