using OrderProcessing_System.Models;

namespace OrderProcessing_System.Data
{
    // Static repository for managing customers
    public static class CustomerRepository
    {
        // Dictionary for fast lookup by customer ID
        public static Dictionary<int, Customer> Customers { get; private set; } = new Dictionary<int, Customer>();

        // Add a customer
        public static void Add(Customer customer)
        {
            if (!Customers.ContainsKey(customer.Id))
            {
                Customers[customer.Id] = customer;
            }
        }

        // Get customer by ID
        public static Customer? GetById(int id)
        {
            return Customers.ContainsKey(id) ? Customers[id] : null;
        }

        // Clear all customers
        public static void Clear()
        {
            Customers.Clear();
        }

        // Get all customers
        public static List<Customer> GetAll()
        {
            return Customers.Values.ToList();
        }
    }
}
