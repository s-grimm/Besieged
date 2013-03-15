using Framework.Map;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using Utilities;
using Framework.Utilities.Xml;
using BesiegedServer.Maps;
namespace BesiegedServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ServiceHost svcHost = null;
            try
            {
                svcHost = new ServiceHost(typeof(BesiegedServer), new Uri("http://localhost:31337/BesiegedServer/"));
                svcHost.AddServiceEndpoint(typeof(Framework.ServiceContracts.IBesiegedServer), new WSDualHttpBinding(), "BesiegedMessage");
                svcHost.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = true });
                svcHost.Open();
                ConsoleLogger.Push("Service Started.");
                MapUtilities.SaveToFile(new GameMap().ToXml());
            }
            catch (Exception ex)
            {
                ConsoleLogger.Push(ex.Message);
            }
            finally
            {
                while (true)
                {
                    var serverMessage = Console.ReadLine().Trim().ToString().ToLower();
                    if (serverMessage == "exit") break;
                    else if (serverMessage == "?" || serverMessage == "help" || serverMessage == "\\help")
                    {
                        ConsoleLogger.Push("Besieged Server Commands\nexit: Stops the server.");
                    }
                    else if (serverMessage == "list")
                    {
                        //var clients = GameObject.GetClients();
                        //if (clients.Count <= 0)
                        //{
                        //    Console.Write("> ");
                        //    continue;
                        //}
                        //Console.WriteLine("Connected Clients");
                        //foreach (BesiegedLogic.Objects.Client client in clients)
                        //{
                        //    Console.WriteLine(client.Alias);
                        //}
                        //Convert this to use Jesse's Code
                    }
                    else
                    {
                        ConsoleLogger.Push("Command " + serverMessage + " is not recognized");
                    }
                }
                if (svcHost != null)
                {
                    svcHost.Close();
                }
            }
        }
    }
}