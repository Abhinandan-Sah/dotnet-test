namespace OrderProcessing_System.Models
{
    public class Customer
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        // Constructor
        public Customer(int id, string name, string email, string phone = "")
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
        }
    }
}