﻿using BankAccounts.Data;
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

            try
            {
                decimal amount = decimal.Parse(args[1]);
                ba.WithdrawMoney(amount);
                Context.OperationHistorySet.Add(new OperationHistory()
                {
                    Amount = amount,
                    BankAccountId = ba.Id,
                    OperationDate = DateTime.UtcNow,
                    OperationType = OperationType.Withdrawal
                });
                Context.SaveChanges();
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid amount format, please make sure the value you provided is a decimal number");
            }
        }
    }
}
