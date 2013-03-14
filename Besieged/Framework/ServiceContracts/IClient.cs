using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ServiceContracts
{
    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void Notify(string message);
    }
}
