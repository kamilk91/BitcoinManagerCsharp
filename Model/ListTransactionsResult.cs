using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BitcoinBasedNode.Model
{
    public class Transaction
    {
        [Key]
        public int id { get; set; }
        public string address { get; set; }
        public string category { get; set; }
        public decimal amount { get; set; }
        public int vout { get; set; }
        public int confirmations { get; set; }
        public bool generated { get; set; }
        public string blockhash { get; set; }
        public int blockindex { get; set; }
        public int blocktime { get; set; }
        public string txid { get; set; }
        //public IList<object> walletconflicts { get; set; }
        public int time { get; set; }
        public int timereceived { get; set; }

    }
    public class ListTransactionsResult
    {
        public IList<Transaction> result { get; set; }
    }

}
