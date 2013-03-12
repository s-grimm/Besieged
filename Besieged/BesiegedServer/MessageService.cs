using System.Collections.Concurrent;
using BesiegedLogic;
using System.ServiceModel;
using System.Runtime.Serialization;
namespace BesiegedServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MessageService : Framework.Server.Services.IMessageService
    {
        public void SendCommand(string cmd)
        {
            if (cmd == null) return;
            //if(cmd is Framework.Command.Server.Connect){
             //   Framework.Command.Server.Connect unWrapped = (Framework.Command.Server.Connect)cmd;
              //  GameObject.AddNewClient((string)unWrapped.Value);
            var res = GameObject.AddNewClient(cmd);
            Framework.IGameState gamestate = OperationContext.Current.GetCallbackChannel<Framework.IGameState>();
            gamestate.UpdateClient(res.Alias, res.ClientID);
           // }
        }
    }
}