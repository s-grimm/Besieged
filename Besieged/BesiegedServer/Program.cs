using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace BesiegedServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ServiceHost svcHost = null;
            try
            {
                svcHost = new ServiceHost(typeof(MessageService), new Uri("net.tcp://localhost:31337/BesiegedServer/"));
                svcHost.AddServiceEndpoint(typeof(IMessageService), new NetTcpBinding(), "BesiegedMessage");
                Console.Write("Service Started.\n> ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                while ( true )
                {
                    var serverMessage = Console.ReadLine().Trim().ToString().ToLower();
                    if (serverMessage == "exit") break;
                    else if(serverMessage == "?" || serverMessage == "help" || serverMessage == "\\help")
                    {
                        Console.WriteLine("Besieged Server Commands\nexit: Stops the server.");
                        Console.Write("> ");
                    }
                }
                if(svcHost != null)
                {
                    svcHost.Close();
                }
            }
        }
    }
}