using OrderProcessing_System.Models;

namespace OrderProcessing_System.Services
{
    // Service to handle logistics notifications
    public class LogisticsNotification
    {
        // Collection to store notification messages for audit
        public List<string> NotificationLog { get; private set; }

        public LogisticsNotification()
        {
            NotificationLog = new List<string>();
        }

        // Method to send notification to logistics team
        public void SendNotification(Order order, OrderStatus newStatus)
        {
            string message = $"📦 LOGISTICS NOTIFICATION: Order #{order.OrderId} status changed to {newStatus}.";
            
            // Add logistics-specific actions
            switch (newStatus)
            {
                case OrderStatus.Paid:
                    message += " Action Required: Prepare items for packing.";
                    break;
                case OrderStatus.Packed:
                    message += " Action Required: Ready for dispatch.";
                    break;
                case OrderStatus.Shipped:
                    message += $" Order dispatched to {order.Customer.Name}. Track shipment.";
                    break;
                case OrderStatus.Delivered:
                    message += " Delivery confirmed. Update inventory records.";
                    break;
                case OrderStatus.Cancelled:
                    message += " Action Required: Return items to inventory.";
                    break;
            }
            
            Console.WriteLine(message);
            NotificationLog.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
        }
    }
}
