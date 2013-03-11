using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Framework;
namespace Framework.Server.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMessageService" in both code and config file together.
    [ServiceContract]
    public interface IMessageService
    {
        [OperationContract]
        //void SendCommand(Framework.Command.ICommand cmd);
        void SendCommand(string cmd);
    }
}
