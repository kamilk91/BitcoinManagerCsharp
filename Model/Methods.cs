using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using static BitcoinBasedNode.Communication.Bitcoind;

namespace BitcoinBasedNode.Model
{
    public interface ICryptoBase
    {
        SendTransactionResult SendTransaction(string address, decimal amount);
        GetBalanceResult GetBalance();
        ListTransactionsResult ListTransactions();
        Result GetNewAddress();
        Result UnlockWallet(string passphrase, int seconds);
        GetTransactionByTxidResult GetTransactionByTxid(string txid, bool watchonly = false);
        ListTransactionsForAddressResult ListTransactionsForAddress(string address, TransactionDirection direction = TransactionDirection.BOTH);
        GetReceivedByAddressResult GetReceivedByAddress(string address, int minconf = 1);

    };

    public interface ICryptoExtended
    {
        GetBalanceResult GetBalance(int minimumConfirmations, string dummy = "*", bool includeWatchonlyAddresses = false);
        ListTransactionsResult ListTransactions(int count = 10, int skip = 0, bool include_watchonly = false, string dummy = "*");
        SendTransactionResult SendTransaction(string address, decimal amount, string comment = "", string comment_to = "", bool substractFeeFromAmount = false, bool replacableBIP125 = false, int conf_target = 3, ESTIMATE_MODE estimateMode = ESTIMATE_MODE.UNSET);
        SendTransactionResult SendTransaction(OutboundTransaction transaction);
        SendTransactionResult SendTransactions(OutboundTransactions transactions);
        Result GetNewAddress(AddressParameters addressParameters);
        Result UnlockWallet(UnlockWalletData data);
        GetTransactionByTxidResult GetTransactionByTxid(GetTransactionByTxid transaction);
        ListTransactionsForAddressResult ListTransactionsForAddress(ListTransactionsForAddress forAddress);
        GetReceivedByAddressResult GetReceivedByAddress(GetReceivedByAddress address);

    }



}
