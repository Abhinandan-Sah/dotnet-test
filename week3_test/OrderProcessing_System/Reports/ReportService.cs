using OrderProcessing_System.Models;
using OrderProcessing_System.Data;

namespace OrderProcessing_System.Services
{
    // Service class responsible for printing reports
    public class ReportService
    {
        // Print order summary with all details
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

        // Print complete order report (summary + history)
        public void PrintCompleteOrderReport(int orderId)
        {
            PrintOrderSummary(orderId);
            PrintStatusHistory(orderId);
        }

        // Print all orders complete report
        public void PrintAllOrdersReport()
        {
            var orders = OrderRepository.GetAll();
            
            Console.WriteLine($"\n{'='*60}");
            Console.WriteLine("📊 ALL ORDERS COMPLETE REPORT");
            Console.WriteLine($"{'='*60}");
            Console.WriteLine($"Total Orders: {orders.Count}");
            Console.WriteLine($"{'='*60}");
            
            foreach (var order in orders)
            {
                PrintCompleteOrderReport(order.OrderId);
            }
        }

        // Print system statistics
        public void PrintSystemStatistics()
        {
            var orders = OrderRepository.GetAll();
            var customers = CustomerRepository.GetAll();
            var products = CatalogRepository.GetAll();

            Console.WriteLine($"\n{'='*60}");
            Console.WriteLine("📈 SYSTEM STATISTICS");
            Console.WriteLine($"{'='*60}");
            Console.WriteLine($"Total Products: {products.Count}");
            Console.WriteLine($"Total Customers: {customers.Count}");
            Console.WriteLine($"Total Orders: {orders.Count}");
            Console.WriteLine();
            
            // Orders by status
            Console.WriteLine("Orders by Status:");
            var statusGroups = orders.GroupBy(o => o.Status);
            foreach (var group in statusGroups.OrderBy(g => g.Key))
            {
                Console.WriteLine($"  {group.Key,-15} : {group.Count(),3} orders");
            }
            
            Console.WriteLine();
            
            // Financial summary
            decimal totalRevenue = orders.Sum(o => o.GetTotalAmount());
            decimal deliveredRevenue = orders.Where(o => o.Status == OrderStatus.Delivered).Sum(o => o.GetTotalAmount());
            
            Console.WriteLine("Financial Summary:");
            Console.WriteLine($"  Total Orders Value:     ${totalRevenue:F2}");
            Console.WriteLine($"  Delivered Orders Value: ${deliveredRevenue:F2}");
            Console.WriteLine($"  Pending Value:          ${totalRevenue - deliveredRevenue:F2}");
            
            Console.WriteLine($"{'='*60}");
        }
    }
}
