using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp() 
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }
        [Test]
        [TestCase("Rudresh","Pranav",true)]
        public void DownloadInstaller_SuccessfulDownload_ReturnsTrue(string customerName, string installerName, bool expectedOutput) 
        {            
            _fileDownloader.Setup(fd => fd.DownloadFile(It.IsAny<string>(),null));

            var result = _installerHelper.DownloadInstaller(customerName,installerName);

            Assert.That(result,Is.EqualTo(expectedOutput));
        }

        [Test]
        [TestCase("Rudresh", "Pranav", false)]
        public void DownloadInstaller_ErrornousDownload_ReturnsFalse(string customerName, string installerName, bool expectedOutput)
        {                   
            _fileDownloader.Setup(fd => fd.DownloadFile(It.IsAny<string>(), null))
                           .Throws<WebException>();

            var result = _installerHelper.DownloadInstaller(customerName,installerName);

            Assert.That(result, Is.EqualTo(expectedOutput));
        }
    }
}
