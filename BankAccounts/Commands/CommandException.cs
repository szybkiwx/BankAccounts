﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Commands
{
    public class CommandException : Exception
    {
        public CommandException(string msg) : base(msg)
        {
        }
    }
}
