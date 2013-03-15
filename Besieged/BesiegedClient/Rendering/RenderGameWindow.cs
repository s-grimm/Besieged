using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
                UIComponentPath = @"resources\UI\4x3\";
            }
            else
            {
                //16:9
                UIComponentPath = @"resources\UI\16x9\";
            }
            
        }
    }
}
