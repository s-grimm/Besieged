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

            
        }
    }
}
