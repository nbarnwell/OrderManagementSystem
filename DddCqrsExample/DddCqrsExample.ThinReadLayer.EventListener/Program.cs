using System;

namespace DddCqrsExample.ThinReadLayer.EventListener
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
