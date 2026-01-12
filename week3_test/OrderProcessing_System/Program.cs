using OrderProcessing_System.Data;
using OrderProcessing_System.Services;
using OrderProcessing_System.Models;

// ═══════════════════════════════════════════════════════════════════════════
// 🏪 ONLINE ORDER PROCESSING & STATUS NOTIFICATIONS SYSTEM
// ═══════════════════════════════════════════════════════════════════════════
// This application demonstrates:
// - OOPS: Encapsulation, Composition, Polymorphism
// - Generic Collections: Dictionary<K,V>, List<T>
// - Delegates: Multicast delegates for notifications
// - Business Rules: Status transition validation
// ═══════════════════════════════════════════════════════════════════════════

Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
Console.WriteLine("║   🏪 ONLINE ORDER PROCESSING & STATUS NOTIFICATIONS SYSTEM   ║");
Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝\n");

// ─────────────────────────────────────────────────────────────────────────
// STEP 1: SEED SAMPLE DATA (5 Products, 3 Customers, 4 Orders)
// ─────────────────────────────────────────────────────────────────────────
Console.WriteLine("📦 Loading sample data...\n");
SampleDataSeeder.SeedCatalog();
var orders = SampleDataSeeder.SeedOrdersFromRepos();

Console.WriteLine("✅ Data loaded successfully!");
Console.WriteLine($"   - Products: {CatalogRepository.GetAll().Count}");
Console.WriteLine($"   - Customers: {CustomerRepository.GetAll().Count}");
Console.WriteLine($"   - Orders: {OrderRepository.GetAll().Count}\n");

// ─────────────────────────────────────────────────────────────────────────
// STEP 2: CREATE NOTIFICATION SERVICES & REPORT SERVICE
// ─────────────────────────────────────────────────────────────────────────
CustomerNotification customerNotification = new CustomerNotification();
LogisticsNotification logisticsNotification = new LogisticsNotification();
ReportService reportService = new ReportService(); // Report service for printing

// ─────────────────────────────────────────────────────────────────────────
// STEP 3: CREATE ORDER PROCESSING SERVICE & ATTACH DELEGATES (MULTICAST)
// ─────────────────────────────────────────────────────────────────────────
OrderProcess orderProcess = new OrderProcess();

// Attach multiple notification handlers to the delegate (multicast)
orderProcess.OnStatusChanged += customerNotification.SendNotification;
orderProcess.OnStatusChanged += logisticsNotification.SendNotification;

Console.WriteLine("🔗 Delegates attached: Customer & Logistics notifications enabled\n");

// ─────────────────────────────────────────────────────────────────────────
// STEP 4: DEMONSTRATE ORDER PROCESSING WITH VALID WORKFLOW
// ─────────────────────────────────────────────────────────────────────────
Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════╗");
Console.WriteLine("║               🚀 ORDER #1001 - VALID WORKFLOW                 ║");
Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");

reportService.PrintOrderSummary(1001);

// Process Order #1001 through complete workflow
orderProcess.ProcessOrderWorkflow(1001, new List<OrderStatus>
{
    OrderStatus.Paid,
    OrderStatus.Packed,
    OrderStatus.Shipped,
    OrderStatus.Delivered
});

reportService.PrintStatusHistory(1001);

// ─────────────────────────────────────────────────────────────────────────
// STEP 5: DEMONSTRATE INVALID STATUS TRANSITIONS (BUSINESS RULES)
// ─────────────────────────────────────────────────────────────────────────
Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════╗");
Console.WriteLine("║          ⚠️  ORDER #1002 - INVALID TRANSITIONS DEMO          ║");
Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");

reportService.PrintOrderSummary(1002);

// Try to ship before paying (should fail)
orderProcess.ChangeOrderStatus(1002, OrderStatus.Shipped);

// Try to deliver before shipping (should fail)
orderProcess.ChangeOrderStatus(1002, OrderStatus.Delivered);

// Valid progression
orderProcess.ProcessOrderWorkflow(1002, new List<OrderStatus>
{
    OrderStatus.Paid,
    OrderStatus.Packed,
    OrderStatus.Shipped
});

reportService.PrintStatusHistory(1002);

// ─────────────────────────────────────────────────────────────────────────
// STEP 6: DEMONSTRATE CANCELLED ORDER SCENARIO
// ─────────────────────────────────────────────────────────────────────────
Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════╗");
Console.WriteLine("║              ❌ ORDER #1003 - CANCELLATION DEMO               ║");
Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");

reportService.PrintOrderSummary(1003);

// Process payment then cancel
orderProcess.ChangeOrderStatus(1003, OrderStatus.Paid);
orderProcess.ChangeOrderStatus(1003, OrderStatus.Cancelled);

// Try to progress cancelled order (should fail)
orderProcess.ChangeOrderStatus(1003, OrderStatus.Packed);

reportService.PrintStatusHistory(1003);

// ─────────────────────────────────────────────────────────────────────────
// STEP 7: DEMONSTRATE PARTIAL WORKFLOW
// ─────────────────────────────────────────────────────────────────────────
Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════╗");
Console.WriteLine("║            📦 ORDER #1004 - PARTIAL WORKFLOW DEMO             ║");
Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");

reportService.PrintOrderSummary(1004);

// Process until packed
orderProcess.ProcessOrderWorkflow(1004, new List<OrderStatus>
{
    OrderStatus.Paid,
    OrderStatus.Packed
});

reportService.PrintStatusHistory(1004);

// ─────────────────────────────────────────────────────────────────────────
// STEP 8: PRINT FINAL REPORTS USING REPORT SERVICE
// ─────────────────────────────────────────────────────────────────────────
Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════╗");
Console.WriteLine("║                    📊 FINAL REPORTS                           ║");
Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");

reportService.PrintOrdersByCustomer();
reportService.PrintSystemStatistics();

// ─────────────────────────────────────────────────────────────────────────
// STEP 9: DEMONSTRATE GENERIC COLLECTIONS USAGE
// ─────────────────────────────────────────────────────────────────────────
Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════╗");
Console.WriteLine("║           📚 GENERIC COLLECTIONS DEMONSTRATION                ║");
Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝\n");

Console.WriteLine("🔹 Products stored in Dictionary<int, Product> for O(1) lookup:");
var product = CatalogRepository.GetById(1);
Console.WriteLine($"   GetById(1): {product?.Name} - ${product?.Price:F2}");

Console.WriteLine("\n🔹 Orders stored in Dictionary<int, Order> for O(1) lookup:");
var order = OrderRepository.GetById(1001);
Console.WriteLine($"   GetById(1001): Order #{order?.OrderId} - {order?.Status}");

Console.WriteLine("\n🔹 Order Items stored in List<OrderItem> for ordered collection:");
if (order != null)
{
    Console.WriteLine($"   Order #1001 has {order.Items.Count} items");
}

Console.WriteLine("\n🔹 Status History stored in List<OrderStatusLog> for timeline:");
if (order != null)
{
    Console.WriteLine($"   Order #1001 has {order.StatusHistory.Count} status changes logged");
}

// ─────────────────────────────────────────────────────────────────────────
// STEP 10: SUMMARY
// ─────────────────────────────────────────────────────────────────────────
Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════╗");
Console.WriteLine("║                      ✅ SUMMARY                               ║");
Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝\n");

Console.WriteLine("✓ OOPS Principles Demonstrated:");
Console.WriteLine("  • Encapsulation: Private setters, validation in Order class");
Console.WriteLine("  • Composition: Order contains List<OrderItem>");
Console.WriteLine("  • Abstraction: Services separate business logic\n");

Console.WriteLine("✓ Generic Collections Used:");
Console.WriteLine("  • Dictionary<int, Product> - Fast product lookup");
Console.WriteLine("  • Dictionary<int, Customer> - Fast customer lookup");
Console.WriteLine("  • Dictionary<int, Order> - Fast order lookup");
Console.WriteLine("  • List<OrderItem> - Ordered item collection");
Console.WriteLine("  • List<OrderStatusLog> - Status history timeline\n");

Console.WriteLine("✓ Delegates Demonstrated:");
Console.WriteLine("  • Multicast delegate: OrderStatusChangedDelegate");
Console.WriteLine("  • Multiple subscribers: CustomerNotification & LogisticsNotification");
Console.WriteLine("  • Notifications triggered automatically on status change\n");

Console.WriteLine("✓ Business Rules Enforced:");
Console.WriteLine("  • Cannot ship before paid");
Console.WriteLine("  • Cannot pack before paid");
Console.WriteLine("  • Cannot deliver before shipped");
Console.WriteLine("  • Cancelled orders cannot progress\n");

Console.WriteLine($"📧 Total Customer Notifications Sent: {customerNotification.NotificationLog.Count}");
Console.WriteLine($"📦 Total Logistics Notifications Sent: {logisticsNotification.NotificationLog.Count}\n");

Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
Console.WriteLine("║         🎉 ORDER PROCESSING SYSTEM COMPLETED SUCCESSFULLY     ║");
Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
