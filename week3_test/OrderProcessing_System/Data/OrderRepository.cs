using OrderProcessing_System.Models;

namespace OrderProcessing_System.Data
{
    // Static repository for managing orders
    public static class OrderRepository
    {
        // Dictionary for fast lookup by order ID
        public static Dictionary<int, Order> Orders { get; private set; } = new Dictionary<int, Order>();

        // Add an order
        public static void Add(Order order)
        {
            if (!Orders.ContainsKey(order.OrderId))
            {
                Orders[order.OrderId] = order;
            }
        }

        // Get order by ID
        public static Order? GetById(int id)
        {
            return Orders.ContainsKey(id) ? Orders[id] : null;
        }

        // Clear all orders
        public static void Clear()
        {
            Orders.Clear();
        }

        // Get all orders
        public static List<Order> GetAll()
        {
            return Orders.Values.ToList();
        }

        // Get orders by customer
        public static List<Order> GetByCustomerId(int customerId)
        {
            return Orders.Values.Where(o => o.Customer.Id == customerId).ToList();
        }
    }
}
