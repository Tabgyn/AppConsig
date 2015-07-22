using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AppConsig.Dados;
using AppConsig.Entidades;
using AppConsig.Servicos;
using AppConsig.Servicos.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AppConsig.Testes.Servicos
{
    [TestClass]
    public class ServicoAvisoTeste
    {
        private IServicoAviso _servicoAvisoMock;
        Mock<IContexto> _contextoMock;
        Mock<DbSet<Aviso>> _setMock;
        IQueryable<Aviso> _listaAvisos;

        [TestInitialize]
        public void Initialize()
        {
            _listaAvisos = new List<Aviso>
                           {
                               new Aviso() {Id = 1, Texto = "Lorem ipsum dolor sit amet, consectetur adipiscing elit."},
                               new Aviso() {Id = 2, Texto = "Duis aute irure dolor in reprehenderit in voluptate velit."},
                               new Aviso() {Id = 3, Texto = "Excepteur sint occaecat cupidatat non proident, sunt in culpa."}
                           }.AsQueryable();

            _setMock = new Mock<DbSet<Aviso>>();
            _setMock.As<IQueryable<Aviso>>().Setup(m => m.Provider).Returns(_listaAvisos.Provider);
            _setMock.As<IQueryable<Aviso>>().Setup(m => m.Expression).Returns(_listaAvisos.Expression);
            _setMock.As<IQueryable<Aviso>>().Setup(m => m.ElementType).Returns(_listaAvisos.ElementType);
            _setMock.As<IQueryable<Aviso>>().Setup(m => m.GetEnumerator()).Returns(_listaAvisos.GetEnumerator());

            _contextoMock = new Mock<IContexto>();
            _contextoMock.Setup(c => c.Set<Aviso>()).Returns(_setMock.Object);
            _contextoMock.Setup(c => c.Avisos).Returns(_setMock.Object);

            _servicoAvisoMock = new ServicoAviso(_contextoMock.Object);
        }

        [TestMethod]
        public void ObterTodosAvisos()
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
            int Id = 1;
            Aviso aviso = new Aviso() { Texto = "xxx yyy zzz" };

            _setMock.Setup(m => m.Add(aviso)).Returns((Aviso e) =>
                                                      {
                                                          e.Id = Id;
                                                          return e;
                                                      });

            //Act
            _servicoAvisoMock.Criar(aviso);

            //Assert
            Assert.AreEqual(Id, aviso.Id);
            _contextoMock.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}