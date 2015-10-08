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
    public class TestNoticeService
    {
        private INoticeService _noticeServiceMock;
        Mock<IContext> _contextMock;
        Mock<DbSet<Notice>> _setMock;
        IQueryable<Notice> _noticeList;
        
        [TestInitialize]
        public void Initialize()
        {
            _noticeList = new List<Notice>
                           {
                               new Notice() {Id = 1, Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit."},
                               new Notice() {Id = 2, Text = "Duis aute irure dolor in reprehenderit in voluptate velit."},
                               new Notice() {Id = 3, Text = "Excepteur sint occaecat cupidatat non proident, sunt in culpa."}
                           }.AsQueryable();

            _setMock = new Mock<DbSet<Notice>>();
            _setMock.As<IQueryable<Notice>>().Setup(m => m.Provider).Returns(_noticeList.Provider);
            _setMock.As<IQueryable<Notice>>().Setup(m => m.Expression).Returns(_noticeList.Expression);
            _setMock.As<IQueryable<Notice>>().Setup(m => m.ElementType).Returns(_noticeList.ElementType);
            _setMock.As<IQueryable<Notice>>().Setup(m => m.GetEnumerator()).Returns(_noticeList.GetEnumerator());

            _contextMock = new Mock<IContext>();
            _contextMock.Setup(c => c.Set<Notice>()).Returns(_setMock.Object);
            _contextMock.Setup(c => c.Notices).Returns(_setMock.Object);

            _noticeServiceMock = new NoticeService(_contextMock.Object);
        }

        [TestMethod]
        public void GetAllNotices()
        {
            //Act
            List<Notice> results = _noticeServiceMock.GetAll().ToList();

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
        }

        [TestMethod]
        public void CreateNotice()
        {
            //Arrange
            Notice notice = new Notice() { Text = "xxx yyy zzz" };

            _setMock.Setup(m => m.Add(notice)).Returns((Notice e) =>
                                                      {
                                                          e.Id = 1;
                                                          return e;
                                                      });

            //Act
            _noticeServiceMock.Insert(notice);

            //Assert
            Assert.AreEqual(1, notice.Id);
            _contextMock.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}