using BankAccounts.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Data
{
    public class BADataContext : DbContext
    {
        public virtual IDbSet<CheckedBankAccount> CheckedBankAccountSet { get; set; }
        public virtual IDbSet<SavingsBankAccount> SavingsBankAccountSet { get; set; }
        public virtual IDbSet<OperationHistory> OperationHistorySet { get; set; }
        
        public BADataContext()
            : base("BankAccounts")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SavingsBankAccount>().Property(q => q.Amount).HasPrecision(18, 10);
            modelBuilder.Entity<CheckedBankAccount>().Property(q => q.Amount).HasPrecision(18, 10);
            modelBuilder.Entity<OperationHistory>().Property(q => q.Amount).HasPrecision(18, 10);
        }
    }
}
