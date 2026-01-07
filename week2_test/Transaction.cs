using System;
using System.Collections.Generic;
using System.Text;

namespace week2_test
{

    public enum ExpenseCategory
    {
        Office,
        Travel,
        Food,
        Utilities,
        Misc
    }

    public enum IncomeSource
    {
        Cash,
        BankTransfer,
        UPI,
        Cheque,
        Other
    }

    interface IReportable
    {
        void GetSummary();
    }

    abstract class Transaction : IReportable
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        protected Transaction(int id, DateTime date, decimal amount, string description)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Description = description;
        }

        public abstract void GetSummary();
    }

    public class ExpenseTransaction : Transaction
    {
        public ExpenseCategory Category { get; set; }

        public ExpenseTransaction(int id, DateTime date, decimal amount, string description, ExpenseCategory category)
            : base(id, date, amount, description)
        {
            Category = category;
        }

        public override void GetSummary()
        {
            Console.WriteLine($"Expense - ID: {Id}, Date: {Date}, Amount: {Amount}, Description: {Description}, Category: {Category}");
        }
    }

    public class IncomeTransaction : Transaction
    {
        public IncomeSource Source { get; set; }

        public IncomeTransaction(int id, DateTime date, decimal amount, string description, IncomeSource source)
            : base(id, date, amount, description)
        {
            Source = source;
        }

        public override void GetSummary()
        {
            Console.WriteLine($"Income - ID: {Id}, Date: {Date}, Amount: {Amount}, Description: {Description}, Source: {Source}");
        }
    }

}
