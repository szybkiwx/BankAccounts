using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Data.Entities
{
    public class SavingsBankAccount : BankAccount
    {
        private const int FirstInterestTreshold = 100000;
        private const int SecondInterestTreshold = 200000;

        public int Withdrawals { get; set; }

        public override decimal GetInterestRate()
        {
            if (Amount < FirstInterestTreshold) 
            {
                return 0.03m;
            }

            if (Amount < SecondInterestTreshold)
            {
                return 0.04m;
            }

            return 0.06m;
        }

        public override void WithdrawMoney(decimal delta)
        {
            decimal withdrawalFeePercentage = Withdrawals >= 6 ? 0.02m : 0m;

            if (Amount - delta < 0)
            {
                throw new Exception("The withdraw amount is bigger than you account's balance");
            }

            Amount -= (1 + withdrawalFeePercentage) * delta;

            Withdrawals++;
        }
    }
}
