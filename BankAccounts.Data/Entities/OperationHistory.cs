using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Data.Entities
{
    public enum OperationType 
    {
        Deposit, 
        Withdrawal
    }

    public class OperationHistory
    {
        public int Id { get; set; }

        public int BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public BankAccount BankAccount { get; set; }

        public DateTime OperationDate { get; set; }

        public OperationType OperationType { get; set; }

        public decimal Amount { get; set; }
    }
}
