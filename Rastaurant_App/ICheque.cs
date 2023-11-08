using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rastaurant_App
{
    public interface ICheque
    {
        public void Checkout(Table selectedTable, List<Table> tables, Dictionary<Table, List<Order>> tableOrders);
    }
}
