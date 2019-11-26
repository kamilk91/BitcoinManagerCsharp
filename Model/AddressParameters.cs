using System;
using System.Collections.Generic;
using System.Text;
using static BitcoinBasedNode.Communication.Bitcoind;

namespace BitcoinBasedNode.Model
{
    public class AddressParameters
    {
        public string label { get; set; } = "";
        public AddressTypes addressType { get; set; } = AddressTypes.p2shsegwit;
        public string account { get; set; }
    }
}
