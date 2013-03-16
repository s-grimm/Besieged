using Framework.Commands;
using Framework.ServiceContracts;
using Framework.Utilities.Xml;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using Utilities;
using BesiegedServer.Maps;
namespace BesiegedServer
{
    internal class Program
    {
        private static ServerClient m_ServerClient;
        private static IBesiegedServer m_BesiegedServer;

        private static void ProcessMessage(Command command)
        {
            if (command is CommandServerStarted)
            {
                ConsoleLogger.Push("Server started - Two way connection established");
            }
        }

        private static void Main(string[] args)
        {
            ServiceHost svcHost = null;
            try
            {
                svcHost = new ServiceHost(typeof(BesiegedServer), new Uri("http://localhost:31337/BesiegedServer/"));
                svcHost.AddServiceEndpoint(typeof(Framework.ServiceContracts.IBesiegedServer), new WSDualHttpBinding(), "BesiegedMessage");
                svcHost.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = true });
                svcHost.Credentials.WindowsAuthentication.AllowAnonymousLogons = true;
                svcHost.Open();

                // Configure a client callback for the server itself to force start the process
                m_ServerClient = new ServerClient();
                EndpointAddress endpointAddress = new EndpointAddress("http://localhost:31337/BesiegedServer/BesiegedMessage");
                DuplexChannelFactory<IBesiegedServer> duplexChannelFactory = new DuplexChannelFactory<IBesiegedServer>(m_ServerClient, new WSDualHttpBinding(), endpointAddress);
                m_BesiegedServer = duplexChannelFactory.CreateChannel();

                Task.Factory.StartNew(() =>
                {
                    CommandStartServer commandConnect = new CommandStartServer();
                    m_BesiegedServer.SendCommand(commandConnect.ToXml());
                });

                Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        Command message = m_ServerClient.MessageQueue.Take();
                        ProcessMessage(message);
                    }
                }, TaskCreationOptions.LongRunning);
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