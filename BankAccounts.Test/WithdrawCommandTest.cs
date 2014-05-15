using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccounts.Commands;
using BankAccounts.Data;
using Moq;
using BankAccounts.Data.Entities;
using System.Data.Entity;
using System.Collections.Generic;

namespace BankAccounts.Test
{
    [TestClass]
    public class WithdrawCommandTest
    {
        private SavingsBankAccount _savingsAccount;
        private Mock<DbSet<SavingsBankAccount>> _savingsAccountSet;
        private Mock<DbSet<OperationHistory>> _operationHistorySet;
        private Mock<BADataContext> _context; 
        [TestInitialize]
        public void Initialize()
        {
            _savingsAccount = new SavingsBankAccount()
            {
                Amount = 47
            };

            var data = new List<SavingsBankAccount>()
            {
                _savingsAccount
            };

            _savingsAccountSet = new Mock<DbSet<SavingsBankAccount>>();
            TestHelper.SetupMockDbSet<SavingsBankAccount>(_savingsAccountSet, data);

            _operationHistorySet = new Mock<DbSet<OperationHistory>>();
            _context = new Mock<BADataContext>();
            _context.Setup(x => x.SavingsBankAccountSet).Returns(_savingsAccountSet.Object);
            _context.Setup(x => x.OperationHistorySet).Returns(_operationHistorySet.Object); ;
        }


        [TestMethod]
        public void Execute_GivenProperAmountIncreaseBanalce()
        {
            var command = new WithdrawCommand(_context.Object);
            command.Execute(_savingsAccount, new[] { "23" });
            Assert.AreEqual(47 - 23, _savingsAccount.Amount);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void Execute_GivenInproperlyFormattedAmountThrowException()
        {
            var command = new WithdrawCommand(_context.Object);
            command.Execute(_savingsAccount, new[] { "23f" });
        }

        [ExpectedException(typeof(CommandException))]
        [TestMethod]
        public void Execute_GivenAmountExceedingBalanceThrowExeption()
        {
            // balance overdrawn
            var command = new WithdrawCommand(_context.Object);
            command.Execute(_savingsAccount, new[] { "67" });
        }
    }
}
