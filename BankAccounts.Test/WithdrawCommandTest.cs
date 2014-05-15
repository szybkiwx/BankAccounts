using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccounts.Commands;
using BankAccounts.Data;
using Moq;
using BankAccounts.Data.Entities;
using System.Data.Entity;

namespace BankAccounts.Test
{
    [TestClass]
    public class WithdrawCommandTest
    {
        private SavingsBankAccount _savingsAccount;
        private Mock<DbSet<SavingsBankAccount>> _savingsAccountSet;
        private Mock<BADataContext> _context; 
        [TestInitialize]
        public void Initialize()
        {
            _savingsAccount = new SavingsBankAccount()
            {
                Amount = 13
            };
            _savingsAccountSet = new Mock<DbSet<SavingsBankAccount>>();
            _savingsAccountSet.Setup(x => x.First()).Returns(_savingsAccount);
            _context = new Mock<BADataContext>();
            _context.Setup(x => x.SavingsBankAccountSet).Returns(_savingsAccountSet.Object);
        }


        [TestMethod]
        public void Execute_GivenProperAmountIncreaseBanalce()
        {
            var command = new WithdrawCommand(_context.Object);
            command.Execute(_savingsAccount, new[] { "23" });
            Assert.AreEqual(13 + 23, _savingsAccount.Amount);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void Execute_GivenInproperlyFormattedAmountThrowException()
        {
            var command = new WithdrawCommand(_context.Object);
            command.Execute(_savingsAccount, new[] { "23f" });
        }
    }
}
