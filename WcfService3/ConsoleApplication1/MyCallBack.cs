using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplication1.ServiceReference1;
using System.ServiceModel;

namespace ConsoleApplication1
{
    class MyCallBack: IService1Callback, IDisposable 
    {
        Service1Client proxy;     
        
        public void callService()
        {
           InstanceContext context = new InstanceContext(this);
           proxy = new Service1Client(context);
           proxy.OpenSession();
        } 

        public void Dispose()
        {
            proxy.Close();
        }

        public void OnSessionCreated(string str)
        {
            Console.WriteLine(str);
        }
    }
}
