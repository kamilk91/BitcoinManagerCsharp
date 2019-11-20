using BitcoinBasedNode.Helpers;
using BitcoinBasedNode.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitcoinBasedNode.Communication
{
    partial class Bitcoind
    {
        /// <summary>
        /// Get total active balance on your wallet. (Default main wallet, and minimum 0 confirmations. Totally basic usage of getbalance method)
        /// </summary>
        /// <returns></returns>
        public GetBalanceResult GetBalance()
        {
            IRestRequest req = new RestRequest(Method.POST);
            req = addMandatoryHeader(req);
            JObject obj = BodySkeleton("getbalance");
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);
            GetBalanceResult result = JsonConvert.DeserializeObject<GetBalanceResult>(response.Content);
            return result;
        }

        /// <summary>
        /// Get total active balance for specified minimum confirmations.
        /// </summary>
        /// <param name="minimumConfirmations">
        /// Integer number of minimum confirmations of transactions you want to see.
        /// </param>
        /// <param name="dummy">
        /// vermiform appendix. Must be *, because? ( ͡° ͜ʖ ͡°)
        /// </param>
        /// <param name="includeWatchonlyAddresses">
        /// Set true if you are stalker! Kidding, specify if balance should include watchonly addresses. 
        /// </param>
        /// <returns></returns>
        public GetBalanceResult GetBalance(int minimumConfirmations, string dummy = "*", bool includeWatchonlyAddresses = false)
        {
            IRestRequest req = new RestRequest(Method.POST);
            req = addMandatoryHeader(req);
            JArray parameter = new JArray();

            parameter.Add(dummy);
            parameter.Add(minimumConfirmations);
            parameter.Add(includeWatchonlyAddresses);
            JObject obj = BodySkeleton("getbalance", parameter);



            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);
            GetBalanceResult result = JsonConvert.DeserializeObject<GetBalanceResult>(response.Content);
            return result;
        }

        /// <summary>
        /// List transactions default.
        /// </summary>
        /// <returns></returns>
        public ListTransactionsResult ListTransactions()
        {
            IRestRequest req = new RestRequest(Method.POST);
            req = addMandatoryHeader(req);
            JObject obj = BodySkeleton("listtransactions");
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);

            ListTransactionsResult res = JsonConvert.DeserializeObject<ListTransactionsResult>(response.Content);
            return res;

        }

        /// <summary>
        /// List transactions with specify parameters.
        /// </summary>
        /// <param name="count">
        /// Default 10, count of transactions returned.
        /// </param>
        /// <param name="skip">
        /// how much transactions should be skipped?
        /// </param>
        /// <param name="include_watchonly">
        /// include watchonly addresses?
        /// </param>
        /// <param name="dummy">
        /// leave it alone. 
        /// </param>
        /// <returns></returns>

        public ListTransactionsResult ListTransactions(int count = 10, int skip = 0, bool include_watchonly = false, string dummy = "*")
        {
            IRestRequest req = new RestRequest(Method.POST);
            req = addMandatoryHeader(req);
            JArray parameter = new JArray();
            parameter.Add(dummy);
            parameter.Add(count);
            parameter.Add(skip);
            parameter.Add(include_watchonly);
            JObject obj = BodySkeleton("listtransactions", parameter);
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);
            ListTransactionsResult res = JsonConvert.DeserializeObject<ListTransactionsResult>(response.Content);
            return res;


        }

        /// <summary>
        /// Simply send transaction to address with amount.
        /// </summary>
        /// <param name="address">
        /// Bitcoin address, it is not validating for it here.
        /// </param>
        /// <param name="amount">
        /// amount of bitcoins you want to send in 0.00000000 format 
        /// </param>
        /// <returns></returns>
        public SendTransactionResult SendTransaction(string address, decimal amount)
        {
            IRestRequest req = new RestRequest(Method.POST);
            req = addMandatoryHeader(req);
            JArray parameter = new JArray();
            parameter.Add(address);
            parameter.Add(amount);

            JObject obj = BodySkeleton("sendtoaddress", parameter);
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);
            SendTransactionResult res = JsonConvert.DeserializeObject<SendTransactionResult>(response.Content);
            return res;
        }


        /// <summary>
        /// Advanced transaction sending. If you want to stay some parameters as default, just leave them, and specify values of interested parameters. 
        /// </summary>
        /// <param name="address">
        /// Bitcoin address
        /// </param>
        /// <param name="amount">
        /// Amount to send
        /// </param>
        /// <param name="comment">
        /// Transaction comment in string.
        /// </param>
        /// <param name="comment_to">
        /// Comment persona.
        /// </param>
        /// <param name="substractFeeFromAmount">
        /// Do you want to send exactly number of bitcoins? If you want to send for example whole wallet balance just use this option.
        /// </param>
        /// <param name="replacableBIP125">
        /// Don't touch if you dont understand. 
        /// </param>
        /// <param name="conf_target">
        /// Target of confirmations in Blockchain.
        /// </param>
        /// <param name="estimateMode">
        /// Enum value to decide how to choose fee for your transaction
        /// </param>
        /// <returns></returns>
        public SendTransactionResult SendTransaction(string address, decimal amount, string comment = "", string comment_to = "", bool substractFeeFromAmount = false, bool replacableBIP125 = false, int conf_target = 3, ESTIMATE_MODE estimateMode = ESTIMATE_MODE.UNSET)
        {
            IRestRequest req = new RestRequest(Method.POST);
            req = addMandatoryHeader(req);
            JArray parameter = new JArray();
            parameter.Add(address);
            parameter.Add(amount);
            parameter.Add(comment);
            parameter.Add(comment_to);
            parameter.Add(substractFeeFromAmount);
            parameter.Add(replacableBIP125);
            parameter.Add(conf_target);
            parameter.Add(estimateMode.ToString());
            JObject obj = BodySkeleton("sendtoaddress", parameter);
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);
            SendTransactionResult res = JsonConvert.DeserializeObject<SendTransactionResult>(response.Content);
            return res;
        }


        public SendTransactionResult SendTransaction(OutboundTransaction transaction)
        {
            IRestRequest req = new RestRequest(Method.POST);
            req = addMandatoryHeader(req);
            JArray parameter = new JArray();
            parameter.Add(transaction.address);
            parameter.Add(transaction.amount);
            parameter.Add(transaction.comment);
            parameter.Add(transaction.comment_to);
            parameter.Add(transaction.substractfeefromamount);
            parameter.Add(transaction.replacableviabip125);
            parameter.Add(transaction.conf_target);
            parameter.Add(transaction.estimate_mode.ToString());
            JObject obj = BodySkeleton("sendtoaddress", parameter);
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);
            SendTransactionResult res = JsonConvert.DeserializeObject<SendTransactionResult>(response.Content);
            return res;
        }
        /// <summary>
        /// Send more than one transaction. You have to add BitcoinBasedNode.Models, and create object. When adding single transaction, you can skip everything except address, and amount, 
        /// other parameters will be ignored.
        /// </summary>
        /// <param name="transactions">
        /// Create new object OutbountTransactions. 
        /// </param>
        /// <returns></returns>
        public SendTransactionResult SendTransactions(OutboundTransactions transactions)
        {
            IRestRequest req = GenerateStandardRequest();
            
            JArray parameter = new JArray();
            parameter.Add("");
            JObject transactionsArray = new JObject();
            foreach (OutboundTransaction transaction in transactions.transactions)
            {
                transactionsArray.Add
                    (
                        new JProperty(transaction.address, transaction.amount)
                    );
            }
            parameter.Add(transactionsArray);
            parameter.Add(transactions.minconf);
            parameter.Add(transactions.comment);
            parameter.Add(transactions.substractFeeFrom);
            parameter.Add(transactions.replacableBip125);
            parameter.Add(transactions.confTarget);
            parameter.Add(transactions.estimateMode.ToString());
            JObject obj = BodySkeleton("sendmany", parameter);
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);
            SendTransactionResult result = JsonConvert.DeserializeObject<SendTransactionResult>(response.Content); ;
            return result;


        }

        /// <summary>
        /// Generate new Bitcoin address 
        /// </summary>
        /// <param name="label">
        /// Friendly name of your address.
        /// </param>
        /// 
        /// <param name="addressType">
        /// Please, do not use if you don't know what are you doing. Read bitcoin.org.
        /// </param>
        /// <returns></returns>
        public Result GetNewAddress(string label = "", AddressTypes addressType = AddressTypes.legacy)
        {
            string addressTypeString = "";
            if (addressType == AddressTypes.p2shsegwit)
                addressTypeString = "p2sh-segwit";
            else
            {
                addressTypeString = addressType.ToString();
            }
            IRestRequest req = GenerateStandardRequest();
            JArray parameter = new JArray();
            parameter.Add(label);
            parameter.Add(addressTypeString);
            JObject obj = BodySkeleton("getnewaddress", parameter);
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);
            Result result = JsonConvert.DeserializeObject<Result>(response.Content);
            return result;
        }

        /// <summary>
        /// Generate new Bitcoin address 
        /// </summary>
        /// <param name="addressParameters">
        /// Same method, but with friendly model. 
        /// </param>
        /// <returns></returns>
        public Result GetNewAddress(AddressParameters addressParameters)
        {
            string addressTypeString = "";
            if (addressParameters.addressType == AddressTypes.p2shsegwit)
                addressTypeString = "p2sh-segwit";
            else
            {
                addressTypeString = addressParameters.addressType.ToString();
            }
            IRestRequest req = GenerateStandardRequest();
            JArray parameter = new JArray();
            parameter.Add(addressParameters.label);
            parameter.Add(addressTypeString);
            JObject obj = BodySkeleton("getnewaddress", parameter);
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);
            Result result = JsonConvert.DeserializeObject<Result>(response.Content);
            return result;
        }

        /// <summary>
        /// Just take a new address, without label and default address format. 
        /// </summary>
        /// <returns></returns>
        public Result GetNewAddress()
        {
           
            IRestRequest req = GenerateStandardRequest();
            JArray parameter = new JArray();
            JObject obj = BodySkeleton("getnewaddress", parameter);
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);
            Result result = JsonConvert.DeserializeObject<Result>(response.Content);
            return result;
        }

        /// <summary>
        /// Simply unlock your encrypted wallet.
        /// </summary>
        /// <param name="passphrase">
        /// Passphrase you specifed when you encrypted wallet. 
        /// </param>
        /// <param name="seconds">
        /// How much time a wallet should be unlocked? 
        /// </param>
        /// <returns></returns>
        public Result UnlockWallet(string passphrase, int seconds)
        {
            IRestRequest req = GenerateStandardRequest();
            JArray parameter = new JArray();
            parameter.Add(passphrase);
            parameter.Add(seconds);
            JObject obj = BodySkeleton("walletpassphrase", parameter);
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);
            Result result = JsonConvert.DeserializeObject<Result>(response.Content);
            if(result.result == null)
            {
                result.result = "unlocked";
            }
            return result;
        }

        /// <summary>
        ///  Simply unlock your encrypted wallet.
        /// </summary>
        /// <param name="data">
        /// Use model to unlock wallet, and specify human readable time period params. 
        /// </param>
        /// <returns></returns>
        public Result UnlockWallet(UnlockWalletData data)
        {
            IRestRequest req = GenerateStandardRequest();
            JArray parameter = new JArray();
            parameter.Add(data.passphrase);
            int timeForUnlock = TimeCalculator.CalculateTime(data.time);
            parameter.Add(timeForUnlock);
            JObject obj = BodySkeleton("walletpassphrase", parameter);
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);
            Result result = JsonConvert.DeserializeObject<Result>(response.Content);
            if (result.result == null)
            {
                result.result = "unlocked";
            }
            return result;
        }

        /// <summary>
        /// Get transaction by Hash / txid
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public GetTransactionByTxidResult GetTransactionByTxid(GetTransactionByTxid transaction)
        {
            IRestRequest req = GenerateStandardRequest();
            JArray parameter = new JArray();
            parameter.Add(transaction.txid);
            parameter.Add(transaction.include_watchonly);
            JObject obj = BodySkeleton("gettransaction", parameter);
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);
            GetTransactionByTxidResult res = JsonConvert.DeserializeObject<GetTransactionByTxidResult>(response.Content);
            return res;
        }

        /// <summary>
        /// Get transaction by Hash / txid
        /// </summary>
        /// <param name="txid"></param>
        /// <param name="watchonly"></param>
        /// <returns></returns>
        public GetTransactionByTxidResult GetTransactionByTxid(string txid, bool watchonly = false)
        {
            IRestRequest req = GenerateStandardRequest();
            JArray parameter = new JArray();
            parameter.Add(txid);
            parameter.Add(watchonly);
            JObject obj = BodySkeleton("gettransaction", parameter);
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);
            GetTransactionByTxidResult res = JsonConvert.DeserializeObject<GetTransactionByTxidResult>(response.Content);
            return res;
        }

        /// <summary>
        /// Method to list transactions for specify address.
        /// </summary>
        /// <param name="forAddress"></param>
        /// <returns></returns>
        public ListTransactionsForAddressResult ListTransactionsForAddress(ListTransactionsForAddress forAddress)
        {
            ListTransactionsForAddressResult final = new ListTransactionsForAddressResult();
            final.transactions = new List<Transaction>();
            ListTransactionsResult list = ListTransactions(1000000);
            var transactions = list.result.Where(x => x.address == forAddress.address);
            if(forAddress.direction == TransactionDirection.SENT)
            {
                transactions = transactions.Where(x => x.category == "send");
            }
            else if(forAddress.direction == TransactionDirection.RECEIVED)
            {
                transactions = transactions.Where(x => x.category == "receive");
            }
            transactions = transactions.Select(x => x).OrderByDescending(x => x.blockindex);

            foreach(Transaction trans in transactions)
            {
                final.transactions.Add(trans);
            }

            return final;

        }

        /// <summary>
        /// Method to list transactions for specify address.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public ListTransactionsForAddressResult ListTransactionsForAddress(string address, TransactionDirection direction = TransactionDirection.BOTH)
        {
            ListTransactionsForAddressResult final = new ListTransactionsForAddressResult();
            final.transactions = new List<Transaction>();
            ListTransactionsResult list = ListTransactions(1000000);
            var transactions = list.result.Where(x => x.address == address);
            if (direction == TransactionDirection.SENT)
            {
                transactions = transactions.Where(x => x.category == "send");
            }
            else if (direction == TransactionDirection.RECEIVED)
            {
                transactions = transactions.Where(x => x.category == "receive");
            }
            transactions = transactions.Select(x => x).OrderByDescending(x => x.blockindex);

            foreach (Transaction trans in transactions)
            {
                final.transactions.Add(trans);
            }

            return final;

        }
        /// <summary>
        /// Classic Bitcoin Core method to see how much crypto address received.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public GetReceivedByAddressResult GetReceivedByAddress(GetReceivedByAddress address)
        {
            IRestRequest req = GenerateStandardRequest();
            JArray parameter = new JArray();
            parameter.Add(address.address);
            parameter.Add(address.minconf);
            JObject obj = BodySkeleton("getreceivedbyaddress", parameter);
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);

            GetReceivedByAddressResult result = JsonConvert.DeserializeObject<GetReceivedByAddressResult>(response.Content);
            return result;
        }

        /// <summary>
        /// Classic Bitcoin Core method to see how much crypto address received.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="minconf"></param>
        /// <returns></returns>
        public GetReceivedByAddressResult GetReceivedByAddress(string address, int minconf = 1)
        {
            IRestRequest req = GenerateStandardRequest();
            JArray parameter = new JArray();
            parameter.Add(address);
            parameter.Add(minconf);
            JObject obj = BodySkeleton("getreceivedbyaddress", parameter);
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);

            GetReceivedByAddressResult result = JsonConvert.DeserializeObject<GetReceivedByAddressResult>(response.Content);
            return result;
        }
    }
}
