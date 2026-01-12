using OrderProcessing_System.Models;

namespace OrderProcessing_System.Data
{
    // Static repository for managing products
    public static class CatalogRepository
    {
        // Dictionary for fast lookup by product ID
        public static Dictionary<int, Product> Products { get; private set; } = new Dictionary<int, Product>();

        // Add a product to the catalog
        public static void Add(Product product)
        {
            if (!Products.ContainsKey(product.Id))
            {
                Products[product.Id] = product;
            }
        }

        // Get product by ID
        public static Product? GetById(int id)
        {
            return Products.ContainsKey(id) ? Products[id] : null;
        }

        // Clear all products
        public static void Clear()
        {
            Products.Clear();
        }

        // Get all products
        public static List<Product> GetAll()
        {
            return Products.Values.ToList();
        }
    }
}
