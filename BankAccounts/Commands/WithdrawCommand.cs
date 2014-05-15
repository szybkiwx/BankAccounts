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
    public class WithdrawCommand : Command
    {
        public WithdrawCommand(BADataContext ctx)
            : base(ctx)
        {
        }

        public override void Execute(BankAccount ba, string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("\"withdraw\" requires second argument");
            }

            decimal amount;
            try
            {
                amount = decimal.Parse(args[0]);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid amount format, please make sure the value you provided is a decimal number");
            }

            try
            {
                ba.WithdrawMoney(amount);
            }
            catch (Exception e)
            {
                throw new CommandException(e.Message);
            }

            Context.OperationHistorySet.Add(new OperationHistory()
            {
                Amount = amount,
                BankAccountId = ba.Id,
                OperationDate = DateTime.UtcNow,
                OperationType = OperationType.Withdrawal
            });
    
            Context.SaveChanges();
        }
    }
}
