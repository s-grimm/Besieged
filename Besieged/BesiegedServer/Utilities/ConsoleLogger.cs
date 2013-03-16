using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class ConsoleLogger
    {
        private static BlockingCollection<string> Messages;

        static ConsoleLogger()
        {
            Messages = new BlockingCollection<string>();
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    ConsoleLogger.Write();
                }
            }, TaskCreationOptions.LongRunning);
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

        private static void Write()
        {
            Console.WriteLine(Messages.Take());
            Console.WriteLine();
        }
    }
}