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
    }
}
