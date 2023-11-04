namespace Rastaurant_App
{
	internal class Program
	{
		static void Main(string[] args)
		{
            RestaurantService restaurant = new RestaurantService();

            while (true)
            {
                Console.WriteLine("Restaurant Service Menu:");
                Console.WriteLine("1. Register Order");
                Console.WriteLine("2. Mark Table as Vacant");
                Console.WriteLine("3. Exit");

                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        restaurant.RegisterOrder();
                        break;
                    case "2":
                        restaurant.MarkTableAsVacant();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
	}
}