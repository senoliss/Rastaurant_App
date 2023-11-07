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
        private List<Order> orders;

        public RestaurantService()
        {
            // Initialize data from files (e.g., menu.csv, tables.txt)
            menu = LoadMenu("Files/Menu.csv");
            tables = LoadTables("Files/Tables.txt");
            orders = new List<Order>();
        }

        public void RegisterOrder()
        {
            List<string> strings = new List<string>();
            int selectedIndex = 0;
            //Console.WriteLine("Available Tables:");
            foreach (Table table in tables)
            {
                strings.Add("Table " + table.TableNumber.ToString());

                //if (!table.IsOccupied)
                //{
                //    Console.WriteLine($"Table {table.TableNumber}");
                //}
            }
            string[] tables2 = strings.ToArray();
            Drawer.DrawMenu("Unocupied tables", tables2, selectedIndex);

            //Console.Write("Enter table number: ");
            int tableNumber = int.Parse(Console.ReadLine());

            Table selectedTable = tables.FirstOrDefault(table => table.TableNumber == tableNumber);
            if (selectedTable != null && !selectedTable.IsOccupied)
            {
                Console.WriteLine("Menu Items:");
                for (int i = 0; i < menu.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {menu[i].Name} - {menu[i].Price}");
                }

                Console.Write("Select menu item (by number) ↑ ↓: ");
                int menuItemNumber = int.Parse(Console.ReadLine()) - 1;

                if (menuItemNumber >= 0 && menuItemNumber < menu.Count)
                {
                    Menu selectedItem = menu[menuItemNumber];
                    Order order = new Order(selectedTable.TableNumber, selectedItem, DateTime.Now);
                    orders.Add(order);
                    selectedTable.IsOccupied = true;
                    Console.WriteLine("Order registered successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid menu item selection.");
                }
            }
            else
            {
                Console.WriteLine("Invalid table number or table is already occupied.");
            }
        }

        public void MarkTableAsVacant()
        {
            Console.Write("Enter the table number to mark as vacant: ");
            int tableNumber = int.Parse(Console.ReadLine());

            Table selectedTable = tables.FirstOrDefault(table => table.TableNumber == tableNumber);
            if (selectedTable != null && selectedTable.IsOccupied)
            {
                selectedTable.IsOccupied = false;
                Console.WriteLine($"Table {tableNumber} marked as vacant.");
            }
            else
            {
                Console.WriteLine("Invalid table number or table is not occupied.");
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

                // old script where csv file didn't had header lines
                //foreach (string line in menuLines)
                //{
                //    string[] parts = line.Split(',');
                //    if (parts.Length == 2)
                //    {
                //        string itemName = parts[0].Trim();
                //        double itemPrice = double.Parse(parts[1].Trim());
                //        Menu menuItem = new Menu(itemName, itemPrice);
                //        menu.Add(menuItem);
                //    }
                //}
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
            //Table table1 = new Table(1);
            //Table table2 = new Table(2);
            //Table table3 = new Table(3);
            //Table table4 = new Table(4);
            //Table table5 = new Table(5);

            //tables.Add(table1);
            //tables.Add(table2);
            //tables.Add(table3);
            //tables.Add(table4);
            //tables.Add(table5);

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
