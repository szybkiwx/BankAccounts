using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Commands
{
    public class CommandAggregate
    {
        [Dependency]
        public DepositCommand DepositCommand { get; set; }

        [Dependency]
        public WithdrawCommand WithdrawCommand { get; set; }

        [Dependency]
        public BankStatementCommand BankStatementCommand { get; set; }

    }
}
