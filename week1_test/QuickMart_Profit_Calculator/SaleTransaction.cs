using System.Security.Cryptography.X509Certificates;

namespace QuickMart_Profit_Calculator
{
    public class SaleTransaction
    {
        public string? InvoiceNo { get; set; }
        public string? CustomerName { get; set; }
        public string? ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal SellingAmount { get; set; }

        public string? ProfitOrLossStatus { get; set; }
        public decimal ProfitOrLossAmount { get; set; }
        public  decimal ProfitMarginPercent { get; set; }

        public SaleTransaction(string InvoiceNo, string CustomerName, string ItemName, int Quantity, decimal PurchaseAmount, decimal SellingAmount)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(InvoiceNo))
                {
                    throw new ArgumentNullException("Invalid input. NULL/WhiteSpace Error");
                }
                else if (Quantity <= 0)
                {
                    throw new ArgumentNullException("Quantity mustn't be less than 1");
                }
                else if (PurchaseAmount <= 0)
                {
                    throw new ArgumentNullException("PurchaseAmount mustn't be less than 1");
                }
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

        public void CreateTransaction()
        {
            if (PurchaseAmount == SellingAmount)
            {
                ProfitOrLossStatus = "BREAK-EVEN";
                ProfitOrLossAmount=0;
            }
            else if (PurchaseAmount < SellingAmount)
            {
                ProfitOrLossStatus = "PROFIT";
                ProfitOrLossAmount=(SellingAmount-PurchaseAmount);
            }
            else
            {
                ProfitOrLossStatus="LOSS";
                ProfitOrLossAmount=(PurchaseAmount-SellingAmount);
            }

            ProfitMarginPercent = (ProfitOrLossAmount/PurchaseAmount) * 100;
            Console.WriteLine("Transaction saved successfully");
            Console.WriteLine("Status: "+ ProfitOrLossStatus);
            Console.WriteLine($"Profit/Loss Amount: {Math.Round(ProfitOrLossAmount, 2)}");
            if (ProfitOrLossStatus == "PROFIT")
            {
                Console.WriteLine($"Profit Margin (%) : {Math.Round(ProfitMarginPercent, 2)}");

            }
        }

        public static void ViewLastTransaction()
        {
        }

        public void CalculateProfitLoss()
        {

        }

    }

}