using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rastaurant_App
{
	public class Table
	{
        /* Here we eill have a Table class which will hande one table as an object 
         * and check it's number and occupancy. 
         * This data might be loaded from a file in Restaurant handling class.*/
        public int TableNumber { get; }
        public bool IsOccupied { get; set; }

        public Table(int tableNumber)
        {
            TableNumber = tableNumber;
            IsOccupied = false;
        }
    }
}
