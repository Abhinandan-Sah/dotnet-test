namespace week2_test
{
    // helper class for calculations
    public static class Calculation
    {
        // simple math - income minus expense = what's left
        public static decimal CalculateNetTotal(decimal totalIncome, decimal totalExpense)
        {
            return totalIncome - totalExpense;
        }
    }
}