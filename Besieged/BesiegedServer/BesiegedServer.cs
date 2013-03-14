using System.Collections.Concurrent;
using BesiegedLogic;
using System.ServiceModel;
using System.Runtime.Serialization;
using Framework.ServiceContracts;
using Framework.Commands;
using Framework.Utilities.Xml;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BesiegedServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BesiegedServer : IBesiegedServer
    {

        private BlockingCollection<Command> m_MessageQueue = new BlockingCollection<Command>();
        private ConcurrentDictionary<string, IClient> m_ConnectedClients = new ConcurrentDictionary<string, IClient>();  // global hook for all clients
        private ConcurrentDictionary<string, BesiegedGameInstance> m_Games = new ConcurrentDictionary<string, BesiegedGameInstance>();

        public BesiegedServer()
        {
            // Hardcode a game instance as a test
            string uniqueGameIdentifier = Guid.NewGuid().ToString();
            m_Games.GetOrAdd(uniqueGameIdentifier, new BesiegedGameInstance(uniqueGameIdentifier, "Test Game"));
            
            // Start spinning the process message loop
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Command command = m_MessageQueue.Take();
                    ProcessMessage(command);
                }
            }, TaskCreationOptions.LongRunning);
        }

        public void SendCommand(string serializedCommand)
        {
            try
            {
                Command command = serializedCommand.FromXml<Command>();
                if (command != null)
                {
                    if (command is CommandConnect)
                    {
                        CommandConnect connectCommand = command as CommandConnect;
                        IClient clientCallBack = OperationContext.Current.GetCallbackChannel<IClient>();

                        CommandAggregate commandAggregate = new CommandAggregate();
                        string newClientIdentifier = Guid.NewGuid().ToString();     // notify the client of their unique identifier which will be used for inter-client communication
                        CommandConnectionSuccessful commandConnectionSuccsessful = new CommandConnectionSuccessful(newClientIdentifier);
                        commandAggregate.Commands.Add(commandConnectionSuccsessful);
                        if (m_Games.Count > 0)  // notify the client of any pre-existing game instances that they might be able to join
                        {
                            foreach (KeyValuePair<string, BesiegedGameInstance> game in m_Games)
                            {
                                if (!game.Value.IsGameInstanceFull)
                                {
                                    string capacity = string.Format("{0}/4 players", game.Value.ConnectedClients.Count);
                                    CommandNotifyGame commandNotifyGame = new CommandNotifyGame(game.Value.UniqueIdentifier, game.Value.Name, capacity, false);
                                    commandAggregate.Commands.Add(commandNotifyGame);
                                }
                            }
                        }
                        string test = commandAggregate.ToXml();
                        clientCallBack.Notify(commandAggregate.ToXml());

                        m_ConnectedClients.GetOrAdd(newClientIdentifier, clientCallBack);     // Add an entry to the global client hook
                    }
                    else
                    {
                        m_MessageQueue.Add(command);
                    }
                }
            }
            catch (Exception ex)
            {
                // error handling
            }
        }

        public void ProcessMessage(Command command)
        {
            try
            {
                if (command is CommandJoinGame)
                {
                    CommandJoinGame commandJoinGame = command as CommandJoinGame;
                    if (m_Games.ContainsKey(commandJoinGame.GameIdentifier))
                    {
                        if (!m_Games[commandJoinGame.GameIdentifier].IsGameInstanceFull)
                        {
                            BesiegedGameInstance gameInstance = m_Games[commandJoinGame.GameIdentifier];
                            IClient client = m_ConnectedClients[commandJoinGame.ClientIdentifier];
                            ConnectedClient connectedClient = new ConnectedClient("Alias", commandJoinGame.ClientIdentifier, client);
                            gameInstance.ConnectedClients.Add(connectedClient);

                            string capacity = string.Format("{0}/4 players", gameInstance.ConnectedClients.Count); // notify all connect clients of the updated game instance
                            CommandNotifyGame commandNotifyGame = new CommandNotifyGame(gameInstance.UniqueIdentifier, gameInstance.Name, capacity, gameInstance.IsGameInstanceFull);
                            NotifyAllConnectedClients(commandNotifyGame.ToXml());
                        }
                        else
                        {
                            CommandServerError commandServerError = new CommandServerError("Game is full");
                            NotifyClient(commandJoinGame.ClientIdentifier, commandServerError.ToXml());
                        }
                    }
                    else
                    {
                        CommandServerError commandServerError = new CommandServerError("Game cannot be found");
                        NotifyClient(commandJoinGame.ClientIdentifier, commandServerError.ToXml());
                    }
                }
            }
            catch (Exception ex)
            {
                // error handling
            }
        }

        public void NotifyClient(string clientId, string command)
        {
            if (m_ConnectedClients.ContainsKey(clientId))
            {
                m_ConnectedClients[clientId].Notify(command);   
            }
        }

        public void NotifyAllConnectedClients(string command)
        {
            try
            {
                foreach (KeyValuePair<string, IClient> client in m_ConnectedClients)
                {
                    if (((ICommunicationObject)client.Value).State == CommunicationState.Opened)
                    {
                        client.Value.Notify(command);
                    }
                }
            }
            catch (Exception ex)
            {
                // error handling
            }
        }
    }
}