using System.Collections.Concurrent;
using BesiegedLogic;
using System.ServiceModel;
using System.Runtime.Serialization;
using Framework.ServiceContracts;
using Framework.Commands;
using Framework.Utilities.Xml;
using System;
namespace BesiegedServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MessageService : Framework.ServiceContracts.IMessageService
    {
        //public void SendCommand(string cmd)
        //{
        //    if (cmd == null) return;
        //    //if(cmd is Framework.Command.Server.Connect){
        //     //   Framework.Command.Server.Connect unWrapped = (Framework.Command.Server.Connect)cmd;
        //      //  GameObject.AddNewClient((string)unWrapped.Value);
        //    var res = GameObject.AddNewClient(cmd);
        //    Framework.IClient gamestate = OperationContext.Current.GetCallbackChannel<Framework.IClient>();
           
        //   // }
        //}

        public bool Subscribe()
        {
            try
            {
                IClient client = OperationContext.Current.GetCallbackChannel<IClient>();
                //if (!_clients.Contains(client))
                //{
                    //_clients.Add(client);
                    //if (((ICommunicationObject)client).State == CommunicationState.Opened)
                    //{
                        var res = GameObject.AddNewClient("Shane"); //change this method to accept a alias
                        ConnectionSuccessful connectionSuccessfulMessage = new ConnectionSuccessful();
                        string message = connectionSuccessfulMessage.ToXml();
                        client.Notify(message);
                //    }
                //}
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            //throw new System.NotImplementedException();
        }

        public bool Unsubscribe()
        {
            throw new System.NotImplementedException();
        }

        public void SendMessage(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}