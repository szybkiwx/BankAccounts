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
    public class BankStatementCommand : Command
    {
        public BankStatementCommand(BADataContext ctx)
            : base(ctx)
        {
        }

        public override void Execute(BankAccount ba, string[] args)
        {
            if (args.Length < 2)
            {
                throw new ArgumentException("\"statement \" requires two arguments");
            }


            DateTime dateFrom;
            DateTime dateTo;

            try
            {
                dateFrom = DateTime.Parse(args[0]);
                dateTo = DateTime.Parse(args[1]);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid date format");
            }

            var history = Context.OperationHistorySet.Where(x => x.OperationDate >= dateFrom && x.OperationDate < dateTo).AsEnumerable();
            string text = history.Aggregate<OperationHistory, string>(
                string.Empty,
                (string total, OperationHistory next) =>
                {
                    string type = next.OperationType == OperationType.Deposit ? "deposit" : "withdrawal";
                    return total + string.Format("{0}. {1} - {2}\n", next.OperationDate, type, next.BankAccount.Amount);
                });

            Console.WriteLine("Account statement: \n");
            Console.WriteLine(string.Format("Balance: {0}\n", ba.Amount));
            Console.WriteLine(text);
        }
    }
}
