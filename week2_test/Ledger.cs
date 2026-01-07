using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO.Pipelines;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Transactions;
using System.Xml;
using System.Xml.XPath;
using Microsoft.VisualBasic;
using Transaction = week2_test.Transaction;

namespace week2_test
{
    // generic ledger - can store income or expense transactions
    // T has to be a Transaction type (or child of it)
    public class Ledger<T> where T : Transaction
    {
        private List<T> transactions;  // stores all the transactions


        // initialize empty list when ledger is created
        public Ledger()
        {
            transactions = new List<T>();
        }

        // add new transaction to the list
        public void AddEntry(T entry)
        {
            transactions.Add(entry);
        }

        // get transactions for a specific date
        public List<T> GetTransactionsByDate(DateTime date)
        {
            List<T> result = new List<T>();
            foreach(T transaction in transactions)
            {
                // comparing only dates not time
                if(transaction.Date.Date == date.Date)
                {
                    result.Add(transaction);
                }
                
            }
            return result;
        }

        // adds up all the amounts
        public decimal CalculateTotal()
        {
            decimal total =0;

            // loop through and add each amount
            foreach(T transaction in transactions){
                total += transaction.Amount;
            }
            return total;
        }
        
        // returns the whole list of transactions
        public List<T> GetTransactions()
        {
            return transactions;
        }
    }
}
