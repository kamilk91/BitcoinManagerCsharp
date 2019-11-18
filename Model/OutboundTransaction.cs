using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BitcoinBasedNode.Model
{
    public class OutboundTransaction
    {

        public string address { get; set; }

        public decimal amount { get; set; }
        public string comment { get; set; } = "";
        public string comment_to { get; set; } = "";
        public bool substractfeefromamount { get; set; } = false;
        public bool replacableviabip125 { get; set; } = false;
        public int conf_target { get; set; } = 3;
        public ESTIMATE_MODE estimate_mode { get; set; } = ESTIMATE_MODE.UNSET;

        public bool IWantToSendTransactionWithoutBitcoins { get; set; } = false;
     


    }

    public class OutboundTransactions
    {
        public IList<OutboundTransaction> transactions { get; set; }
        public JArray substractFeeFrom { get; set; } = new JArray();
        public int minconf { get; set; } = 3;
        public string comment { get; set; } = "";
        public bool replacableBip125 { get; set; } = false;
        public int confTarget { get; set; } = 3;
        public ESTIMATE_MODE estimateMode { get; set; }
    }
}
