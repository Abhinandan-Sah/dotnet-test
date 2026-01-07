namespace week2_test
{
    // where the money is coming from
    public enum IncomeSource
    {
        Cash,          // physical cash
        BankTransfer,  // bank transfer
        UPI,           // google pay, phonepe etc
        Cheque,        // cheque payment
        Other          // idk something else
    }
    
    // this is for money coming IN
    public class IncomeTransaction : Transaction
    {
        public IncomeSource Source { get; set; }  // where did we get the money

        // constructor for income transaction
        public IncomeTransaction(int id, DateTime date, decimal amount, string description, IncomeSource source)
            : base(id, date, amount, description)
        {
            Source = source;
        }

        // show the income details
        public override void GetSummary()
        {
            Console.WriteLine($"Income - ID: {Id}, Date: {Date}, Amount: {Amount}, Description: {Description}, Source: {Source}");
        }
    }
}