using System;

namespace DddCqrsExample.ThinReadLayer.EventListener
{
    public static class Program
    {
        public static void Main()
        {
            new Listener().Start();

            Console.WriteLine("Listening for published events. Press any key to close...");
            Console.ReadKey();
        }
    }
}
