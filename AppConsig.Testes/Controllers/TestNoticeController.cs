using System.Collections.Generic;
using System.Web.Mvc;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;
using AppConsig.Web.Gestor.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AppConsig.Tests.Controllers
{
    [TestClass]
    public class TestNoticeController
    {
        private Mock<INoticeService> _servicoMock;
        AvisoController _avisoController;
        List<Notice> _listNotices;

        [TestInitialize]
        public void Initialize()
        {

            _servicoMock = new Mock<INoticeService>();
            _avisoController = new AvisoController(_servicoMock.Object);
            _listNotices = new List<Notice>()
                           {
                               new Notice() {Id = 1, Text = "XXX"},
                               new Notice() {Id = 2, Text = "YYY"},
                               new Notice() {Id = 3, Text = "ZZZ"}
                           };
        }



        [TestMethod]
        public void GetAllNotices()
        {
            //Arrange 
            _servicoMock.Setup(x => x.GetAll(null)).Returns(_listNotices);

            //Act 
            var result = ((_avisoController.Index("", "", "", null, null) as ViewResult).Model) as List<Notice>;

            //Assert 
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual("XXX", result[0].Text);
            Assert.AreEqual("YYY", result[1].Text);
            Assert.AreEqual("ZZZ", result[2].Text);

        }

        [TestMethod]
        public void CreateValidNotice()
        {
            //Arrange 
            Notice c = new Notice() { Text = "XXX YYY ZZZ" };

            //Act 
            var result = (RedirectToRouteResult)_avisoController.Criar(c);

            //Assert 
            _servicoMock.Verify(m => m.Insert(c), Times.Once);
            Assert.AreEqual("Index", result.RouteValues["action"]);

        }

        [TestMethod]
        public void CreateInvalidNotice()
        {
            // Arrange 
            Notice c = new Notice() { Text = "" };
            _avisoController.ModelState.AddModelError("Error", "Something went wrong");

            //Act 
            var result = (ViewResult)_avisoController.Criar(c);

            //Assert 
            _servicoMock.Verify(m => m.Insert(c), Times.Never);
            Assert.AreEqual("", result.ViewName);
        }
    }
}