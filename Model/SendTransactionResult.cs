using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinBasedNode.Model
{
    public class SendTransactionResult
    {
        public string result { get; set; }
        public object error { get; set; }
        public int id { get; set; }
    }
}
