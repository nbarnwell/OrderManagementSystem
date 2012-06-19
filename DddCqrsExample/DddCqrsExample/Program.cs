using System;
using DddCqrsExample.Domain;
using DddCqrsExample.Domain.Orders;
using DddCqrsExample.Framework;
using Simple.Data;

namespace DddCqrsExample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = new Bootstrapper().Run();

            // Probably this would come from Customer.CreateOrder()
            var commandProcessor = container.Resolve<ICommandProcessor>();
            try
            {
                Console.WriteLine("Would you like to place a new sales order? (y/n):");
                string answer = Console.ReadLine().Trim();
                if (answer == "y")
                {
                    Console.WriteLine("Enter the maximum order value in GBP:"); // Yes, you'd get this from the customers's details...
                    decimal maxOrderValue = decimal.Parse(Console.ReadLine());

                    commandProcessor.Process(new CreateSalesOrderCommand(new Money(maxOrderValue, Currency.GBP)));

                    Console.WriteLine();
                    Console.WriteLine("Thank you for your order.");
                    Console.WriteLine();
                }

                
                
                Console.WriteLine("Would you like to add additional items to an existing sales order? (y/n):");
                answer = Console.ReadLine().Trim();
                if (answer == "y")
                {
                    dynamic db = Database.OpenNamedConnection("ReadStore");

                    Console.WriteLine("Orders:");
                    foreach (var order in db.SalesOrders.All())
                    {
                        Console.WriteLine("   {0}", order.Id);
                    }

                    Console.WriteLine();

                    // This would come from user selecting an order and saying "add items" or adding items to basket from a search page
                    Console.WriteLine("Enter the order ID to add items to:");
                    string id = Console.ReadLine();

                    Console.WriteLine("Enter the SKU:");
                    string sku = Console.ReadLine();

                    Console.WriteLine("Enter the quantity:");
                    uint quantity = uint.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the unit price in GBP:"); // Yes, you'd get this from the product's details...
                    decimal unitPrice = decimal.Parse(Console.ReadLine());
                    
                    commandProcessor.Process(new AddItemsToSalesOrderCommand(id, new Sku(sku), quantity, new Money(unitPrice, Currency.GBP)));
                }
            }
            finally
            {
                container.Release(commandProcessor);
            }

            Console.WriteLine("Completed.");
            Console.ReadKey();
        }
    }
}
