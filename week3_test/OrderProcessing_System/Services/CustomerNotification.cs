using OrderProcessing_System.Models;

namespace OrderProcessing_System.Services
{
    // Service to handle customer notifications
    public class CustomerNotification
    {
        // Collection to store notification messages for audit
        public List<string> NotificationLog { get; private set; }

        public CustomerNotification()
        {
            NotificationLog = new List<string>();
        }

        // Method to send notification to customer
        public void SendNotification(Order order, OrderStatus newStatus)
        {
            string message = $"📧 CUSTOMER NOTIFICATION: Dear {order.Customer.Name}, your order #{order.OrderId} status has been updated to {newStatus}.";
            
            // Add status-specific messages
            switch (newStatus)
            {
                case OrderStatus.Paid:
                    message += " Thank you for your payment!";
                    break;
                case OrderStatus.Packed:
                    message += " Your order is being prepared for shipment.";
                    break;
                case OrderStatus.Shipped:
                    message += " Your order is on the way!";
                    break;
                case OrderStatus.Delivered:
                    message += " Your order has been delivered. Thank you for shopping with us!";
                    break;
                case OrderStatus.Cancelled:
                    message += " Your order has been cancelled. Refund will be processed if applicable.";
                    break;
            }

            message += $" Email sent to: {order.Customer.Email}";
            
            Console.WriteLine(message);
            NotificationLog.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
        }
    }
}
