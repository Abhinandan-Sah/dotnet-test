namespace OrderProcessing_System.Delegates
{
    // Custom delegate for order status change notifications
    // Takes Order and new OrderStatus as parameters
    public delegate void OrderStatusChangedDelegate(Models.Order order, OrderStatus newStatus);
}
