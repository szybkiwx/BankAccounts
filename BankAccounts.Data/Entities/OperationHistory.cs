using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Data.Entities
{
    public class OperationHistory
    {
        public int Id { get; set; }

        public BankAccount BankAccount { get; set; }

        public DateTime OperationDate { get; set; }
    }
}
