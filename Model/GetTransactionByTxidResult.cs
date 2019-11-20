using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinBasedNode.Model
{
    public class Detail
    {
        public string address { get; set; }
        public string category { get; set; }
        public decimal amount { get; set; }
        public int vout { get; set; }
    }

    public class GTBTRResult
    {
        public decimal amount { get; set; }
        public int confirmations { get; set; }
        public bool generated { get; set; }
        public string blockhash { get; set; }
        public int blockindex { get; set; }
        public int blocktime { get; set; }
        public string txid { get; set; }
        public IList<object> walletconflicts { get; set; }
        public int time { get; set; }
        public int timereceived { get; set; }
        public string bip125replaceable { get; set; }
        public IList<Detail> details { get; set; }
        public string hex { get; set; }
    }

    public class GetTransactionByTxidResult
    {
        public GTBTRResult result { get; set; }
        public object error { get; set; }
        public int id { get; set; }
    }
}
