using BankAccounts.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Data
{
    public class BADataContextInitializer : DropCreateDatabaseIfModelChanges<BADataContext>
    {
        protected override void Seed(BADataContext context)
        {
            context.CheckedBankAccountSet.Add(new CheckedBankAccount());
            context.SavingsBankAccountSet.Add(new SavingsBankAccount());
        }
    }
}
