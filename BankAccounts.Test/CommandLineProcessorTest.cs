using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccounts.Commands;
using BankAccounts.Data;
using Moq;
using System.Data.Entity;
using BankAccounts.Data.Entities;

namespace BankAccounts.Test
{
    [TestClass]
    public class CommandLineProcessorTest
    {
        private Mock<BADataContext> _contextMock;
        //private Mock<DbSet<SavingsBankAccount>> _savingsBaSetMock;
        //private Mock<DbSet<CheckedBankAccount>> _checkedBaSetMock;
        //private Mock<DbSet<CheckedBankAccount>> _checkedBaSetMock;

        private Mock<BankStatementCommand> _bankStatementCommand;
        private Mock<DepositCommand> _depositCommand;
        private Mock<WithdrawCommandTest> _withdrawCommand;
        private Mock<CommandAggregate> _cmdAggregate;
        private Mock<ICommandHelper> _cmdHelper;
        private CommandLineProcessor _service;
        [TestInitialize]
        public void Initialize()
        {
            //_contextMock = new Mock<BADataContext>();
            _cmdAggregate = new Mock<CommandAggregate>();
            _bankStatementCommand = new Mock<BankStatementCommand>();
            _cmdAggregate.Setup(x => x.BankStatementCommand).Returns(_bankStatementCommand.Object);
            _cmdAggregate.Setup(x => x.DepositCommand).Returns(_depositCommand.Object);
            //_cmdAggregate.Setup(x => x.WithdrawCommand).Returns(_withdrawCommand.Object);
            _cmdHelper = new Mock<ICommandHelper>();

            _service = new CommandLineProcessor(_cmdAggregate.Object, _cmdHelper.Object);
        }

        [TestMethod]
        public void Process_GivenDepositCommandDepositCommandInvoked()
        {

            _service.ProcessCommand("deposit checked 40");
        }
    }
}
