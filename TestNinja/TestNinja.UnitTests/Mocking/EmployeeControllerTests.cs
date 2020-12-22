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
    class EmployeeControllerTests
    {
        [Test]
        public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDb() 
        {
            var repository = new Mock<IEmployeeRepository>();
            var controller = new EmployeeController(repository.Object);

            controller.DeleteEmployee(1);

            repository.Verify(s => s.DeleteEmployee(1));
        }

        [Test]
        public void DeleteEmployee_WhenCalled_ReturnsRedirectResult()
        {
            var repository = new Mock<IEmployeeRepository>();
            var controller = new EmployeeController(repository.Object);            

            var result = controller.DeleteEmployee(1);

            Assert.That(result,Is.TypeOf<RedirectResult>());
        }
    }
}
