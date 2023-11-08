using Microsoft.VisualStudio.TestPlatform.TestHost;
using Rastaurant_App;

namespace CheckoutSum_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckoutSum_Test()
        {
            // Arrange
            var restaurant = new RestaurantService();
            var selectedTable = new Table(1) { IsOccupied = true };
            var orderedItem = new Menu("Soup", "Mushroom", 2.5);
            var orderedItem2 = new Menu("Soup", "Pumpkin", 2.7);
            var now = DateTime.Now;
            List<Order> orders = new List<Order>();
            Dictionary<Table, List<Order>> tableOrders = new Dictionary<Table, List<Order>>();
            tableOrders.Add(selectedTable, orders);
            orders.Add(new Order(1, orderedItem, now));
            orders.Add(new Order(1, orderedItem2, now));



            // double expected = 13
            double exppected = 5.2;

            // act
            double actual = restaurant.CheckoutSum(tableOrders, selectedTable);

            // Assert
            Assert.AreEqual(exppected, actual);
        }
    }
}