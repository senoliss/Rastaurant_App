using System.Threading.Channels;

namespace Rastaurant_App
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Restaurant Servicer";
            Console.WindowWidth = 80;
            Console.WindowHeight = 25;
            Console.BufferWidth = 80;
            Console.BufferHeight = 25;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            string[] menuItems = { "1. Register Order", "2. Mark Table as vacant", "3. Checkout", "4. Exit" };
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
                DrawMenu("Restaurant Service Application", menuItems, selectedIndex);

                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = Math.Max(0, selectedIndex - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = Math.Min(menuItems.Length - 1, selectedIndex + 1);
                        break;
                    case ConsoleKey.Enter:
                        HandleMenuItemSelection(Int32.Parse(menuItems[selectedIndex].Split('.')[0]));
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

        static void DrawMenu(string title, string[] options, int selectedIndex)
        {
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Draw the title
            int titleX = (windowWidth - title.Length) / 2;
            Console.SetCursorPosition(titleX, 2);
            Console.WriteLine(title);

            // Draw the menu options
            for (int i = 0; i < options.Length; i++)
            {
                int optionX = (windowWidth - options[i].Length) / 2;
                int optionY = 7 + i;

                Console.SetCursorPosition(optionX, optionY);

                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(options[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write(options[i]);
                }
            }

            // Draw the borders
            for (int i = 0; i < windowWidth; i++)
            {
                Console.SetCursorPosition(i, 4);
                Console.Write("─");
                Console.SetCursorPosition(i, windowHeight - 2);
                Console.Write("─");
            }
            for (int i = 5; i < windowHeight - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("│");
                Console.SetCursorPosition(windowWidth - 1, i);
                Console.Write("│");
            }

            Console.SetCursorPosition(0, 4);
            Console.Write("┌");
            Console.SetCursorPosition(windowWidth - 1, 4);
            Console.Write("┐");
            Console.SetCursorPosition(0, windowHeight - 2);
            Console.Write("└");
            Console.SetCursorPosition(windowWidth - 1, windowHeight - 2);
            Console.Write("┘");
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