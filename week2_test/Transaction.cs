using System;
using System.Collections.Generic;
using System.Text;

namespace week2_test
{
    // just a simple interface to make sure all transactions can print summary
    interface IReportable
    {
        void GetSummary();
    }

    // base class for transactions - income and expense both inherit this
    public abstract class Transaction : IReportable
    {
        public int Id { get; set; }  // unique id for each transaction
        public DateTime Date { get; set; }  // when it happened
        public decimal Amount { get; set; }  // how much money
        public string Description { get; set; }  // what was it for

        // constructor - sets up the basic stuff all transactions need
        protected Transaction(int id, DateTime date, decimal amount, string description)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Description = description;
        }

        // each transaction type needs to implement this differently
        public abstract void GetSummary();
    }
}
