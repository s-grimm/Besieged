using BesiegedServer;
using Framework.BesiegedMessages;
using Framework.ServiceContracts;
using Framework.Utilities.Xml;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using Utilities;

namespace BesiegedConsole
{
    internal class Program
    {
        private static ServerClient m_ServerClient;
        private static IBesiegedServer m_BesiegedServer;

        private static void Main(string[] args)
        {
            ServiceHost svcHost = null;
            try
            {
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.None, true)
                    {
                        ReliableSession = { InactivityTimeout = new TimeSpan(0, 2, 0) },
                        SendTimeout = new TimeSpan(0, 2, 0),
                        ReceiveTimeout = new TimeSpan(0, 2, 0),
                        OpenTimeout = new TimeSpan(0, 1, 0),
                        CloseTimeout = new TimeSpan(0, 1, 0),
                        MaxReceivedMessageSize = 2147483647,
                        ReaderQuotas =
                            {
                                MaxArrayLength = 2147483647,
                                MaxBytesPerRead = 2147483647,
                                MaxStringContentLength = 2147483647,
                                MaxDepth = 2147483647,
                            },
                    };

                svcHost = new ServiceHost(typeof(BesiegedServer.BesiegedServer), new Uri("net.tcp://172.21.72.122:31337/BesiegedServer/"));
                svcHost.AddServiceEndpoint(typeof(Framework.ServiceContracts.IBesiegedServer), binding, "BesiegedMessage");
                svcHost.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = false });
                svcHost.Open();

                // Configure a client callback for the server itself to force start the process
                m_ServerClient = new ServerClient();
                EndpointAddress endpointAddress = new EndpointAddress("net.tcp://172.21.72.122:31337/BesiegedServer/BesiegedMessage");
                DuplexChannelFactory<IBesiegedServer> duplexChannelFactory = new DuplexChannelFactory<IBesiegedServer>(m_ServerClient, new NetTcpBinding(SecurityMode.None, true), endpointAddress);
                m_BesiegedServer = duplexChannelFactory.CreateChannel();

                Task.Factory.StartNew(() =>
                {
                    var startServer = new GenericServerMessage() { MessageEnum = ServerMessage.ServerMessageEnum.StartServer };
                    m_BesiegedServer.SendMessage(startServer.ToXml());
                });

                var subject = new Subject<BesiegedMessage>();

                var messagePublisher = m_ServerClient.MessageQueue
                    .GetConsumingEnumerable()
                    .ToObservable(TaskPoolScheduler.Default)
                    .Subscribe(subject);

                // All generic server messages are handled here
                var genericServerMessageSubscriber = subject
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
                var m_ServerMessageSubscriber = subject
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