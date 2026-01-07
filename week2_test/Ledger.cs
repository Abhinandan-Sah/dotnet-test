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
    public class Ledger<T> where T : Transaction
    {
        private List<T> transactions;

        public Ledger()
        {
            transactions = new List<T>();
        }

        public void AddEntry(T entry)
        {
            transactions.Add(entry);
        }

        public List<T> GetTransactionsByDate(DateTime date)
        {
            List<T> result = new List<T>();
            foreach(T transaction in transactions)
            {
                if(transaction.Date.Date == date.Date)
                {
                    result.Add(transaction);
                }
                
            }
            return result;
        }

        public decimal CalculateTotal()
        {
            decimal total =0;

            foreach(T transaction in transactions){
                total += transaction.Amount;
            }
            return total;
        }
    }
}
