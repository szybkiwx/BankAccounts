using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Commands
{
    public class CommandAggregate
    {
        public AddCommand AddCommand { get; set; }
        public WithdrawCommand WithdrawCommand { get; set; }
        public BankStatement BankStatement { get; set; }
    }
}
