using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinBasedNode.Model
{
    public class GetTransactionByTxid
    {
        public string txid { get; set; }
        public bool include_watchonly { get; set; } = false;
    }


}
