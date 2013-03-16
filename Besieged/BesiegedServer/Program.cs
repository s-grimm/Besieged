using Framework.Commands;
using Framework.ServiceContracts;
using Framework.Utilities.Xml;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;

namespace BesiegedServer
{
    internal class Program
    {
        private static ServerClient m_Client = new ServerClient();
        private static IBesiegedServer m_BesiegedServer;

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
                EndpointAddress endpointAddress = new EndpointAddress("http://localhost:31337/BesiegedServer/BesiegedMessage");
                DuplexChannelFactory<IBesiegedServer> duplexChannelFactory = new DuplexChannelFactory<IBesiegedServer>(m_Client, new WSDualHttpBinding(), endpointAddress);
                m_BesiegedServer = duplexChannelFactory.CreateChannel();

                Task.Factory.StartNew(() =>
                {
                    CommandStartServer commandConnect = new CommandStartServer();
                    m_BesiegedServer.SendCommand(commandConnect.ToXml());
                });

                Console.Write("Service Started.\n> ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                while (true)
                {
                    var serverMessage = Console.ReadLine().Trim().ToString().ToLower();
                    if (serverMessage == "exit") break;
                    else if (serverMessage == "?" || serverMessage == "help" || serverMessage == "\\help")
                    {
                        Console.WriteLine("Besieged Server Commands\nexit: Stops the server.");
                        Console.Write("> ");
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
                }
                if (svcHost != null)
                {
                    svcHost.Close();
                }
            }
        }
    }
}