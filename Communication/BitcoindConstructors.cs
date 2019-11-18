using BitcoinBasedNode.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinBasedNode.Communication
{
    partial class Bitcoind
    {
        /// <summary>
        /// Create instance of Bitcoind with rpc username and password.
        /// </summary>
        /// <param name="rpcUserName">
        /// Set it in bitcoin.conf, or start with flag --rpcuser
        /// </param>
        /// <param name="rpcPassword">
        /// /// Set it in bitcoin.conf, or start with flag --rpcpassword
        /// </param>
        /// <param name="host">
        /// If you started locally it should be 127.0.0.1
        /// </param>
        /// <param name="port">
        /// Default regtest port is 18443, default mainnet port is 8332. For altcoins you should check for each one.
        /// </param>
        public Bitcoind(string rpcUserName, string rpcPassword, string host = "127.0.0.1", string port = "18443")
        {
            this.rpcUserName = rpcUserName;
            this.rpcPassword = rpcPassword;
            this.host = host;
            this.port = port;
            this.client = CreateClient();
            NodeAlive();
        }

        private readonly string rpcUserName;
        private readonly string rpcPassword;
        private readonly string host;
        private readonly string port;
        RestClient client;

        private void NodeAlive()
        {
            IRestRequest req = new RestRequest(method: Method.POST);
            req = addMandatoryHeader(req);
            JObject obj = BodySkeleton("getwalletinfo");
            req.AddJsonBody(obj.ToString());
            var response = client.Execute(req);
            ValidateResponse(response);

        } 



        







        /*
         * 
         * 
         * 
         * 
         *  Internal methods.
         * 
         * 
         * 
         */



        private void ValidateResponse(IRestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException("Can't connect, check passphrase and rpc login. ");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                JObject err = GetError(response);
                throw new Exception($"{err.ToString()}");
            }
            // Check for errors inside bitcoin node. 
            
            StandardError errorNodeLevel = JsonConvert.DeserializeObject<StandardError>(response.Content);
            if(errorNodeLevel.error != null)
            {
                throw new Exception("Error on Crypto node level\n" + response.Content);
            }


        }

        /// <summary>
        /// Get error from http rest response.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private JObject GetError(IRestResponse response)
        {
            JObject err = new JObject();
            err.Add(new JProperty("StatusCode", $"{response.StatusCode}"));
            err.Add(new JProperty("Message", response.Content));
            return err;
        }

        /// <summary>
        /// Build body for RPC Bitcoin/Altcoins
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private JObject BodySkeleton(string method, JArray parameters = null)
        {
            JObject obj = new JObject();
            obj.Add(new JProperty("jsonrpc", "1.0"));
            obj.Add(new JProperty("id", 1));
            obj.Add(new JProperty("method", $"{method}"));
            if (parameters == null)
            {
                obj.Add(new JProperty("params", new JArray()));
            }
            else
            {
                obj.Add("params", parameters);
            }

            return obj;

        }
        /// <summary>
        /// Create client.
        /// </summary>
        /// <returns></returns>
        private RestClient CreateClient()
        {
            RestClient cli = new RestClient($"http://{host}:{port}");
            cli.Authenticator = new HttpBasicAuthenticator(rpcUserName, rpcPassword);
            return cli;
        }
        /// <summary>
        /// Add required header
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        private IRestRequest addMandatoryHeader(IRestRequest req)
        {
            req.AddHeader("Content-type", "application/json");

            return req;
        }

        /*
         * 
         * 
         * 
         *  Types
         * 
         * 
         */
       

    }


}

