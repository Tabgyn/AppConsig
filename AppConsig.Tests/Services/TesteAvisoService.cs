using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services;
using AppConsig.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AppConsig.Tests.Services
{
    [TestClass]
    public class TesteAvisoService
    {
        private IServicoAviso _servicoAvisoMock;
        Mock<IContext> _contextMock;
        Mock<DbSet<Aviso>> _setMock;
        IQueryable<Aviso> _listaAviso;
        
        [TestInitialize]
        public void Initialize()
        {
            _listaAviso = new List<Aviso>
                           {
                               new Aviso() {Id = 1, Texto = "Lorem ipsum dolor sit amet, consectetur adipiscing elit."},
                               new Aviso() {Id = 2, Texto = "Duis aute irure dolor in reprehenderit in voluptate velit."},
                               new Aviso() {Id = 3, Texto = "Excepteur sint occaecat cupidatat non proident, sunt in culpa."}
                           }.AsQueryable();

            _setMock = new Mock<DbSet<Aviso>>();
            _setMock.As<IQueryable<Aviso>>().Setup(m => m.Provider).Returns(_listaAviso.Provider);
            _setMock.As<IQueryable<Aviso>>().Setup(m => m.Expression).Returns(_listaAviso.Expression);
            _setMock.As<IQueryable<Aviso>>().Setup(m => m.ElementType).Returns(_listaAviso.ElementType);
            _setMock.As<IQueryable<Aviso>>().Setup(m => m.GetEnumerator()).Returns(_listaAviso.GetEnumerator());

            _contextMock = new Mock<IContext>();
            _contextMock.Setup(c => c.Set<Aviso>()).Returns(_setMock.Object);
            _contextMock.Setup(c => c.Avisos).Returns(_setMock.Object);

            _servicoAvisoMock = new ServicoAviso(_contextMock.Object);
        }

        [TestMethod]
        public void ObterAvisos()
        {
            //Act
            List<Aviso> results = _servicoAvisoMock.ObterTodos().ToList();

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
        }

        [TestMethod]
        public void CriarAviso()
        {
            //Arrange
            Aviso notice = new Aviso() { Texto = "xxx yyy zzz" };

            _setMock.Setup(m => m.Add(notice)).Returns((Aviso e) =>
                                                      {
                                                          e.Id = 1;
                                                          return e;
                                                      });

            //Act
            _servicoAvisoMock.Criar(notice);

            //Assert
            Assert.AreEqual(1, notice.Id);
            _contextMock.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}