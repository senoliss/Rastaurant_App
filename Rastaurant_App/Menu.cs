using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rastaurant_App
{
	public class Menu
	{
        /*This class will be used to handle menu. 
		 * Menu contains Pages, which contains Food and Drink items. 
		 * Those items are divided to breakfast, lunch, dinner, snacks, desserts 
		 * and alternatively to alcohol and non acohol beverages like  
		 * tea, juice, milkshakes, lemonades, carbonatated drinks, coffee.*/

        public string Name { get; }
        public double Price { get; }

        public Menu(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
