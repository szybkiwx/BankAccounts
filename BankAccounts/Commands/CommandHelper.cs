using BankAccounts.Data;
using BankAccounts.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Commands
{
    public interface ICommandHelper
    {
        BankAccount GetAccountByType(string type);
    }

    public class CommandHelper : ICommandHelper
    {
        private BADataContext _context;

        public CommandHelper(BADataContext context)
        {
            _context = context;
        }

        public BankAccount GetAccountByType(string type)
        {
            switch (type)
            {
                case "checked":
                    return _context.CheckedBankAccountSet.First();
                case "savings":
                    return _context.SavingsBankAccountSet.First();
                default:
                    throw new CommandException(string.Format("Invalid account type: {0}. Please provide one of the following: checked, savings", type));
            }
        }
    }
}
