using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rastaurant_App
{
	public class Order
	{
        /*Here we will have an Order class which will handle order object for customer. 
         It should contain: 
            Customer info,
            Table info,
            Ordered items list from menu
            Time of placed order*/
        public int TableNumber { get; }
        public Menu OrderedItem { get; }
        public double TotalAmount { get; }
        public DateTime OrderDateTime { get; }

        public Order(int tableNumber, Menu orderedItem, DateTime orderDateTime)
        {
            TableNumber = tableNumber;
            OrderedItem = orderedItem;
            TotalAmount = orderedItem.Price;
            OrderDateTime = orderDateTime;
        }
    }
}
