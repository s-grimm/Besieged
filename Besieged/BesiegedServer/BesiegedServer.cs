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

        private BlockingCollection<Command> _messageQueue = new BlockingCollection<Command>();
        private Dictionary<string, IClient> _connectedClients = new Dictionary<string,IClient>();  // global hook for all clients

        public BesiegedServer()
        {
            // Start spinning the process message loop
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Command command = _messageQueue.Take();
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
                        // notify the client of their unique identifier which will be used for inter-client communication
                        string newClientIdentifier = Guid.NewGuid().ToString();
                        CommandConnectionSuccessful commandConnectionSuccsessful = new CommandConnectionSuccessful(newClientIdentifier);
                        clientCallBack.Notify(commandConnectionSuccsessful.ToXml());
                        // Add an entry to the global client hook
                        _connectedClients.Add(newClientIdentifier, clientCallBack);
                    }
                    else
                    {
                        _messageQueue.Add(command);
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