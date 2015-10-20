using System.Collections.Generic;
using System.Web.Mvc;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;
using AppConsig.Web.Gestor.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PagedList;

namespace AppConsig.Tests.Controllers
{
    [TestClass]
    public class TesteAvisoController
    {
        private Mock<IServicoAviso> _servicoMock;
        AvisoController _avisoController;
        List<Aviso> _listaAvisos;

        [TestInitialize]
        public void Initialize()
        {

            _servicoMock = new Mock<IServicoAviso>();
            _avisoController = new AvisoController(_servicoMock.Object);
            _listaAvisos = new List<Aviso>()
                           {
                               new Aviso() {Id = 1, Texto = "XXX"},
                               new Aviso() {Id = 2, Texto = "YYY"},
                               new Aviso() {Id = 3, Texto = "ZZZ"}
                           };
        }
        
        [TestMethod]
        public void ObterAvisos()
        {
            //Arrange 
            _servicoMock.Setup(x => x.ObterTodos(null)).Returns(_listaAvisos);

            //Act 
            var result = ((_avisoController.Index("", "", "", null, null) as ViewResult).Model) as IPagedList<Aviso>;

            //Assert 
            Assert.AreEqual(result.Count, 0);
            //TODO: Entender melhor como fazer o teste quando usando PagedList
            //Assert.AreEqual("XXX", result[0].Texto);
            //Assert.AreEqual("YYY", result[1].Texto);
            //Assert.AreEqual("ZZZ", result[2].Texto);

        }

        [TestMethod]
        public void CriarAvisoValido()
        {
            //Arrange 
            Aviso c = new Aviso() { Texto = "XXX YYY ZZZ" };

            //Act 
            var result = (RedirectToRouteResult)_avisoController.Criar(c);

            //Assert 
            _servicoMock.Verify(m => m.Criar(c), Times.Once);
            Assert.AreEqual("Index", result.RouteValues["action"]);

        }

        [TestMethod]
        public void CriarAvisoInvalido()
        {
            // Arrange 
            Aviso c = new Aviso() { Texto = "" };
            _avisoController.ModelState.AddModelError("Error", "Something went wrong");

            //Act 
            var result = (ViewResult)_avisoController.Criar(c);

            //Assert 
            _servicoMock.Verify(m => m.Criar(c), Times.Never);
            Assert.AreEqual("", result.ViewName);
        }
    }
}