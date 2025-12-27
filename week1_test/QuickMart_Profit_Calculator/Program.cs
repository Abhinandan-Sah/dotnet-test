namespace QuickMart_Profit_Calculator
{
    public class Program
    {
        

        public static void Main()
        {
            // Entry point for the QuickMart Profit Calculator application Problem
            int inp;
            do
            {
                Console.WriteLine("=========Menu Options==========");
                Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");
                while (!int.TryParse(Console.ReadLine(), out inp))
                {
                    Console.WriteLine("Invalid Input. Enter valid input.");
                }

                if (inp < 1 || inp > 4)
                {
                    Console.WriteLine("Invalid Input. Enter number from 1 to 4");
                }
                else
                {
                    if (inp == 1)
                    {
                        Console.Write("Enter Invoice No: ");
                        string? InvoiceNo = Console.ReadLine();

                        Console.Write("Enter Customer Name: ");
                        string? CustomerName = Console.ReadLine();

                        Console.Write("Enter Item Name: ");
                        string? ItemName = Console.ReadLine();
                        
                        Console.Write("Enter Quantity: ");
                        int Quantity;
                        while (!int.TryParse(Console.ReadLine(), out Quantity))
                        {
                            Console.WriteLine("Invalid Quantity. Enter valid Quantity");
                        }
                        Console.Write("Enter Purchase Amount (total): ");
                        decimal PurchaseAmount;
                        while (!decimal.TryParse(Console.ReadLine(), out PurchaseAmount))
                        {
                            Console.WriteLine("Invalid PurchaseAmount. Enter valid PurchaseAmount");
                        }
                        Console.Write("Enter Selling Amount (total): ");
                        decimal SellingAmount;
                        while (!decimal.TryParse(Console.ReadLine(), out SellingAmount))
                        {
                            Console.WriteLine("Invalid SellingAmount. Enter valid SellingAmount");
                        }

                        SaleTransaction saleTransaction = new SaleTransaction(InvoiceNo, CustomerName, ItemName, Quantity, PurchaseAmount, SellingAmount);
                        saleTransaction.CreateTransaction();
                    }
                    else if (inp == 2)
                    {

                    }
                    else if (inp == 3)
                    {

                    }
                }


            } while (inp != 4);
            Console.WriteLine("Thanks for Visiting.");
        }
    }
}
