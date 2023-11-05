using System.Threading.Channels;

namespace Rastaurant_App
{
	internal class Program
	{
		static void Main(string[] args)
		{
			RestaurantService restaurant = new RestaurantService();

			while (true)
			{
				Console.WriteLine(@"
					Restaurant Service Menu:
                                    1. Register Order
                                    2. Mark Table as Vacant
                                    3. Print Checkout
                                    4. Exit");

				Console.Write("Select an option: ");

				if (!Int32.TryParse(Console.ReadLine(), out int choice))
				{
					Console.WriteLine("Input is not a number!");
				}

				switch ((MenuOptions)choice)
				{
					case MenuOptions.RegisterOrder:
						restaurant.RegisterOrder();
						break;
					case MenuOptions.MarkTableAsVacant:
						restaurant.MarkTableAsVacant();
						break;
					case MenuOptions.PrintCheck:
						//Method to print checkout;
						break;
					case MenuOptions.Exit:
						Environment.Exit(0);
						break;
					default:
						Console.WriteLine("Invalid option. Please try again.");
						break;
				}
			}
		}

		public enum MenuOptions
		{
			RegisterOrder = 1,
			MarkTableAsVacant = 2,
			PrintCheck = 3,
			Exit = 4
		}
	}
}