using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinBasedNode.Model
{
    public class TransactionIntern
    {
        public decimal fee { get; set; }
        public int amount { get; set; }
        public int blockindex { get; set; }
        public string category { get; set; }
        public int confirmations { get; set; }
        public string address { get; set; }
        public string txid { get; set; }
        public int block { get; set; }
        public string blockhash { get; set; }
        public string account { get; set; }
    }

    public class TransactionsIntern
    {
        public string lastblock { get; set; }
        public IList<TransactionIntern> transactions { get; set; }
    }
}
