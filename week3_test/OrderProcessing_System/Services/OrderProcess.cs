using OrderProcessing_System.Models;
using OrderProcessing_System.Delegates;
using OrderProcessing_System.Data;

namespace OrderProcessing_System.Services
{
    // Service class to handle order processing and status changes
    public class OrderProcess
    {
        // Multicast delegate for status change notifications
        public OrderStatusChangedDelegate? OnStatusChanged { get; set; }

        // Change order status with validation and notifications
        public bool ChangeOrderStatus(int orderId, OrderStatus newStatus)
        {
            // Get order from repository
            var order = OrderRepository.GetById(orderId);
            
            if (order == null)
            {
                Console.WriteLine($"❌ Error: Order #{orderId} not found");
                return false;
            }

            Console.WriteLine($"\n🔄 Attempting to change Order #{orderId} from {order.Status} to {newStatus}...");

            // Attempt to change status
            if (order.ChangeStatus(newStatus, out string errorMessage))
            {
                Console.WriteLine($"✅ Order #{orderId} status successfully changed to {newStatus}");
                
                // Trigger delegate notifications (multicast)
                OnStatusChanged?.Invoke(order, newStatus);
                
                return true;
            }
            else
            {
                Console.WriteLine(errorMessage);
                return false;
            }
        }

        // Process multiple status changes for an order (workflow)
        public void ProcessOrderWorkflow(int orderId, List<OrderStatus> statusSequence)
        {
            Console.WriteLine($"\n{'='*60}");
            Console.WriteLine($"🚀 Processing workflow for Order #{orderId}");
            Console.WriteLine($"{'='*60}");

            foreach (var status in statusSequence)
            {
                ChangeOrderStatus(orderId, status);
                Thread.Sleep(500); // Simulate processing time
            }
        }

        // Print order summary
        public void PrintOrderSummary(int orderId)
        {
            var order = OrderRepository.GetById(orderId);
            
            if (order == null)
            {
                Console.WriteLine($"❌ Order #{orderId} not found");
                return;
            }

            Console.WriteLine($"\n{'='*60}");
            Console.WriteLine($"📋 ORDER SUMMARY - Order #{order.OrderId}");
            Console.WriteLine($"{'='*60}");
            Console.WriteLine($"Customer: {order.Customer.Name} ({order.Customer.Email})");
            Console.WriteLine($"Order Date: {order.CreatedAt:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Current Status: {order.Status}");
            Console.WriteLine($"\nItems:");
            
            foreach (var item in order.Items)
            {
                Console.WriteLine($"  - {item.Product.Name} x {item.Quantity} @ ${item.Product.Price:F2} = ${item.GetTotal():F2}");
            }
            
            Console.WriteLine($"\n  Subtotal: ${order.GetSubtotal():F2}");
            Console.WriteLine($"  Discount: -${order.GetDiscountAmount():F2}");
            Console.WriteLine($"  Subtotal After Discount: ${order.GetSubtotalAfterDiscount():F2}");
            Console.WriteLine($"  Tax: +${order.GetTaxAmount():F2}");
            Console.WriteLine($"  {'─'*40}");
            Console.WriteLine($"  Total Amount: ${order.GetTotalAmount():F2}");
            Console.WriteLine($"{'='*60}");
        }

        // Print status history timeline
        public void PrintStatusHistory(int orderId)
        {
            var order = OrderRepository.GetById(orderId);
            
            if (order == null)
            {
                Console.WriteLine($"❌ Order #{orderId} not found");
                return;
            }

            Console.WriteLine($"\n{'='*60}");
            Console.WriteLine($"📜 STATUS HISTORY TIMELINE - Order #{order.OrderId}");
            Console.WriteLine($"{'='*60}");
            
            foreach (var log in order.StatusHistory)
            {
                Console.WriteLine($"[{log.Timestamp:yyyy-MM-dd HH:mm:ss}] {log.OldStatus} → {log.NewStatus}");
                if (!string.IsNullOrEmpty(log.Message))
                {
                    Console.WriteLine($"  📝 {log.Message}");
                }
            }
            
            Console.WriteLine($"{'='*60}");
        }

        // Print all orders grouped by customer
        public void PrintOrdersByCustomer()
        {
            Console.WriteLine($"\n{'='*60}");
            Console.WriteLine("📊 ORDERS BY CUSTOMER");
            Console.WriteLine($"{'='*60}");

            var customers = CustomerRepository.GetAll();
            
            foreach (var customer in customers)
            {
                var customerOrders = OrderRepository.GetByCustomerId(customer.Id);
                
                if (customerOrders.Any())
                {
                    Console.WriteLine($"\n👤 {customer.Name} ({customer.Email})");
                    Console.WriteLine($"   Total Orders: {customerOrders.Count}");
                    
                    foreach (var order in customerOrders)
                    {
                        Console.WriteLine($"   - Order #{order.OrderId}: {order.Status} | Total: ${order.GetTotalAmount():F2}");
                    }
                }
            }
            
            Console.WriteLine($"{'='*60}");
        }
    }
}
