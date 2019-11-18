using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinBasedNode.Model
{
    public class GetBalanceResult
    {
        public decimal result { get; set; }
        public string error { get; set; }
        public int id { get; set; }
    }


}
