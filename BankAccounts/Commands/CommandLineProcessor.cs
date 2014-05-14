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

        public CommandLineProcessor(CommandAggregate commandAggregate)
        {
            _commandAggregate = commandAggregate;
            _commands = new Dictionary<string, Command>()
            {
                { "add", commandAggregate.AddCommand },
                { "withdraw", commandAggregate.WithdrawCommand },
                { "statement", commandAggregate.BankStatement }
            };
        }

        public void ProcessCommand(string commandString)
        {
            var tokens = commandString.Split(new[] { ' ' }).Select(x => x.Trim());
            var command = tokens.First();
            var args = tokens.Skip(1);

            if (_commands.ContainsKey(command))
            {
                _commands[command].Execute(args);
            }
        }

        public void PrintCommands()
        {
            _commands.Keys.ToList().ForEach(x => Console.WriteLine(x));
        }
    }
}
