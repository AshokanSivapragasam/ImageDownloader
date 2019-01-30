using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplication1.ServiceReference1;
using System.ServiceModel; 

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            MyCallBack obj = new MyCallBack();
            obj.callService();
            Console.Read();
            obj.Dispose();

        }
    }
}
