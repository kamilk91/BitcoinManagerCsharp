using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinBasedNode.Model
{
    public class GetReceivedByAddress
    {
        public string address { get; set; }
        public int minconf { get; set; } = 1;
    }

    public class GetReceivedByAddressResult
    {
        public decimal result { get; set; }
    }
}
