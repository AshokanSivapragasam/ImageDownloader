using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService3
{
    [ServiceContract(CallbackContract = typeof(ISessionCallBack))]
    public interface IService1
    {
        [OperationContract(IsOneWay=true)]
        void OpenSession();
    }

    public interface ISessionCallBack
    {
        [OperationContract(IsOneWay=true)]
        void OnSessionCreated(string str); 
    }
}
