# Use in your project

### Install with PackageManager
```Install-Package BitcoinManagerCsharp ``` 
### Install with .NET CLI
```dotnet add package BitcoinManagerCsharp```
### Add reference
```<PackageReference Include="BitcoinManagerCsharp"  />```
### Add with Paket CLI
```paket add BitcoinManagerCsharp ```

# What's contained in this project

source code without builds. To build use ```dotnet build --debug ```

# Samples and potentialy important info

## Methods implemented
- **GetBalance**
- **ListTransactions**
- **SendTransaction** *(sendtoaddress in Bitcoin Core)*
- **SendTransactions** *(sendmany in Bitcoin Core)*
- **GetNewAddress**
- **UnlockWallet** *(walletpassphrase in Bitcoin Core)*
- **GetTransactionByTxid** *(gettransaction in Bitcoin Core)*
- **GetReceivedByAddress**

Methods does not exist in Bitcoin Core, and are created by myself:

- **ListTransactionsForAddress** *It uses listtransactions, and then it sorting transactions via LINQ with specify conditions*


## Pink Parter
- stream or socket with new transaction notification
- stream or socket with sent transaction is confirmed (depends on choosen confirmation number)
- implement more methods
- tests
- tests with altcoins built on Bitcoin skeleton

## Subscribe to new transactions notifier

1. Create new instance of Bitcoind 
```
Bitcoind inst = new Bitcoind("a", "b");
```
2. Create instance of Notifier, using your Bitcoind Instance

```
Notifier n = new Notifier(inst,1000, DataType.LOCAL);
```
note that you can use DataType LOCAL or DATABASE. Local is faster, database will create SQLite file on your disk, and will save transactions. As you think, LOCAL data disapear when program with program ends. 
default interval is 1000, and it works good. Its enough for Bitcoin.

3. Subscribe to event
```
n.onTransaction += N_onTransaction;
```
4. and tell program what to do, if new transaction is detected:
```
   private static void N_onTransaction(Transaction transaction)
        {
            Console.WriteLine($"new transaction on {transaction.amount} for {transaction.address} with # {transaction.txid}");
        }
```

Full example

```
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
```


## ListTransactionsForAddress method info

I made special method, which is very usefull, and it does not exist in Bitcoin Core API. It allows you to see details about transactions for specify address. It is sorted by block descending, so you should see from newer to oldest. 

It works with 2 cases, first built with object:
```
var x = cli.ListTransactionsForAddress(new ListTransactionsForAddress { 
            address = "2MvdNsXraMT6F3VddveDeu2pFuVkuPibrXQ",
            direction = TransactionDirection.BOTH
            });

```

And second with just simply info about address, and interested transactions. 

```
var x = cli.ListTransactionsForAddress("2MvdNsXraMT6F3VddveDeu2pFuVkuPibrXQ", /*its optional -> */ TransactionDirection.BOTH);
```

If you have any suggestions to this method, please make new Issue, and if your tips will interest me, ill update this method. 


## Sendmany sample

```

Bitcoind instance = new Bitcoind("a", "b");

            OutboundTransactions transactions = new OutboundTransactions
            {
                
                comment = "noComment",
                confTarget = 1,
                minconf = 3,
                
                transactions = new List<OutboundTransaction>
                {
                    new OutboundTransaction
                    {
                        address = "2MyS4wzevEx7TdEZhTPnYinMgzPm5Q6xAKo",
                        amount = (decimal)0.12
                    },
                     new OutboundTransaction
                    {
                        address = "2ND5tZyKruM7qGy5GdLcqNrjZ7QxUR3rr4t",
                        amount = (decimal)0.12
                    },
                      new OutboundTransaction
                    {
                        address = "2MuqNqKmBdjbzHWoxfN9QxkuAJrLkNokVRZ",
                        amount = (decimal)0.12
                    },
                       new OutboundTransaction
                    {
                        address = "2N8xWJSBrHNJSXqxidQe7GnW78NnuWJJVu9",
                        amount = (decimal)0.12
                    }
                }


            };

            SendTransactionResult result = instance.SendTransactions(transactions);
            Console.WriteLine(result.result);

```

## Unlock wallet for human-readable time!

```
 instance.UnlockWallet(new UnlockWalletData {
  passphrase = "admin1",
   time = new Time {
    seconds = 10,
     minutes = 1,
     hours = 1,
     days = 22,
     weeks = 3,
     months = 1,
     years = 2,
   }
 });
```


 Here are values which are summarable, so you can do with your wallet unlocking whatever you want. 
 ### Note
 
 Actually, about constans period of times this calculator is 1:1, but remember that time is counted by easy model:
 ```
second = 1;
minute = 60;
hour = 60 * 60;
day = 60 * 60 * 24;
//week, month, and year are counted in +- day scale
week = 60 * 60 * 24 * 7;
month = 60 * 60 * 24 * 30;
year = 60 * 60 * 24 * 365;
 ```
 So "year" here is ```31,536,000 seconds```  which gives exactly 365 days. 
 
 # Credits
 - [Github](https://github.com/kamilk91/BitcoinManagerCsharp)
 - [Nuget.org](https://www.nuget.org/packages/BitcoinManagerCsharp/)
 
