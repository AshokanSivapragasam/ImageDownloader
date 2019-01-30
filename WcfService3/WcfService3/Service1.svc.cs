using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Timers;

namespace WcfService3
{   
    [ServiceBehavior(InstanceContextMode= InstanceContextMode.PerCall)]
    public class Service1 : IService1
    {
        public static System.Timers.Timer timer;
        public static ISessionCallBack callback;

        public void OpenSession()
        {
            Console.WriteLine("> Session opened at {0}", DateTime.Now);
            callback = OperationContext.Current.GetCallbackChannel<ISessionCallBack>();

            timer = new System.Timers.Timer(5000);
            timer.Elapsed += OnTimerElapsed;
            timer.Enabled = true;
        }

        void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            callback.OnSessionCreated("Session ID: " + Guid.NewGuid().ToString());
        }

    }
}
