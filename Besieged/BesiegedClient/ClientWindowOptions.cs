using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BesiegedClient
{
    public static class ClientWindowOptions
    {
        public static Dimensions WindowDimensions;

        static ClientWindowOptions()
        {
            WindowDimensions = new Dimensions() { Width = 800, Height = 600 };
        }
    }
}
