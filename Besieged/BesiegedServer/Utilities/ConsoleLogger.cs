using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BesiegedServer.Utilities
{
    public static class ConsoleLogger
    {
        private static BlockingCollection<string> Messages;

        static ConsoleLogger()
        {
            Messages = new BlockingCollection<string>();
        }

        public static bool Push(string ex)
        {
            try
            {
                Messages.Add(ex);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void Write()
        {
            Console.WriteLine(Messages.Take());
        }
    }
}