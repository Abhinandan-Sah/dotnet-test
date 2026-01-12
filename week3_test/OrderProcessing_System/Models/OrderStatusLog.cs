namespace OrderProcessing_System.Models
{
    // Class representing a log entry for order status changes
    public class OrderStatusLog
    {
        public OrderStatus OldStatus { get; private set; } // Status before the change
        public OrderStatus NewStatus { get; private set; } // Status after the change
        public DateTime Timestamp { get; private set; } // Time of the status change
        public string Message { get; private set; } // Optional message for the status change

        // Constructor
        public OrderStatusLog(OrderStatus oldStatus, OrderStatus newStatus, DateTime timestamp, string message = "")
        {
            OldStatus = oldStatus;
            NewStatus = newStatus;
            Timestamp = timestamp;
            Message = message;
        }
    }
}