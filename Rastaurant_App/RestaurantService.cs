using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rastaurant_App
{
    public class RestaurantService
    {
        /*Here we will have a Restaurant class to handle services for other classes, 
         * such as take order from customer. 
         * Mark table as occupied or vacant when customer arrives. 
         * Load the menu and tables occupancy from some txt or csv files
         * 
         * Adittionaly we can Add:
         *      how much time will it take to complete order, 
         *      do we have enough ingridients for that order, 
         *      menu of the day with discounts,
         *      random table occupancy for week days 
         *      or import a calendar and weekdays to give discounts for that day
         *      and etc.
         */
        private List<Menu> menu;
        private List<Table> tables;
        private List<Order> orders; // track all orders
        private Table selectedTable;    // track selected tables to mark them occupied when selecting new table
        private Dictionary<Table, List<Order>> tableOrders = new Dictionary<Table, List<Order>>();  // to track orders for specific tables

        public RestaurantService()
        {
            // Initialize data from files (e.g., menu.csv, tables.txt)
            menu = LoadMenu("Files/Menu.csv");
            tables = LoadTables("Files/Tables.txt");
            orders = new List<Order>();
        }

        public void RegisterOrder()
        {
            Console.WriteLine("Available Tables:");
            TablePrinter();

            Console.Write("Enter table number: ");
            int tableNumber = int.Parse(Console.ReadLine());

            selectedTable = tables.FirstOrDefault(table => table.TableNumber == tableNumber);
            if (selectedTable == null)
            {
                Console.WriteLine("Invalid table number or table is already occupied.");
            }



            if (selectedTable.IsOccupied)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Selected Table is occupied! Hence it is red!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Do you want to order more for table {selectedTable.TableNumber} ? y/n: ");
                string kk = Console.ReadLine();
                if (kk == "y") selectedTable.IsOccupied = false;
                Thread.Sleep(3000);
            }

            if (selectedTable != null && !selectedTable.IsOccupied)
            {
                selectedTable.IsOccupied = true;
                bool orderNotFinished = true;
                List<int> OrderedItems = new List<int>();
                List<int> selectedIndexes = new List<int>();
                int k = 1;

                while (orderNotFinished)
                {
                    Console.Clear();
                    Console.WriteLine($"Selected Table: {selectedTable.TableNumber}");
                    Console.Write($"Now Ordered Items:");
                    for (int i = 0; i < OrderedItems.Count; i++)
                    {
                        int itemIndex = OrderedItems[i];
                        Console.Write($"{itemIndex + 1}");
                        if (i < OrderedItems.Count - 1)
                        {
                            Console.Write(",");
                        }
                    }
                    List<Order> newOrder = new List<Order>();
                    orders.Clear();
                    foreach (int itemIndex in OrderedItems)
                    {
                        if (itemIndex >= 0 && itemIndex < menu.Count)
                        {
                            Menu selectedItem = menu[itemIndex];
                            Order order = new Order(selectedTable.TableNumber, selectedItem, DateTime.Now);
                            orders.Add(order);
                        }

                    }
                    Console.WriteLine("\n-------------------");
                    // Check if the selected table already has ordered items
                    if (tableOrders.ContainsKey(selectedTable) && tableOrders[selectedTable].Count > 0)
                    {
                        // Retrieve and display the ordered items from previously
                        List<Order> orderedItems = tableOrders[selectedTable];
                        Console.WriteLine($"Previously Ordered items for Table {selectedTable.TableNumber}:");
                        foreach (Order order in orderedItems)
                        {
                            Console.WriteLine($"{order.OrderedItem.Name} - {order.OrderedItem.Price}");
                        }
                        Console.WriteLine("-------------------");
                    }
                    Console.WriteLine("Menu Items:");
                    Console.WriteLine("-------------------");
                    for (int i = 0; i < menu.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {menu[i].Name} - {menu[i].Price}");
                    }
                    Console.WriteLine("-------------------");

                    Console.WriteLine("Select menu items (by one or num separated by ','), press 'q' to finish order: ");

                    string selection = Console.ReadLine();
                    if (selection.ToLower() == "q") orderNotFinished = false;
                    else
                    {
                        try
                        {
                            selectedIndexes = selection.Split(',')
                                .Select(number => int.Parse(number) - 1)
                                .Where(index => index >= 0 && index < menu.Count)
                                .ToList();
                        }
                        catch
                        {
                            Console.WriteLine("Wrong input!");
                            Thread.Sleep(2000);
                        }
                        OrderedItems.AddRange(selectedIndexes);
                    }
                }

                Console.Clear();
                Console.WriteLine("Order registered successfully.");
                Thread.Sleep(3000);
            }

            if (!tableOrders.ContainsKey(selectedTable))
            {
                tableOrders[selectedTable] = new List<Order>();
            }
            tableOrders[selectedTable].AddRange(orders);
        }

        // method to make table vacant at 'MaketableVacant' option selection
        public void MarkTableAsVacant()
        {
            TablePrinter();
            Console.Write("Enter the table number to mark as vacant: ");
            bool isNr = int.TryParse(Console.ReadLine(), out int tableNumber);

            Table selectedTable = tables.FirstOrDefault(table => table.TableNumber == tableNumber);
            if (selectedTable != null && selectedTable.IsOccupied && isNr)
            {
                selectedTable.IsOccupied = false;
                Console.WriteLine($"Table {tableNumber} marked as vacant.");
            }
            else if (selectedTable != null && !selectedTable.IsOccupied && isNr)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Selected table {selectedTable.TableNumber} is vacant!");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(3000);
            }
            else if (!isNr)
            {
                Console.WriteLine("Invalid table number.");
            }
            Console.ReadKey();
        }

        // method to mark talbe vacant on checkout
        public void MarkTableAsVacant(Table selectedTable)
        {
            if (selectedTable != null && selectedTable.IsOccupied)
            {
                selectedTable.IsOccupied = false;
                Console.WriteLine($"Table {selectedTable.TableNumber} marked as vacant.");
            }
            else if (selectedTable != null && !selectedTable.IsOccupied)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Selected table {selectedTable.TableNumber} is vacant!");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(3000);
            }
        }

        public void Checkout()
        {
            Console.WriteLine("Select table to print a checque to: ");

            TablePrinter();

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
                        MarkTableAsVacant(selectedTable);
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

            Console.ReadKey();
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
        public void TablePrinter()
        {
            foreach (Table table in tables)
            {
                if (!table.IsOccupied)
                {
                    Console.WriteLine($"Table {table.TableNumber}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Table {table.TableNumber}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        private List<Menu> LoadMenu(string menuFilePath)
        {
            // Load menu items from a CSV or text file
            List<Menu> menu = new List<Menu>();

            try
            {
                string[] menuLines = File.ReadAllLines(menuFilePath);

                // i begins with 1 to skip header in csv
                for (int i = 1; i < menuLines.Length; i++)
                {
                    string line = menuLines[i];
                    string[] parts = line.Split(',');

                    if (parts.Length == 3)
                    {
                        string category = parts[0];
                        string name = parts[1];
                        double itemPrice = double.Parse(parts[2]);

                        Menu menuItem = new Menu(category, name, itemPrice);
                        menu.Add(menuItem);
                    }
                    else { Console.WriteLine("This item in menu file is in wrong fomrat!"); }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Menu file not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while loading the menu: " + ex.Message);
            }

            return menu;
        }

        public void LoadMenu(List<Menu> menu)
        {
            // somehow to send a parameter here to move in menu windows displaying main dishes in one, dessserts in another and etc with arrowkeys

            Console.WriteLine("Menu Items2:");
            for (int i = 0; i < menu.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menu[i].Name} - {menu[i].Price}");
            }
        }

        private List<Table> LoadTables(string tableFilePath)
        {
            List<Table> tables = new List<Table>();

            try
            {
                string[] tableLines = File.ReadAllLines(tableFilePath);

                foreach (string line in tableLines)
                {
                    if (int.TryParse(line, out int tableNumber))
                    {
                        Table table = new Table(tableNumber);
                        tables.Add(table);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Table file not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while loading tables: " + ex.Message);
            }

            return tables;
        }
    }
}
