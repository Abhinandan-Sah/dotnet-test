namespace week2_test
{
    public class Program
    {
        public static void Main()
        {
            // creating two separate ledgers - one for income, one for expenses
            Ledger<IncomeTransaction> incomeLedger = new Ledger<IncomeTransaction>();
            Ledger<ExpenseTransaction> expenseLedger = new Ledger<ExpenseTransaction>();

            // keep running until user exits
            while (true)
            {
                // show menu
                Console.WriteLine("Digital Petty Cash Ledger Application");
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Add Income");
                Console.WriteLine("2. Add Expense");
                Console.WriteLine("3. Show Totals");
                Console.WriteLine("4. Show All Transactions");
                Console.WriteLine("5. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:// Add income
                        // get all the details for new income
                        Console.WriteLine("Enter income transaction Id: ");
                        int IncomeId = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Amount: ");
                        decimal IncomeAmount = decimal.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Description: ");
                        string? incDesc = Console.ReadLine();

                        Console.WriteLine("Select Source: 0-Cash, 1-BankTransfer, 2-UPI, 3-Cheque, 4-Other");
                        IncomeSource source = (IncomeSource)int.Parse(Console.ReadLine());
                        
                        // create new income transaction and add it
                        incomeLedger.AddEntry(
                            new IncomeTransaction(
                                IncomeId,
                                DateTime.Now,
                                IncomeAmount,
                                incDesc,
                                source
                            )
                        );
                        break;
                    case 2:// Add Expense
                        // same thing but for expenses
                        Console.WriteLine("Enter expense transaction Id: ");
                        int expenseId = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Amount: ");
                        decimal expAmount = decimal.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Description: ");
                        string? expDesc = Console.ReadLine();

                        Console.WriteLine("Select Category: 0-Office, 1-Travel, 2-Food, 3-Utilities, 4-Misc");
                        ExpenseCategory category = (ExpenseCategory)int.Parse(Console.ReadLine());

                        // make new expense and add it
                        expenseLedger.AddEntry(
                            new ExpenseTransaction(
                                expenseId, 
                                DateTime.Now, 
                                expAmount,
                                expDesc,
                                category
                            )
                        );

                        break;
                    case 3:// show Totals
                        // calculate everything
                        decimal totalIncome = incomeLedger.CalculateTotal();
                        decimal totalExpense = expenseLedger.CalculateTotal();
                        decimal netTotal = Calculation.CalculateNetTotal(totalIncome, totalExpense);
                        Console.WriteLine($"Total Income: {totalIncome}");
                        Console.WriteLine($"Total Expense: {totalExpense}");
                        Console.WriteLine($"Net Total: {netTotal}");  // how much is left
                        break;

                    case 4:// Show All Transactions 
                        // print all incomes
                        Console.WriteLine("Income Transactions:");
                        foreach (var income in incomeLedger.GetTransactions())
                        {
                            income.GetSummary();
                        }
                        // print all expenses
                        Console.WriteLine("Expense Transactions:");
                        foreach (var expense in expenseLedger.GetTransactions())
                        {
                            expense.GetSummary();
                        }

                        break;
                    case 5://  exit
                        Console.WriteLine("Exiting Thank for using the ledger application.");
                        return;  // exits the program

                    default:// invalid/out of range input 
                        Console.WriteLine("Invalid/out of range input ");
                        break;


                }

            }
        }
    }
}