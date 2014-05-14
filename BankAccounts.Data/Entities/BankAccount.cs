using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Data.Entities
{
    public abstract class BankAccount
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public abstract decimal GetInterestRate();

        public void DepositMoney(decimal delta)
        {
            Amount += delta;
        }
    }
}
