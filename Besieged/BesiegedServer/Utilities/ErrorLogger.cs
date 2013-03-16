using System;
using System.Collections.Concurrent;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace Utilities
{
    public static class ErrorLogger
    {
        private static BlockingCollection<Exception> Exceptions;

        static ErrorLogger()
        {
            Exceptions = new BlockingCollection<Exception>();
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    ErrorLogger.Write();
                }
            }, TaskCreationOptions.LongRunning);
        }

        public static bool Push(Exception ex)
        {
            try
            {
                Exceptions.Add(ex);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void Write()
        {
            Exception ex = Exceptions.Take();

            string path = DateTime.Today.ToShortDateString().ToString().Replace('/', '-');

            using (FileStream log = File.Open(path + "-errors.log", FileMode.Append, FileAccess.Write))
            {
                using(StreamWriter sw = new StreamWriter(log))
                {
                    string str = DateTime.Now.ToString() + " | " + ex.GetType().Name + " | " + ex.Message;

                    if (ex.InnerException != null)
                        str += "|" + ex.InnerException;

                    str += "|" + ex.StackTrace;
                    sw.WriteLine(str);
                }
            }
        }
    }
}