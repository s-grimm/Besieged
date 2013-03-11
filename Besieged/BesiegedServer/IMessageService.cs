using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Framework;
namespace BesiegedServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMessageService" in both code and config file together.
    [ServiceContract]
    public interface IMessageService
    {
        [OperationContract]
        void Process(Framework.Command.ICommand cmd);
    }
}
