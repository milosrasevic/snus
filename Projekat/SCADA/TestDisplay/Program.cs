using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestDisplay
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.DisplayClient proxy = new ServiceReference1.DisplayClient();

            while (true)
            {
                Console.WriteLine(proxy.readDisplayValues());
                Thread.Sleep(1000);
            }
        }
    }
}
