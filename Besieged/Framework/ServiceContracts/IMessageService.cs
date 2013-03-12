using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Framework;
namespace Framework.ServiceContracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMessageService" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IClient))]
    public interface IMessageService
    {
        [OperationContract]
        bool Subscribe();

        [OperationContract]
        bool Unsubscribe();

        [OperationContract]
        void SendMessage(string message);
    }
}
