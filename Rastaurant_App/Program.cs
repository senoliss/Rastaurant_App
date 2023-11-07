using System.Threading.Channels;

namespace Rastaurant_App
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Restaurant Servicer";
            Console.WindowWidth = 80;
            Console.WindowHeight = 24;
            Console.BufferWidth = 80;
            Console.BufferHeight = 24;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            string[] optionItems = { "1. Register Order", "2. Mark Table as vacant", "3. Checkout", "4. Exit" };
            int selectedIndex = 0;

            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.Col
            //            Console.WriteLine(@"
            //┌─────────────────────────────────────────┐
            //│     Restaurant Service Application      │
            //├─────────────────────────────────────────┤
            //│                                         │
            //│   Select an option:                     │
            //│    1.Register Order                     │
            //│    2.Mark Table as vacant               │
            //│    3.Checkout                           │
            //│    4.Exit                               │
            //│                                         │
            //│                                         │
            //│                                         │
            //│                                         │
            //│                                         │
            //│                                         │
            //│                                         │
            //├─────────────────────────────────────────┤
            //└─────────────────────────────────────────┘");

            while (true)
            {
                Console.Clear();
                Drawer.DrawMenu("Restaurant Service Application", optionItems, selectedIndex);

                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = Math.Max(0, selectedIndex - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = Math.Min(optionItems.Length - 1, selectedIndex + 1);
                        break;
                    case ConsoleKey.Enter:
                        HandleMenuItemSelection(Int32.Parse(optionItems[selectedIndex].Split('.')[0]));
                        break;
                }
                //            Console.WriteLine(@"
                //	Restaurant Service Menu:
                //                                1. Register Order
                //                                2. Mark Table as Vacant
                //                                3. Print Checkout
                //                                4. Exit");

                //Console.Write("Select an option: ");

                //            if (!Int32.TryParse(Console.ReadLine(), out int choice))
                //            {
                //                Console.WriteLine("Input is not a number!");
                //            }

                //            switch ((MenuOptions)choice)
                //{
                //	case MenuOptions.RegisterOrder:
                //		restaurant.RegisterOrder();
                //		break;
                //	case MenuOptions.MarkTableAsVacant:
                //		restaurant.MarkTableAsVacant();
                //		break;
                //	case MenuOptions.PrintCheck:
                //		//Method to print checkout;
                //		break;
                //	case MenuOptions.Exit:
                //		Environment.Exit(0);
                //		break;
                //	default:
                //		Console.WriteLine("Invalid option. Please try again.");
                //		break;
                //}

                //if((MenuOptions)choice == MenuOptions.RegisterOrder) restaurant.RegisterOrder();
                //if((MenuOptions)choice == MenuOptions.MarkTableAsVacant) restaurant.MarkTableAsVacant();
                //if((MenuOptions)choice == MenuOptions.PrintCheck) restaurant.MarkTableAsVacant();
                //if((MenuOptions)choice == MenuOptions.Exit) Environment.Exit(0);

            }
        }

		public enum MenuOptions
		{
			RegisterOrder = 1,
			MarkTableAsVacant = 2,
			PrintCheck = 3,
			Exit = 4
		}

       
        static void HandleMenuItemSelection(int choice)
        {
            RestaurantService restaurant = new RestaurantService();
            Console.Clear();
            // Add logic to handle the selected menu item.
            Console.WriteLine($"Selected option: {choice}");
            if ((MenuOptions)choice == MenuOptions.RegisterOrder) restaurant.RegisterOrder();
            if ((MenuOptions)choice == MenuOptions.MarkTableAsVacant) restaurant.MarkTableAsVacant();
            if ((MenuOptions)choice == MenuOptions.PrintCheck) restaurant.MarkTableAsVacant();
            if ((MenuOptions)choice == MenuOptions.Exit) Environment.Exit(0);
            Console.ReadKey();
        }
    }
}