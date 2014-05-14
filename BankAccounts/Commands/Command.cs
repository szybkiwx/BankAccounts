using BankAccounts.Data;
using BankAccounts.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Commands
{
    public abstract class Command
    {
        protected BADataContext Context { get; set; }

        public Command(BADataContext context)
        {
            Context = context;
        }

        public abstract void Execute(BankAccount ba, string[] args);
    }
}
