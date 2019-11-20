using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinBasedNode.Model
{
    public class ListTransactionsForAddressResult
    {
        public IList<Transaction> transactions { get; set; }
    }
}
