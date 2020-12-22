using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class HtmlFormatterTests
    {
        [Test]
        public void FormatAsBold_WhenCalled_ReturnsBoldString() 
        {
            var formatter = new HtmlFormatter();

            var result = formatter.FormatAsBold("Rudresh");

            //Specific
            Assert.That(result, Is.EqualTo("<strong>Rudresh</strong>"));
            ///or
            //More General
            Assert.That(result, Does.StartWith("<strong>"));
            Assert.That(result, Does.EndWith("</strong>"));
            Assert.That(result, Does.Contain("Rudresh"));
        }
    }
}
