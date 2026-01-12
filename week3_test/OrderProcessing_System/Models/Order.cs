namespace OrderProcessing_System.Models
{
    public class Order
    {
        public int OrderId { get; private set; }
        public Customer Customer { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        
        // Status history log
        public List<OrderStatusLog> StatusHistory { get; private set; }

        // Pricing fields
        private decimal _discountPercentage;
        private decimal _taxPercentage;

        // Constructor
        public Order(int orderId, Customer customer, decimal discountPercentage = 0, decimal taxPercentage = 10)
        {
            OrderId = orderId;
            Customer = customer;
            Items = new List<OrderItem>();
            Status = OrderStatus.Created;
            CreatedAt = DateTime.Now;
            StatusHistory = new List<OrderStatusLog>();
            _discountPercentage = discountPercentage;
            _taxPercentage = taxPercentage;
            
            // Log initial status
            StatusHistory.Add(new OrderStatusLog(OrderStatus.Created, OrderStatus.Created, CreatedAt, "Order created"));
        }

        // Add item to order (composition)
        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        // Calculate subtotal (before discount and tax)
        public decimal GetSubtotal()
        {
            return Items.Sum(item => item.GetTotal());
        }

        // Calculate discount amount
        public decimal GetDiscountAmount()
        {
            return GetSubtotal() * (_discountPercentage / 100);
        }

        // Calculate subtotal after discount
        public decimal GetSubtotalAfterDiscount()
        {
            return GetSubtotal() - GetDiscountAmount();
        }

        // Calculate tax amount
        public decimal GetTaxAmount()
        {
            return GetSubtotalAfterDiscount() * (_taxPercentage / 100);
        }

        // Calculate total amount (with discount and tax)
        public decimal GetTotalAmount()
        {
            return GetSubtotalAfterDiscount() + GetTaxAmount();
        }

        // Change order status with validation
        public bool ChangeStatus(OrderStatus newStatus, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validate status transition
            if (!IsValidTransition(Status, newStatus, out errorMessage))
            {
                return false;
            }

            OrderStatus oldStatus = Status;
            Status = newStatus;
            UpdatedAt = DateTime.Now;

            // Log status change
            StatusHistory.Add(new OrderStatusLog(oldStatus, newStatus, UpdatedAt.Value, $"Status changed from {oldStatus} to {newStatus}"));

            return true;
        }

        // Validate if status transition is allowed
        private bool IsValidTransition(OrderStatus from, OrderStatus to, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Cannot progress cancelled orders
            if (from == OrderStatus.Cancelled)
            {
                errorMessage = "❌ Cannot change status of cancelled orders";
                return false;
            }

            // Cannot ship before paid
            if (to == OrderStatus.Shipped && from != OrderStatus.Packed)
            {
                errorMessage = "❌ Cannot ship order. Order must be Packed first";
                return false;
            }

            // Cannot pack before paid
            if (to == OrderStatus.Packed && from != OrderStatus.Paid)
            {
                errorMessage = "❌ Cannot pack order. Order must be Paid first";
                return false;
            }

            // Cannot deliver before shipped
            if (to == OrderStatus.Delivered && from != OrderStatus.Shipped)
            {
                errorMessage = "❌ Cannot deliver order. Order must be Shipped first";
                return false;
            }

            // Valid transition rules
            var validTransitions = new Dictionary<OrderStatus, List<OrderStatus>>
            {
                { OrderStatus.Created, new List<OrderStatus> { OrderStatus.Paid, OrderStatus.Cancelled } },
                { OrderStatus.Paid, new List<OrderStatus> { OrderStatus.Packed, OrderStatus.Cancelled } },
                { OrderStatus.Packed, new List<OrderStatus> { OrderStatus.Shipped, OrderStatus.Cancelled } },
                { OrderStatus.Shipped, new List<OrderStatus> { OrderStatus.Delivered, OrderStatus.Cancelled } },
                { OrderStatus.Delivered, new List<OrderStatus>() }, // Terminal state
                { OrderStatus.Cancelled, new List<OrderStatus>() } // Terminal state
            };

            if (!validTransitions[from].Contains(to))
            {
                errorMessage = $"❌ Invalid status transition from {from} to {to}";
                return false;
            }

            return true;
        }
    }
}