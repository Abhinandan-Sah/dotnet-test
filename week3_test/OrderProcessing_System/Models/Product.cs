namespace OrderProcessing_System.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Constructor
        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        // Method to get the price of the product
        public decimal GetPrice()
        {
            return Price;
        }
    }
}