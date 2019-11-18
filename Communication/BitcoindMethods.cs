using BitcoinBasedNode.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
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
            IRestRequest req = new RestRequest(Method.POST);
            req = addMandatoryHeader(req);
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

    }
}
