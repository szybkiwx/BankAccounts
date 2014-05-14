using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Commands
{
    public class BankStatement : Command
    {
        public BankStatement(DbContext ctx)
            : base(ctx)
        {
        }

        public override void Execute(IEnumerable<string> args)
        {
            throw new NotImplementedException();
        }
    }
}
