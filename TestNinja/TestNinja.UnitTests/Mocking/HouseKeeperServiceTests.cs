using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class HouseKeeperServiceTests
    {
        private Mock<IHouseKeeperRepository> _repository;
        private Mock<IStatementHelper> _statementHelper;
        private Mock<IEmailHelper> _emailHelper;
        private Mock<IXtraMessageBox> _xtraMessageHelper;
        private Housekeeper _housekeeper;
        private DateTime _statementDate = new DateTime(2017,1,1);
        private HousekeeperService _service;
        private string _statementFileName = "fileName";


        [SetUp]
        public void SetUp() 
        {
            _housekeeper = new Housekeeper {Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
            _repository = new Mock<IHouseKeeperRepository>();
            _repository.Setup(r => r.GetHouseKeepers()).Returns(new List<Housekeeper> 
            {
                _housekeeper
            }
            .AsQueryable());
            _statementHelper = new Mock<IStatementHelper>();
            _emailHelper = new Mock<IEmailHelper>();
            _xtraMessageHelper = new Mock<IXtraMessageBox>();
            _service = new HousekeeperService(_repository.Object,_emailHelper.Object,_statementHelper.Object,_xtraMessageHelper.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements() 
        {
            _service.SendStatementEmails(_statementDate);
            _statementHelper.Verify(sh => sh.SaveStatement(_housekeeper.Oid,_housekeeper.FullName,_statementDate));
        }

        [Test]
        public void SendStatementEmails_EmailIsNotAvaialble_DoNotGenerateStatement()
        {
            _housekeeper.Email = null;
            _service.SendStatementEmails(_statementDate);
            _statementHelper.Verify(sh => sh.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate),Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_SendEmailStatement()
        {
            _statementHelper.Setup(sh => sh.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate)).Returns(_statementFileName);
            _service.SendStatementEmails(_statementDate);
            _emailHelper.Verify(eh => eh.EmailFile(_housekeeper.Email,_housekeeper.StatementEmailBody, _statementFileName, It.IsAny<string>()));
        }

        [Test]
        public void SendStatementEmails_StatementIsNotAvaialble_DoNotSendEmail()
        {
            _statementHelper.Setup(sh => sh.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate)).Returns(()=>null);
            _service.SendStatementEmails(_statementDate);
            _emailHelper.Verify(eh => eh.EmailFile(_housekeeper.Email, _housekeeper.StatementEmailBody, _statementFileName, It.IsAny<string>()),Times.Never);
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_ShowMessage()
        {
            _statementHelper.Setup(sh => 
            sh.SaveStatement(
               It.IsAny<int>(),
               It.IsAny<string>(),
                _statementDate)).
                Returns(() => _statementFileName);

            _emailHelper.Setup(eh =>
            eh.EmailFile(
            It.IsAny<string>(),
            It.IsAny<string>(),
            _statementFileName,
            It.IsAny<string>()))
            .Throws<Exception>();
            
            _service.SendStatementEmails(_statementDate);

            _xtraMessageHelper.Verify(xh => xh.Show(It.IsAny<string>(),It.IsAny<string>(),MessageBoxButtons.OK));
        }
    }
}
