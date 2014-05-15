using BankAccounts.Data;
using BankAccounts.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace BankAccounts
{
    public interface IInterestUpdateService
    {
        void Run();
        void UpdateInterests(TimeSpan span);
        void UpdateAccountInterest(BankAccount ba, TimeSpan span);
        void Stop();
    }

    public class InterestUpdateService : IInterestUpdateService
    {
        private BADataContext _context;
        private readonly object syncLock = new object();
        private bool _runningFlag = true;
        private const int _interestCalculateInterval = 5;

        public InterestUpdateService(BADataContext ctx)
        {
            _context = ctx;
        }

        public void Run()
        {
            DateTime lastTime = DateTime.Now;
            while (_runningFlag)
            {
                if ((DateTime.Now - lastTime).Seconds > _interestCalculateInterval)
                {
                    var now = DateTime.Now;
                    var span = now - lastTime;
                    lastTime = now;
                    lock (syncLock)
                    {
                        UpdateInterests(span);
                    }
                }
            }
        }

        public void UpdateInterests(TimeSpan span)
        {
            using (var transaction = new TransactionScope())
            {
                var savingsAccount = _context.SavingsBankAccountSet.First();
                var checkedAccount = _context.CheckedBankAccountSet.First();

                UpdateAccountInterest(savingsAccount, span);
                UpdateAccountInterest(checkedAccount, span);

                _context.SaveChanges();
            }
        }

        public void UpdateAccountInterest(BankAccount ba, TimeSpan span)
        {
            DateTime now = DateTime.Now;
            TimeSpan oneYear = now.AddYears(1) - now;
            decimal percentage = (ba.GetInterestRate() * (decimal)(span.TotalMilliseconds / oneYear.TotalMilliseconds)) / 100m;
            decimal interest = ba.Amount * percentage;
            ba.Amount += interest;
            _context.OperationHistorySet.Add(new OperationHistory()
            {
                Amount = interest,
                BankAccountId = ba.Id,
                OperationDate = DateTime.Now,
                OperationType = OperationType.Interests
            });
        }



        public void Stop()
        {
            _runningFlag = false;
        }
    }
}
