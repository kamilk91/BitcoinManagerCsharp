## Introduction

Just build, and configure node. 

# What's contained in this project

source code without builds. To build use ```dotnet build --debug ```

# Samples and potentialy important info

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
