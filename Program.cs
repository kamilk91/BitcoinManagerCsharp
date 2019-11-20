using System;
using BitcoinBasedNode.Model;
using BitcoinBasedNode.Communication;
using System.Collections.Generic;

namespace BitcoinBasedNode
{
    class Program
    {
        static void Main(string[] args)
        {

            Bitcoind cli = new Bitcoind("a", "b");
            ListTransactionsForAddressResult res =  cli.ListTransactionsForAddress(new ListTransactionsForAddress
            {
                address = "2MvdNsXraMT6F3VddveDeu2pFuVkuPibrXQ",
                direction = Bitcoind.TransactionDirection.SENT
            });

            

            foreach(Transaction x in res.transactions)
            {
                Console.WriteLine(x.txid);
            }


            ListTransactionsForAddressResult res1 = cli.ListTransactionsForAddress("2MvdNsXraMT6F3VddveDeu2pFuVkuPibrXQ", Bitcoind.TransactionDirection.RECEIVED);
            foreach (Transaction x in res1.transactions)
            {
                Console.WriteLine(x.txid);
            }


            GetReceivedByAddressResult received = cli.GetReceivedByAddress("2MvdNsXraMT6F3VddveDeu2pFuVkuPibrXQ", 1);
            GetReceivedByAddressResult received2 = cli.GetReceivedByAddress(new GetReceivedByAddress
            {
                address = "2MvdNsXraMT6F3VddveDeu2pFuVkuPibrXQ",
                minconf = 2
            });

            GetTransactionByTxidResult getTransactionByTxidResult = cli.GetTransactionByTxid(new GetTransactionByTxid
            {
                include_watchonly = false,
                txid = "ab62d74aa9fbf8d7f23b636fa1eb876695e70a610e68ae566df201f5691cbf7b"
            });

            GetTransactionByTxidResult c = cli.GetTransactionByTxid("ab62d74aa9fbf8d7f23b636fa1eb876695e70a610e68ae566df201f5691cbf7b", false);


        }
    }
}
