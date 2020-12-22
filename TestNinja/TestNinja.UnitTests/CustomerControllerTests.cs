using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class CustomerControllerTests
    {
        private CustomerController _customerController;
        
        [SetUp]
        public void SetUp() 
        {
            _customerController = new CustomerController();
        }

        [Test]
        public void GetCustomer_CustomerIdIsZero_ReturnsNotFound() 
        {
            var result = _customerController.GetCustomer(0);
           
            Assert.That(result,Is.TypeOf<NotFound>());            
        }

        [Test]
        public void GetCustomer_CustomerIdIsNotZero_ReturnsOk()
        {
            var result = _customerController.GetCustomer(5);

            Assert.That(result, Is.InstanceOf<Ok>());
        }
    }
}
