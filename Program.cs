using System;
using BitcoinBasedNode.Model;
using BitcoinBasedNode.Communication;
using System.Collections.Generic;
using BitcoinBasedNode.Notifications;

namespace BitcoinBasedNode
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitcoind inst = new Bitcoind("a", "b");
            Notifier n = new Notifier(inst,1000, DataType.LOCAL);
            n.onTransaction += N_onTransaction;









            Console.ReadLine();
           

        }

        private static void N_onTransaction(Transaction transaction)
        {
            Console.WriteLine($"new transaction on {transaction.amount} for {transaction.address} with # {transaction.txid}");
        }
    }
}
