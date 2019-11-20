using System;
using System.Collections.Generic;
using System.Text;
using BitcoinBasedNode.Communication;
using static BitcoinBasedNode.Communication.Bitcoind;

namespace BitcoinBasedNode.Model
{
    public class ListTransactionsForAddress
    {
        public string address { get; set; }
        public TransactionDirection direction { get; set; } = TransactionDirection.BOTH;
    }
}
