## Introduction

Just build, and configure node. 

## What's contained in this project

Basic methods like:
- GetBalance
- ListTransactions
- SendTransaction
- SendTransactions 

## Samples and potentialy important info

### Sendmany sample

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
				amount = (decimal)0.13
			},
		new OutboundTransaction
			{
				address = "2N8xWJSBrHNJSXqxidQe7GnW78NnuWJJVu9",
				amount = (decimal)0.15
			}
		}
	};

SendTransactionResult result = instance.SendTransactions(transactions);
Console.WriteLine(result.result);

```

### Note
Some methods are different names than official. It is just for easy use. More info in summaries for each method. Have fun.