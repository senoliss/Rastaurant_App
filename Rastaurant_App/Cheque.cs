using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rastaurant_App
{
	public class Cheque : ICheque
	{
		/*Here we will have a Checkout class which will hande a checkout recipe fro customer to pay off of. 
		 It should handle Menu items with it's objects, prices and etc.*/

		public void Checkout(Table selectedTable, List<Table> tables, Dictionary<Table, List<Order>> tableOrders)
		{
            Console.WriteLine("Select table to print a checque to: ");

            RestaurantService.TablePrinter(tables);

            Console.Write("Enter table number: ");
            int tableNumber = int.Parse(Console.ReadLine());

            selectedTable = tables.FirstOrDefault(table => table.TableNumber == tableNumber);
            // Check if the selected table already has ordered items
            if (tableOrders.ContainsKey(selectedTable) && tableOrders[selectedTable].Count > 0)
            {
                ConsoleKey key = ConsoleKey.Q;
                do
                {
                    Console.Clear();
                    // Retrieve and display the ordered items from previously
                    double total = CheckoutSum(tableOrders, selectedTable);
                    List<Order> orderedItems = tableOrders[selectedTable];
                    Console.WriteLine($"Previously Ordered items for Table {selectedTable.TableNumber}:");
                    foreach (Order order in orderedItems)
                    {
                        Console.WriteLine($"{order.OrderedItem.Name} - {order.OrderedItem.Price}");
                    }
                    Console.WriteLine("-------------------");
                    Console.WriteLine($"Total: {total}");
                    Console.WriteLine("-------------------");
                    //Console.WriteLine("Pay the amount (y) or hit the streets (n)? ");


                    Console.WriteLine("Pay the amount (y) or hit the streets (n)? ");
                    key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Y)
                    {
                        Console.WriteLine($"\nAmount {total} paid succesfully!");
                        tableOrders[selectedTable].Clear();
                        RestaurantService.MarkTableAsVacant(selectedTable);
                    }
                    else if (key == ConsoleKey.N) Console.WriteLine("\nYou better run fast!");
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nWRONG INPUT!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(1500);
                    }
                }
                while (key != ConsoleKey.Y && key != ConsoleKey.N);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Selected table {selectedTable.TableNumber} is vacant and does not have an open TAB!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public double CheckoutSum(Dictionary<Table, List<Order>> tableOrders, Table selectedTable)
        {
            List<Order> orderedItems = tableOrders[selectedTable];
            double total = 0;
            foreach (Order order in orderedItems)
            {
                total += order.OrderedItem.Price;
            }
            return total;
        }
    }
}
