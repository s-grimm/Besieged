using System.Collections.Concurrent;
using System.ServiceModel;
using System.Runtime.Serialization;
using Framework.ServiceContracts;
using Framework.Commands;
using Framework.Utilities.Xml;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Utilities;

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
            string newGameId = Guid.NewGuid().ToString();
            m_Games.GetOrAdd(newGameId, new BesiegedGameInstance(newGameId, "Test Game", 4));
            
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
                        string newClientId = Guid.NewGuid().ToString();     // notify the client of their unique identifier which will be used for inter-client communication
                        CommandConnectionSuccessful commandConnectionSuccsessful = new CommandConnectionSuccessful(newClientId);
                        commandAggregate.Commands.Add(commandConnectionSuccsessful);
                        if (m_Games.Count > 0)  // notify the client of any pre-existing game instances that they might be able to join
                        {
                            foreach (KeyValuePair<string, BesiegedGameInstance> game in m_Games)
                            {
                                if (!game.Value.IsGameInstanceFull)
                                {
                                    string capacity = string.Format("{0}/{1} players", game.Value.ConnectedClients.Count, game.Value.MaxPlayers);
                                    CommandNotifyGame commandNotifyGame = new CommandNotifyGame(game.Value.GameId, game.Value.Name, capacity, false);
                                    commandAggregate.Commands.Add(commandNotifyGame);
                                }
                            }
                        }
                        string test = commandAggregate.ToXml();
                        clientCallBack.Notify(commandAggregate.ToXml());

                        m_ConnectedClients.GetOrAdd(newClientId, clientCallBack);     // Add an entry to the global client hook
                    }
                    else
                    {
                        m_MessageQueue.Add(command);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Push(ex);
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
                    if (m_Games.ContainsKey(commandJoinGame.GameId))
                    {
                        if (!m_Games[commandJoinGame.GameId].IsGameInstanceFull)
                        {
                            BesiegedGameInstance gameInstance = m_Games[commandJoinGame.GameId];
                            IClient client = m_ConnectedClients[commandJoinGame.ClientId];
                            ConnectedClient connectedClient = new ConnectedClient("Alias", commandJoinGame.ClientId, client);
                            gameInstance.ConnectedClients.Add(connectedClient);
                            if (gameInstance.ConnectedClients.Count == gameInstance.MaxPlayers)
                            {
                                gameInstance.IsGameInstanceFull = true;
                            }

                            string capacity = string.Format("{0}/{1} players", gameInstance.ConnectedClients.Count, gameInstance.MaxPlayers); // notify all connect clients of the updated game instance
                            CommandNotifyGame commandNotifyGame = new CommandNotifyGame(gameInstance.GameId, gameInstance.Name, capacity, gameInstance.IsGameInstanceFull);
                            NotifyAllConnectedClients(commandNotifyGame.ToXml());

                            CommandJoinGameSuccessful commandJoinGameSuccessful = new CommandJoinGameSuccessful(gameInstance.GameId);
                            NotifyClient(commandJoinGame.ClientId, commandJoinGameSuccessful.ToXml());
                        }
                        else
                        {
                            CommandServerError commandServerError = new CommandServerError("Game is full");
                            NotifyClient(commandJoinGame.ClientId, commandServerError.ToXml());
                        }
                    }
                    else
                    {
                        CommandServerError commandServerError = new CommandServerError("Game cannot be found");
                        NotifyClient(commandJoinGame.ClientId, commandServerError.ToXml());
                    }
                }

                else if (command is CommandCreateGame)
                {
                    CommandCreateGame commandCreateGame = command as CommandCreateGame; // create the new game instance
                    string newGameId = Guid.NewGuid().ToString();
                    BesiegedGameInstance gameInstance = new BesiegedGameInstance(newGameId, commandCreateGame.GameName, commandCreateGame.MaxPlayers);
                    m_Games.GetOrAdd(newGameId, gameInstance);

					IClient client = m_ConnectedClients[commandCreateGame.ClientId];    // add the client that requested the new game to the game instance
					ConnectedClient connectedClient = new ConnectedClient("Alias", commandCreateGame.ClientId, client);
					gameInstance.ConnectedClients.Add(connectedClient);

                    CommandJoinGameSuccessful commandJoinGameSuccessful = new CommandJoinGameSuccessful(gameInstance.GameId);
                    NotifyClient(commandCreateGame.ClientId, commandJoinGameSuccessful.ToXml());

                    string capacity = string.Format("{0}/{1} players", gameInstance.ConnectedClients.Count, gameInstance.MaxPlayers);   // notify all connect clients of the updated game instance
                    CommandNotifyGame commandNotifyGame = new CommandNotifyGame(gameInstance.GameId, gameInstance.Name, capacity, gameInstance.IsGameInstanceFull);
                    NotifyAllConnectedClients(commandNotifyGame.ToXml()); 
                }

                else if (command is CommandChatMessage)
                {
                    CommandChatMessage commandChatMessage = command as CommandChatMessage;
                    BesiegedGameInstance gameInstance = m_Games[commandChatMessage.GameId];
                    gameInstance.MessageQueue.Add(commandChatMessage);
                    
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Push(ex);
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
                ErrorLogger.Push(ex);
                // error handling
            }
        }
    }
}