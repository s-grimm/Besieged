using System.Collections.Concurrent;
using System.ServiceModel;
using System.Runtime.Serialization;
using Framework.BesiegedMessages;
using Framework.ServiceContracts;
using Framework.Utilities.Xml;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using Utilities;
using BesiegedServer.Maps;
using Framework.Utilities;

namespace BesiegedServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BesiegedServer : IBesiegedServer
    {
        private static ConcurrentDictionary<string, ConnectedClient> m_ConnectedClients = new ConcurrentDictionary<string, ConnectedClient>();  // global hook for all clients
        private ConcurrentDictionary<string, BesiegedGameInstance> m_Games = new ConcurrentDictionary<string, BesiegedGameInstance>();
        private MonitoredValue<bool> m_IsServerInitialized;
        private IClient m_ServerCallback;

        // Reactive stuff
        public static BlockingCollection<BesiegedMessage> m_MessageQueue = new BlockingCollection<BesiegedMessage>();
        public static IConnectableObservable<BesiegedMessage> MessagePublisher;
        private IDisposable m_GenericServerMessageSubscriber;  // subscribes to generic messages that are only bound for the server
        private IDisposable m_ServerMessageSubscriber;  // subscribes to messages that are only bound for the server
        public static Subject<BesiegedMessage> MessageSubject;

        public BesiegedServer()
        {
            m_IsServerInitialized = new MonitoredValue<bool>(false);

            MessageSubject = new Subject<BesiegedMessage>();

            var subjectSource = m_MessageQueue
                .GetConsumingEnumerable()
                .ToObservable(TaskPoolScheduler.Default)
                .Subscribe(MessageSubject);

            //MessagePublisher = observableMessages.Publish();
        }

        private void ProcessMessages()
        {
            // All generic server messages are handled here
            m_GenericServerMessageSubscriber = MessageSubject
                .Where(message => message is GenericServerMessage)
                .Subscribe(message =>
                {
                    var genericMessage = message as GenericServerMessage;
                    switch (genericMessage.MessageEnum)
                    {
                        case ServerMessage.ServerMessageEnum.StartServer:
                            break;
                        case ServerMessage.ServerMessageEnum.ServerStarted:
                            break;
                        default:
                            ConsoleLogger.Push("Unhandled GenericServerMessage was received: " + genericMessage.MessageEnum.ToString());
                            break;
                    }
                });

            // All server messages are handled here
            m_ServerMessageSubscriber = MessageSubject
                .Where(message => message is ServerMessage && !(message is GenericServerMessage))
                .Subscribe(message =>
                {
                    if (message is CreateGameMessage)
                    {
                        //CommandCreateGame commandCreateGame = command as CommandCreateGame; // create the new game instance
                        CreateGameMessage gameMessage = message as CreateGameMessage;
                        string gameId = Guid.NewGuid().ToString();
                        BesiegedGameInstance gameInstance = new BesiegedGameInstance(gameId, gameMessage.GameName, gameMessage.MaxPlayers, gameMessage.Password, gameMessage.ClientId);
                        m_Games.GetOrAdd(gameId, gameInstance);

                        JoinGameMessage join = new JoinGameMessage() { ClientId = message.ClientId, Password = gameMessage.Password, GameId = gameId };  // add the client that requested the new game to the game instance
                        m_MessageQueue.Add(join);

                        string capacity = string.Format("{0}/{1} players", gameInstance.Players.Count, gameInstance.MaxPlayers);   // notify all connect clients of the updated game instance
                        GameInfoMessage gameInfo = new GameInfoMessage(gameInstance.GameId, gameInstance.Name, capacity, gameInstance.IsGameInstanceFull, gameInstance.Password != string.Empty ? true : false);
                        ConsoleLogger.Push(string.Format("{0} has created a new game called: {1}", m_ConnectedClients[message.ClientId].Name, gameInstance.Name));
                        NotifyAllConnectedClients(gameInfo.ToXml()); 
                    }
                });
        }

        public void SendMessage(string serializedMessage)
        {
            try
            {
                BesiegedMessage message = serializedMessage.FromXml<BesiegedMessage>();
                if (message is GenericServerMessage && (message as GenericServerMessage).MessageEnum == ServerMessage.ServerMessageEnum.StartServer)
                {
                    m_ServerCallback = OperationContext.Current.GetCallbackChannel<IClient>();
                    GenericServerMessage started = new GenericServerMessage() { MessageEnum = ServerMessage.ServerMessageEnum.ServerStarted };
                    m_ServerCallback.SendMessage(started.ToXml());
                    m_IsServerInitialized.Value = true;
                    //StartProcessingMessages();
                    ProcessMessages();
                }
                else if (message is ConnectMessage)
                {
                    IClient callback = OperationContext.Current.GetCallbackChannel<IClient>();
                    string clientId = Guid.NewGuid().ToString();
                    ConnectedClient connectedClient = new ConnectedClient((message as ConnectMessage).Name, clientId, callback);
                    AggregateMessage aggregate = new AggregateMessage();
                    GenericClientMessage success = new GenericClientMessage() { ClientId = clientId, MessageEnum = ClientMessage.ClientMessageEnum.ConnectSuccessful };
                    aggregate.MessageList.Add(success);

                    if (m_Games.Count > 0)  // notify the client of any pre-existing game instances that they might be able to join
                    {
                        foreach (KeyValuePair<string, BesiegedGameInstance> game in m_Games)
                        {
                            if (!game.Value.IsGameInstanceFull)
                            {
                                string capacity = string.Format("{0}/{1} players", game.Value.Players.Count, game.Value.MaxPlayers);
                                GameInfoMessage gameInfo = new GameInfoMessage(game.Value.GameId, game.Value.Name, capacity, false, game.Value.Password != string.Empty ? true : false);
                                aggregate.MessageList.Add(gameInfo);
                            }
                        }
                    }

                    callback.SendMessage(aggregate.ToXml());
                    ConsoleLogger.Push(string.Format("{0} has joined the server", connectedClient.Name));
                    m_ConnectedClients.GetOrAdd(clientId, connectedClient);     // Add an entry to the global client hook
                }
                else
                {
                    m_MessageQueue.Add(message);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Push(ex);
            }
        }

        public static ConnectedClient GetConnectedClientById(string clientId)
        {
            if (m_ConnectedClients.ContainsKey(clientId))
            {
                return m_ConnectedClients[clientId];
            }
            return null;
        }

        public void NotifyClient(string clientId, string command)
        {
            if (m_ConnectedClients.ContainsKey(clientId))
            {
                m_ConnectedClients[clientId].Callback.SendMessage(command);   
            }
        }

        public void NotifyAllConnectedClients(string command)
        {
            try
            {
                foreach (KeyValuePair<string, ConnectedClient> client in m_ConnectedClients)
                {
                    if (((ICommunicationObject)client.Value.Callback).State == CommunicationState.Opened)
                    {
                        client.Value.Callback.SendMessage(command);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Push(ex);
                // error handling
            }
        }
    }
}