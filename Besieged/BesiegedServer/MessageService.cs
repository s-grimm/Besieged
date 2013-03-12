using System.Collections.Concurrent;
using BesiegedLogic;
using System.ServiceModel;
using System.Runtime.Serialization;
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
            throw new System.NotImplementedException();
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