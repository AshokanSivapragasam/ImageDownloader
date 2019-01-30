using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using GPH_QuickMessageServiceLib;

namespace GPH_QuickMessageServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(GPH_QuickMessageService)))
            {
                // Open the host and start listening for incoming messages.
                serviceHost.Open();
                // Keep the service running until the Enter key is pressed.
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press the Enter key to terminate service.");
                Console.ReadLine();
            }
        }
    }
}
