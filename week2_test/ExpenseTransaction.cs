namespace week2_test
{
    // different types of expenses we can track
        public enum ExpenseCategory
    {
        Office,     // pens, paper, stuff like that
        Travel,     // taxi, bus fare etc
        Food,       // snacks, tea, lunch
        Utilities,  // electricity bills maybe?
        Misc        // random stuff that doesn't fit anywhere
    }
    
    // this is for money going OUT - expenses
      public class ExpenseTransaction : Transaction
    {
        public ExpenseCategory Category { get; set; }  // what kind of expense

        // setting up expense with all the details
        public ExpenseTransaction(int id, DateTime date, decimal amount, string description, ExpenseCategory category)
            : base(id, date, amount, description)
        {
            Category = category;
        }

        // prints out the expense info
        public override void GetSummary()
        {
            Console.WriteLine($"Expense - ID: {Id}, Date: {Date}, Amount: {Amount}, Description: {Description}, Category: {Category}");
        }
    }
}