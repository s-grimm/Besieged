using System.Collections.Concurrent;
using BesiegedLogic;
using System.ServiceModel;
using System.Runtime.Serialization;
namespace BesiegedServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MessageService" in both code and config file together.
    public class MessageService : Framework.Server.Services.IMessageService
    {
        public void SendCommand(string cmd)
        {
            if (cmd == null) return;
            //if(cmd is Framework.Command.Server.Connect){
             //   Framework.Command.Server.Connect unWrapped = (Framework.Command.Server.Connect)cmd;
              //  GameObject.AddNewClient((string)unWrapped.Value);
            GameObject.AddNewClient(cmd);
           // }
        }
    }
}