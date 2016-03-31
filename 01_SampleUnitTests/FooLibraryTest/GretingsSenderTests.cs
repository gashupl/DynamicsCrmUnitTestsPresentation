using FooLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using FooLibrary.Interfaces;

namespace FooLibraryTest
{
    [TestClass]
    public class GretingsSenderTests
    {
        #region - Version A (bad approach :))
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Send_IncorrectInput_ThrowsArgumentException()
        //{

        //    GretingsSender sender = new GretingsSender();
        //    sender.Send(String.Empty, string.Empty);
        //}

        //[TestMethod]
        //public void Send_CorrectInput_SendSuccess()
        //{
        //    GretingsSender sender = new GretingsSender();
        //    sender.Send("hello", "piotrg@netwise.pl");

        //    Assert.IsTrue(true); 
        //}
        #endregion

        #region - Version B
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Send_IncorrectInput_ThrowsArgumentException()
        {

            Mock<IMailService> service = new Mock<IMailService>();
            service.Setup(srv => srv.Send(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            GretingsSender sender = new GretingsSender(service.Object);
            sender.Send(String.Empty, string.Empty);
        }

        [TestMethod]
        public void Send_CorrectInput_SendSuccess()
        {

            Mock<IMailService> service = new Mock<IMailService>();
            service.Setup(srv => srv.Send(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            GretingsSender sender = new GretingsSender(service.Object);
            sender.Send("hello", "piotrg@netwise.pl");

            Assert.IsTrue(true);
        }
        #endregion
    }
}
