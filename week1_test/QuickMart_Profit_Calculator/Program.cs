namespace QuickMart_Profit_Calculator
{
    public class Program
    {
        
        
        public static void Main()
        {
            // Entry point for the QuickMart Profit Calculator application Problem
            int inp;
            
            // Main menu loop - continues until user selects Exit option
            do
            {
                // Display menu options
                Console.WriteLine("================== QuickMart Traders ==================");
                Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");
                
                // Validate menu option input - ensure it's an integer
                while (!int.TryParse(Console.ReadLine(), out inp))
                {
                    Console.WriteLine("Invalid Input. Enter valid input.");
                }

                // Validate menu option range
                if (inp < 1 || inp > 4)
                {
                    Console.WriteLine("Invalid Input. Enter number from 1 to 4");
                }
                else
                {
                    // Option 1: Create new transaction with purchase and selling details
                    if (inp == 1)
                    {
                        // Capture transaction information
                        Console.Write("Enter Invoice No: ");
                        string? InvoiceNo = Console.ReadLine();

                        Console.Write("Enter Customer Name: ");
                        string? CustomerName = Console.ReadLine();

                        Console.Write("Enter Item Name: ");
                        string? ItemName = Console.ReadLine();
                        
                        // Validate quantity input
                        Console.Write("Enter Quantity: ");
                        int Quantity;
                        while (!int.TryParse(Console.ReadLine(), out Quantity))
                        {
                            Console.WriteLine("Invalid Quantity. Enter valid Quantity");
                        }
                        // Validate purchase amount input
                        Console.Write("Enter Purchase Amount (total): ");
                        decimal PurchaseAmount;
                        while (!decimal.TryParse(Console.ReadLine(), out PurchaseAmount))
                        {
                            Console.WriteLine("Invalid PurchaseAmount. Enter valid PurchaseAmount");
                        }
                        
                        // Validate selling amount input
                        Console.Write("Enter Selling Amount (total): ");
                        decimal SellingAmount;
                        while (!decimal.TryParse(Console.ReadLine(), out SellingAmount))
                        {
                            Console.WriteLine("Invalid SellingAmount. Enter valid SellingAmount");
                        }

                        // Create transaction object and compute profit/loss
                        SaleTransaction saleTransaction = new SaleTransaction(InvoiceNo, CustomerName, ItemName, Quantity, PurchaseAmount, SellingAmount);
                        saleTransaction.CreateTransaction();
                    }
                    // Option 2: View the last created transaction
                    else if (inp == 2)
                    {
                        SaleTransaction.ViewLastTransaction();
                    }
                    // Option 3: Recompute and display profit/loss for last transaction
                    else if (inp == 3)
                    {
                        if (SaleTransaction.HasLastTransaction && SaleTransaction.LastTransaction != null)
                        {
                            SaleTransaction.LastTransaction.CalculateProfitLoss();
                        }
                    }
                }


            } while (inp != 4);
            Console.WriteLine("Thanks for Visiting.");
        }
    }
}
