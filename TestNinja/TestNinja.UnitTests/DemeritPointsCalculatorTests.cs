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
    class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _demeritPointsCalculator;

        [SetUp]
        public void SetUp()
        {
            _demeritPointsCalculator = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-55)]
        public void CalculateDemeritPoints_SpeedIsLessThanZero_ThrowsArgumentOutOfRangeException(int speed)
        {
            Assert.That(()=>_demeritPointsCalculator.CalculateDemeritPoints(speed),
                Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(355)]
        [TestCase(301)]
        public void CalculateDemeritPoints_SpeedGreaterThanMaxSpeed_ThrowsArgumentOutOfRangeException(int speed)
        {
            Assert.That(() => _demeritPointsCalculator.CalculateDemeritPoints(speed),
               Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(50,0)]
        [TestCase(70,1)]
        [TestCase(80,3)]
        public void CalculateDemeritPoints_ValidSpeed_ReturnsApplicableDemeritPoints(int speed, int expectedOutput)
        {
            var result = _demeritPointsCalculator.CalculateDemeritPoints(speed);

            Assert.That(result, Is.EqualTo(expectedOutput));
        }
    }
}
