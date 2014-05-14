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
        protected DbContext Context { get; set; }

        public Command(DbContext context)
        {
            Context = context;
        }

        public abstract void Execute(IEnumerable<string> args);
    }
}
