using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Data.Entities
{
    public class CheckedBankAccount : BankAccount
    {
        public override decimal GetInterestRate()
        {
            return 0.03m;
        }

        public override void WithdrawMoney(decimal delta)
        {
            if (Amount - delta < 0)
            {
                throw new Exception("The withdraw amount is bigger than you accountt's balance");
            }

            Amount -= delta;
        }
    }
}
