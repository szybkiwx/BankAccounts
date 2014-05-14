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
        public DbSet<CheckedBankAccount> CheckedBankAccountSet { get; set; }
        public DbSet<SavingsBankAccount> SavingsBankAccountSet { get; set; }
        public DbSet<OperationHistory> OperationHistorySet { get; set; }
        
        public BADataContext()
            : base("BankAccounts")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            
        }
    }
}
