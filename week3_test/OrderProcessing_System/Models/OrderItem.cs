namespace OrderProcessing_System.Models
{
    public class OrderItem
    {
        public Product Product { get; } // Product associated with the order item
        public int Quantity { get; } // Quantity of the product ordered

        // Constructor
        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        // Method to calculate total price for the order item
        public decimal GetTotal()
        {
            return Product.GetPrice() * Quantity;
        }
    }
}