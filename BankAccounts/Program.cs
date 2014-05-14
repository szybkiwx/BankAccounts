using BankAccounts.Commands;
using BankAccounts.Data;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts
{
    class Program
    {
        static UnityContainer _container;

        static void Main(string[] args)
        {
            Bootstrap();
            var processor = _container.Resolve<ICommandLineProcessor>();
            string input;

            Console.WriteLine("Welcome to your banking software. Please enter appropriate command. Enter '?' to print help");
            
            do
            {
                input = Console.ReadLine();
                if (input.StartsWith("?"))
                {
                    PrintHelp();
                }
                else
                {
                    try
                    {
                        processor.ProcessCommand(input);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            while (!input.StartsWith("exit"));
        }

        private static void PrintHelp()
        {
            Console.WriteLine("List of commands:\n" +
                "* deposit <account_type> <amount> - increments balance by the value of amount of account_type\n"+
                "* withdraw <account_type> <amount> - decreases balance by the value of amount of account_type\n"+
                "* statement <account_type> show account_type statement");
        }

        static void Bootstrap()
        {
            Database.SetInitializer<BADataContext>(new BADataContextInitializer());
            _container = new UnityContainer();
            _container.RegisterType<CommandAggregate>();
            _container.RegisterType<DepositCommand>();
            _container.RegisterType<WithdrawCommand>();
            _container.RegisterType<BankStatementCommand>();
            _container.RegisterType<ICommandHelper, CommandHelper>();
            _container.RegisterType<ICommandLineProcessor, CommandLineProcessor>();
            _container.RegisterType<DepositCommand>();
            _container.RegisterType<BADataContext>(new ContainerControlledLifetimeManager());
         }
    }
}
