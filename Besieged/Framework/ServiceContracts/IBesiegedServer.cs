using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Framework;
namespace Framework.ServiceContracts
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IClient))]
    public interface IBesiegedServer
    {
        [OperationContract]
        void SendMessage(string message);
    }
}
