using System;
using System.Collections.Concurrent;
using System.Data.SqlClient;
using System.IO;

namespace Utilities
{
    public static class ErrorLogger
    {
        private static BlockingCollection<Exception> Exceptions;

        static ErrorLogger()
        {
            Exceptions = new BlockingCollection<Exception>();
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

        public static void Write()
        {
            Exception ex = Exceptions.Take();

            using(FileStream log = File.Open(DateTime.Today.ToShortDateString().ToString() + "-errors.log", FileMode.OpenOrCreate))
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