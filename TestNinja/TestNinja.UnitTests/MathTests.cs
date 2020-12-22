using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class MathTests
    {
        private Math _math;

        [SetUp]
        public void SetUp() 
        {
            _math = new Math();
        }

        [Test]
        public void Add_WhenCalled_ReturnsSumOfArguments() 
        {
            var result = _math.Add(1,2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Max_FirstArgumentIsGreaterThanSecond_ReturnsFirstArgument() 
        {
            var result = _math.Max(2,1);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_SecondArgumentIsGreaterThanFirst_ReturnsSecondArgument()
        {           
            var result = _math.Max(1, 2);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        [Ignore("For a business reason")]
        public void Max_SecondArgumentIsEqualToFirst_ReturnsSameArgument()
        {         
            var result = _math.Max(2, 2);

            Assert.That(result, Is.EqualTo(2));
        }

        /// <summary>
        /// Parameterized Tests, using this we can avoid writing multiple methods like above and couple them
        /// into single method like this one.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="expectedOutput"></param>
        [Test]
        [TestCase(1, 2, 2)]
        [TestCase(2, 1, 2)]
        [TestCase(2, 2, 2)]
        public void Max_WhenCalled_ReturnsGreaterArgument(int a,int b, int expectedOutput) 
        {
            var result = _math.Max(a, b);

            Assert.That(result,Is.EqualTo(expectedOutput));
        }

        [Test]
        public void GetOddNumbers_WhenCalled_ReturnsOddNumbersUptoLimit()
        {
            var result = _math.GetOddNumbers(5);

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));
        }
    }
}
