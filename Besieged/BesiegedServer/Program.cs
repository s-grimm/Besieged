using Framework.Commands;
using Framework.ServiceContracts;
using Framework.Utilities.Xml;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using Utilities;
using BesiegedServer.Maps;
using Framework.BesiegedMessages;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Reactive.Concurrency;
namespace BesiegedServer
{
    internal class Program
    {
        private static ServerClient m_ServerClient;
        private static IBesiegedServer m_BesiegedServer;
        private static IObservable<BesiegedMessage> m_MessagePublisher;

        private static void Main(string[] args)
        {
            ServiceHost svcHost = null;
            try
            {
                svcHost = new ServiceHost(typeof(BesiegedServer), new Uri("net.tcp://127.0.0.1:31337/BesiegedServer/"));
                svcHost.AddServiceEndpoint(typeof(Framework.ServiceContracts.IBesiegedServer), new NetTcpBinding(SecurityMode.None), "BesiegedMessage");
                svcHost.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = false });
                svcHost.Open();

                // Configure a client callback for the server itself to force start the process
                m_ServerClient = new ServerClient();
                EndpointAddress endpointAddress = new EndpointAddress("net.tcp://127.0.0.1:31337/BesiegedServer/BesiegedMessage");
                DuplexChannelFactory<IBesiegedServer> duplexChannelFactory = new DuplexChannelFactory<IBesiegedServer>(m_ServerClient, new NetTcpBinding(SecurityMode.None), endpointAddress);
                m_BesiegedServer = duplexChannelFactory.CreateChannel();

                Task.Factory.StartNew(() =>
                {
                    var startServer = new GenericServerMessage() { MessageEnum = ServerMessage.ServerMessageEnum.StartServer };
                    m_BesiegedServer.SendMessage(startServer.ToXml());
                });

                m_MessagePublisher = m_ServerClient.MessageQueue
                    .GetConsumingEnumerable()
                    .ToObservable(TaskPoolScheduler.Default);

                // All generic server messages are handled here
                var genericServerMessageSubscriber = m_MessagePublisher
                    .Where(message => message is GenericServerMessage)
                    .Subscribe(message =>
                    {
                        var genericMessage = message as GenericServerMessage;
                        switch (genericMessage.MessageEnum)
                        {
                            case ServerMessage.ServerMessageEnum.StartServer:
                                break;
                            case ServerMessage.ServerMessageEnum.ServerStarted:
                                ConsoleLogger.Push("Server has started.");
                                break;
                            default:
                                ConsoleLogger.Push("Unhandled GenericServerMessage was received: " + genericMessage.MessageEnum.ToString());
                                break;
                        }
                    });

                // All server messages are handled here
                var m_ServerMessageSubscriber = m_MessagePublisher
                    .Where(message => message is ServerMessage && !(message is GenericServerMessage))
                    .Subscribe(message =>
                    {
                        // do stuff with server bound messages here
                    }); 
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