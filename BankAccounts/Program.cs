using BankAccounts.Commands;
using BankAccounts.Data;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
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
            var interestUpdate = _container.Resolve<IInterestUpdateService>();

            var interestThread = new Thread(interestUpdate.Run);
            interestThread.Start();
            string input;

            Console.WriteLine("Welcome to your banking software. Please enter appropriate command. Enter '?' to print help");
            
            do
            {
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
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
                        catch (CommandException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("operation unsuccessful");
                        }
                    }
                }
            }
            while (!input.StartsWith("exit"));
            interestUpdate.Stop();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("List of commands:\n" +
                "* deposit <account_type> <amount> - increments balance by the value of amount of account_type\n"+
                "* withdraw <account_type> <amount> - decreases balance by the value of amount of account_type\n"+
                "* statement <account_type> <date_from> <date_to> show account_type statement from period <date_from>:<date_to>. Format ofthe date should be YYYY-MM-DD HH:mm:ss, e.g. \"2011-03-21 13:26:21\" \n" +
                "* exit - quits the program");
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
            _container.RegisterType<IInterestUpdateService, InterestUpdateService>();
            
         }
    }
}
