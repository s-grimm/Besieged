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
using BesiegedServer.Maps;

namespace BesiegedServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BesiegedServer : IBesiegedServer
    {

        private BlockingCollection<Command> m_MessageQueue = new BlockingCollection<Command>();
        private ConcurrentDictionary<string, ConnectedClient> m_ConnectedClients = new ConcurrentDictionary<string, ConnectedClient>();  // global hook for all clients
        private ConcurrentDictionary<string, BesiegedGameInstance> m_Games = new ConcurrentDictionary<string, BesiegedGameInstance>();
        private bool m_IsServerInitialized = false;
        private IClient m_ServerCallback;

        public BesiegedServer()
        {
            // Hardcode a game instance as a test
            string newGameId = Guid.NewGuid().ToString();
            m_Games.GetOrAdd(newGameId, new BesiegedGameInstance(newGameId, "Test Game", 4, "jesse"));
        }

        private void StartProcessingMessages()
        {
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
                    if (command is CommandStartServer)
                    {
                        if (!m_IsServerInitialized)
                        {
                            m_ServerCallback = OperationContext.Current.GetCallbackChannel<IClient>();
                            CommandServerStarted commandServerStarted = new CommandServerStarted();
                            string test = commandServerStarted.ToXml();
                            m_ServerCallback.Notify(commandServerStarted.ToXml());
                            m_IsServerInitialized = true;
                            StartProcessingMessages();
                        }
                    }
                    
                    else if (command is CommandConnect)
                    {
                        CommandConnect connectCommand = command as CommandConnect;
                        IClient callback = OperationContext.Current.GetCallbackChannel<IClient>();
                        string newClientId = Guid.NewGuid().ToString();     // notify the client of their unique identifier which will be used for inter-client communication
                        ConnectedClient connectedClient = new ConnectedClient(connectCommand.Name, newClientId, callback);

                        CommandAggregate commandAggregate = new CommandAggregate();
                        CommandConnectionSuccessful commandConnectionSuccsessful = new CommandConnectionSuccessful(newClientId);
                        commandAggregate.Commands.Add(commandConnectionSuccsessful);
                        if (m_Games.Count > 0)  // notify the client of any pre-existing game instances that they might be able to join
                        {
                            foreach (KeyValuePair<string, BesiegedGameInstance> game in m_Games)
                            {
                                if (!game.Value.IsGameInstanceFull)
                                {
                                    string capacity = string.Format("{0}/{1} players", game.Value.Players.Count, game.Value.MaxPlayers);
                                    CommandNotifyGame commandNotifyGame = new CommandNotifyGame(game.Value.GameId, game.Value.Name, capacity, false, game.Value.Password != string.Empty ? true: false);
                                    commandAggregate.Commands.Add(commandNotifyGame);
                                }
                            }
                        }
                        connectedClient.Callback.Notify(commandAggregate.ToXml());

                        ConsoleLogger.Push(string.Format("{0} has joined the server", connectedClient.Name));
                        m_ConnectedClients.GetOrAdd(newClientId, connectedClient);     // Add an entry to the global client hook
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
                            if (m_Games[commandJoinGame.GameId].Password == commandJoinGame.Password)
                            {
                                BesiegedGameInstance gameInstance = m_Games[commandJoinGame.GameId];
                                ConnectedClient client = m_ConnectedClients[commandJoinGame.ClientId];
                                gameInstance.AddPlayer(client);
                                if (gameInstance.Players.Count == gameInstance.MaxPlayers)
                                {
                                    gameInstance.IsGameInstanceFull = true;
                                }

                                string capacity = string.Format("{0}/{1} players", gameInstance.Players.Count, gameInstance.MaxPlayers); // notify all connect clients of the updated game instance
                                CommandNotifyGame commandNotifyGame = new CommandNotifyGame(gameInstance.GameId, gameInstance.Name, capacity, gameInstance.IsGameInstanceFull, gameInstance.Password != string.Empty);
                                NotifyAllConnectedClients(commandNotifyGame.ToXml());

                                //CommandJoinGameSuccessful commandJoinGameSuccessful = new CommandJoinGameSuccessful(gameInstance.GameId);
                                //NotifyClient(commandJoinGame.ClientId, commandJoinGameSuccessful.ToXml());
								ConsoleLogger.Push(string.Format("{0} has joined Game {1}", client.Name, gameInstance.Name));
                            }
                            else
                            {
                                CommandServerError commandServerError = new CommandServerError("Incorrect Password");
                                ConsoleLogger.Push(string.Format("{0} has attempted to join Game {1} with an incorrect password", m_ConnectedClients[commandJoinGame.ClientId].Name, m_Games[commandJoinGame.GameId].Name));
                                NotifyClient(commandJoinGame.ClientId, commandServerError.ToXml());
                            }
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
                    BesiegedGameInstance gameInstance = new BesiegedGameInstance(newGameId, commandCreateGame.GameName, commandCreateGame.MaxPlayers, commandCreateGame.Password);
                    m_Games.GetOrAdd(newGameId, gameInstance);

					ConnectedClient client = m_ConnectedClients[commandCreateGame.ClientId];    // add the client that requested the new game to the game instance
                    gameInstance.AddPlayer(client);

                    CommandJoinGameSuccessful commandJoinGameSuccessful = new CommandJoinGameSuccessful(gameInstance.GameId);
                    NotifyClient(commandCreateGame.ClientId, commandJoinGameSuccessful.ToXml());

                    string capacity = string.Format("{0}/{1} players", gameInstance.Players.Count, gameInstance.MaxPlayers);   // notify all connect clients of the updated game instance
                    CommandNotifyGame commandNotifyGame = new CommandNotifyGame(gameInstance.GameId, gameInstance.Name, capacity, gameInstance.IsGameInstanceFull, gameInstance.Password != string.Empty ? true:false);
                    ConsoleLogger.Push(string.Format("{0} has created a new Game Id {1}", client.Name, gameInstance.Name));
                    NotifyAllConnectedClients(commandNotifyGame.ToXml()); 
                }

                else if (command is CommandChatMessage)
                {
                    CommandChatMessage commandChatMessage = command as CommandChatMessage;
                    BesiegedGameInstance gameInstance = m_Games[commandChatMessage.GameId];
                    gameInstance.MessageQueue.Add(commandChatMessage);
                }

                else if (command is CommandSendGameMap)
                {
                    CommandSendGameMap commandSendGameMap = command as CommandSendGameMap;
                    MapUtilities.SaveToFile(commandSendGameMap.SerializedMap);
                }

                else if (command is CommandConnectionTerminated)
                {
                    //CommandConnectionTerminated commandConnectionTerminated = command as CommandConnectionTerminated;

                    //IClient client = m_ConnectedClients[commandConnectionTerminated.ClientId];
                    //ConnectedClient connectedClient = new ConnectedClient("Alias", commandConnectionTerminated.ClientId, client);

                    //if (commandConnectionTerminated.GameId != null)
                    //{
                    //    BesiegedGameInstance gameInstance = m_Games[commandConnectionTerminated.GameId];
                    //    //var gameRemoved = gameInstance.Players.TryTake(out connectedClient);
                    //    //if (gameRemoved)
                    //    //{
                    //    //    ConsoleLogger.Push(string.Format("Client Id {0} has left Game Id {1}", connectedClient.ClientId, gameInstance.GameId));
                    //    //}
                    //}
                    
                    //var removed = m_ConnectedClients.TryRemove(commandConnectionTerminated.ClientId, out client);
                    //if (removed)
                    //{
                    //    ConsoleLogger.Push(string.Format("Client Id {0} has disconnected", commandConnectionTerminated.ClientId));
                    //}
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Push(ex);
            }
        }

        public void NotifyClient(string clientId, string command)
        {
            if (m_ConnectedClients.ContainsKey(clientId))
            {
                m_ConnectedClients[clientId].Callback.Notify(command);   
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
                        client.Value.Callback.Notify(command);
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