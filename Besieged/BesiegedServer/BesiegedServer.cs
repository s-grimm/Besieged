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
        private Dictionary<string, IClient> m_ConnectedClients = new Dictionary<string,IClient>();  // global hook for all clients
        private Dictionary<string, BesiegedGameInstance> m_Games = new Dictionary<string, BesiegedGameInstance>();

        public BesiegedServer()
        {
            // Hardcode a game instance as a test
            string uniqueGameIdentifier = Guid.NewGuid().ToString();
            m_Games.Add(uniqueGameIdentifier, new BesiegedGameInstance(uniqueGameIdentifier));
            
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
                                    CommandNotifyGame commandNotifyGame = new CommandNotifyGame(game.Value.UniqueIdentifier, capacity, false);
                                    commandAggregate.Commands.Add(commandNotifyGame);
                                }
                            }
                        }
                        string test = commandAggregate.ToXml();
                        clientCallBack.Notify(commandAggregate.ToXml());

                        m_ConnectedClients.Add(newClientIdentifier, clientCallBack);     // Add an entry to the global client hook
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
                
            }
            catch (Exception ex)
            {
                // error handling
            }
        }

        public void NotifyAllConnectedClients(Command command)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                // error handling
            }
        }
    }
}