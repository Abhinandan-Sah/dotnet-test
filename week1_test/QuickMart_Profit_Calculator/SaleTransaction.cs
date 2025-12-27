using System.Security.Cryptography.X509Certificates;

namespace QuickMart_Profit_Calculator
{
    /// <summary>
    /// Represents a sales transaction with purchase and selling details
    /// Calculates profit/loss status and profit margin percentage
    /// </summary>
    public class SaleTransaction
    {
        // Transaction identification and item details
        public string? InvoiceNo { get; set; }
        public string? CustomerName { get; set; }
        public string? ItemName { get; set; }
        public int Quantity { get; set; }
        
        // Financial details
        public decimal PurchaseAmount { get; set; }
        public decimal SellingAmount { get; set; }

        // Calculated profit/loss metrics
        public string? ProfitOrLossStatus { get; set; }  // PROFIT, LOSS, or BREAK-EVEN
        public decimal ProfitOrLossAmount { get; set; }
        public  decimal ProfitMarginPercent { get; set; }
        
        // Static storage for last transaction (no arrays/lists allowed)
        public static SaleTransaction? LastTransaction { get; set; }
        public static bool HasLastTransaction {get; set;} =false;

        /// <summary>
        /// Constructor - validates inputs and initializes transaction object
        /// </summary>
        public SaleTransaction(string InvoiceNo, string CustomerName, string ItemName, int Quantity, decimal PurchaseAmount, decimal SellingAmount)
        {
            try
            {
                // Validate InvoiceNo is not empty
                if (string.IsNullOrWhiteSpace(InvoiceNo))
                {
                    throw new ArgumentNullException("Invalid input. NULL/WhiteSpace Error");
                }
                // Quantity must be at least 1
                else if (Quantity <= 0)
                {
                    throw new ArgumentNullException("Quantity mustn't be less than 1");
                }
                // Purchase amount must be positive
                else if (PurchaseAmount <= 0)
                {
                    throw new ArgumentNullException("PurchaseAmount mustn't be less than 1");
                }
                // Selling amount must be non-negative
                else if (SellingAmount < 0)
                {
                    throw new ArgumentNullException("SellingAmount mustn't be less than 0");
                }
                this.InvoiceNo = InvoiceNo;
                this.CustomerName = CustomerName;
                this.ItemName = ItemName;
                this.Quantity = Quantity;
                this.PurchaseAmount = PurchaseAmount;
                this.SellingAmount = SellingAmount;
            }
            catch (Exception err)
            {
                Console.WriteLine("Error occured. " + err);
            }

        }

        /// <summary>
        /// Computes profit/loss status, amount, and margin percentage
        /// Displays results and stores as LastTransaction
        /// </summary>
        public void CreateTransaction()
        {
            // Determine profit/loss status and calculate amount
            if (PurchaseAmount == SellingAmount)
            {
                ProfitOrLossStatus = "BREAK-EVEN";
                ProfitOrLossAmount=0;
            }
            else if (PurchaseAmount < SellingAmount)
            {
                ProfitOrLossStatus = "PROFIT";
                ProfitOrLossAmount=SellingAmount-PurchaseAmount;
            }
            else
            {
                ProfitOrLossStatus="LOSS";
                ProfitOrLossAmount=PurchaseAmount-SellingAmount;
            }

            // Calculate profit margin percentage relative to purchase amount
            ProfitMarginPercent = ProfitOrLossAmount/PurchaseAmount * 100;
            // Display transaction results
            Console.WriteLine("Transaction saved successfully");
            Console.WriteLine("Status: "+ ProfitOrLossStatus);
            Console.WriteLine($"Profit/Loss Amount: {Math.Round(ProfitOrLossAmount, 2)}");
            if (ProfitOrLossStatus == "PROFIT")
            {
                Console.WriteLine($"Profit Margin (%) : {Math.Round(ProfitMarginPercent, 2)}");

            }
            
            // Store as last transaction for later viewing
            LastTransaction = this;
            HasLastTransaction = true;

        }

        /// <summary>
        /// Displays the last created transaction details
        /// Shows message if no transaction exists
        /// </summary>
        public static void ViewLastTransaction()
        {
            // Check if a transaction exists in memory
            if (HasLastTransaction && LastTransaction != null)
            {
                Console.WriteLine("Last Transaction Details:");
                Console.WriteLine($"Invoice No: {LastTransaction.InvoiceNo}");
                Console.WriteLine($"Customer Name: {LastTransaction.CustomerName}");
                Console.WriteLine($"Item Name: {LastTransaction.ItemName}");
                Console.WriteLine($"Quantity: {LastTransaction.Quantity}");
                Console.WriteLine($"Purchase Amount: {LastTransaction.PurchaseAmount}");
                Console.WriteLine($"Selling Amount: {LastTransaction.SellingAmount}");
                Console.WriteLine($"Status: {LastTransaction.ProfitOrLossStatus}");
                Console.WriteLine($"Profit/Loss Amount: {Math.Round(LastTransaction.ProfitOrLossAmount, 2)}");
                if (LastTransaction.ProfitOrLossStatus == "PROFIT")
                {
                    Console.WriteLine($"Profit Margin (%): {Math.Round(LastTransaction.ProfitMarginPercent, 2)}");
                }
            }
            else
            {
                Console.WriteLine("No previous transaction found.");
            }
        }

        /// <summary>
        /// Recomputes and displays profit/loss details for current transaction
        /// </summary>
        public void CalculateProfitLoss()
        {
            // Display recomputed profit/loss metrics
            Console.WriteLine("Recomputed Profit/Loss Details:");
            Console.WriteLine($"Status: {ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {Math.Round(ProfitOrLossAmount, 2)}");
            if (ProfitOrLossStatus == "PROFIT")
            {
                Console.WriteLine($"Profit Margin (%): {Math.Round(ProfitMarginPercent, 2)}");
            }
        }

    }

}