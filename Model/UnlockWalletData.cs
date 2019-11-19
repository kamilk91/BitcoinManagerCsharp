using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinBasedNode.Model
{
    /// <summary>
    /// Unlock wallet model, passphrase + duration.
    /// </summary>
    public class UnlockWalletData
    {
        public string passphrase { get; set; }
        /// <summary>
        /// NOTE: 1 minute = 60 seconds, 1 hour = 60 minutes, 1 day = 24 hours, 1 week = 7 days, 1 month = 30 days, 1 year = 365 days
        /// </summary>
        public Time time { get; set; }
    }
}
