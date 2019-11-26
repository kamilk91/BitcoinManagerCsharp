using BitcoinBasedNode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using BitcoinBasedNode.Communication;
namespace BitcoinBasedNode.Notifications
{
    /// <summary>
    /// BETA, you can test it, transactions should not be doubled, but it will work faster. 
    /// </summary>
    /// 

    public enum DataType { LOCAL, DATABASE }
    public class Notifier
    {
        private Timer IntervalTimer;
        private int Interval;
        public delegate void NewTransaction(Transaction transaction);
        public event NewTransaction onTransaction;
        private SaveDB _transactions = new SaveDB();
        private List<Transaction> _listTransactions = new List<Transaction>();
        private Bitcoind _instance;
        private static bool duringAction = false;
        private DataType dataType;




        public Notifier(Bitcoind instance, int interval = 5000, DataType dataType = DataType.DATABASE)
        {
            this.Interval = interval;
            this._instance = instance;
            this.dataType = dataType;
            InitIntervalTimer();

        }

        private void InitIntervalTimer()
        {
            IntervalTimer = new Timer();
            IntervalTimer.Elapsed += new ElapsedEventHandler(CheckNewTransaction);
            IntervalTimer.Interval = this.Interval;
            IntervalTimer.Start();
        }


        private void CheckNewTransaction(object sender, EventArgs e)
        {



            if (!duringAction)
            {

                duringAction = true;
                ListTransactionsResult result = _instance.ListTransactions(100000);
                if (dataType == DataType.DATABASE)
                {
                    lock (_transactions)
                    {
                        foreach (Transaction transaction in result.result)
                        {
                            var exist = _transactions._transaction.Where(x => x.txid == transaction.txid && x.category == transaction.category).ToList();
                            if (exist.Count == 0)
                            {
                                _transactions.Add(transaction);
                                onTransaction.Invoke(transaction);
                            }

                        }
                        _transactions.SaveChanges();
                    }
                }
                if (dataType == DataType.LOCAL)
                {
                    lock (_listTransactions)
                    {

                        foreach (Transaction transaction in result.result)
                        {
                            var exist = _listTransactions.Where(x => x.txid == transaction.txid && x.category == transaction.category).Select(x => x).ToList();
                            if (exist.Count == 0)
                            {
                                _listTransactions.Add(transaction);
                                onTransaction.Invoke(transaction);
                            }

                        }
                    }
                }
            }



            duringAction = false;


        }




    }
}
