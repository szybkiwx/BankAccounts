using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Commands
{
    public interface ICommandLineProcessor
    {
        void ProcessCommand(string command);
        void PrintCommands();

    }
}
