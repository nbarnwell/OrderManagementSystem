using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DddCqrsEsExample.ThinReadLayer.EventListener
{
    class Program
    {
        static void Main(string[] args)
        {
            new Listener().Start();

            Console.WriteLine("Listening for published events. Press any key to close...");
            Console.ReadKey();
        }
    }
}
