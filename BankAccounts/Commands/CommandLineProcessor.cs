using BankAccounts.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Commands
{
    public class CommandLineProcessor : ICommandLineProcessor
    {
        private CommandAggregate _commandAggregate;
        private Dictionary<string, Command> _commands;
        private ICommandHelper _commandHelper;

        public CommandLineProcessor(CommandAggregate commandAggregate, ICommandHelper commandHelper)
        {
            _commandAggregate = commandAggregate;
            _commandHelper = commandHelper;
            _commands = new Dictionary<string, Command>()
            {
                { "deposit", commandAggregate.DepositCommand },
                { "withdraw", commandAggregate.WithdrawCommand },
                { "statement", commandAggregate.BankStatementCommand }
            };
        }

        public void ProcessCommand(string commandString)
        {
            var tokens = commandString.Split(new[] { ' ' }).Select(x => x.Trim()).ToArray();
            if (tokens.Length < 3)
            {
                throw new CommandException("Not enough arguments");
            }

            var commandName = tokens.First();
            
            
            if (_commands.ContainsKey(commandName))
            {
                try
                {
                    var command = _commands[commandName];
                    string type = tokens[1];
                    var ba = _commandHelper.GetAccountByType(type);

                    var args = tokens.Skip(2).ToArray();
                    command.Execute(ba, args);
                }
                catch (ArgumentException e)
                {
                    throw new CommandException(e.Message);
                }
            }
            else
            {
                throw new CommandException(string.Format("Invalid command {0}", commandName));
            }
        }

        public void PrintCommands()
        {
            _commands.Keys.ToList().ForEach(x => Console.WriteLine(x));
        }
    }
}
