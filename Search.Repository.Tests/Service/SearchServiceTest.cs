using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Search.Repository.Model;
using Search.Repository.Repos;
using Search.Repository.Service;
using System.Collections.Generic;
using System.Linq;

namespace SearchDAL.Tests
{
    [TestClass]
    public class SearchServieTest
    {
        Mock<IPersonRepository> _repoFilled;
        Mock<IPersonRepository> _repoEmpty;

        [TestInitialize]
        public void Setup()
        {
           _repoFilled = new Mock<IPersonRepository>();
            _repoEmpty = new Mock<IPersonRepository>();
            SeedRepos();
        }

        [TestMethod]
        public void Svc_AddPerson_adds_to_Db()
        {
            var service = new SearchService(_repoEmpty.Object);
            service.AddPerson(PersonList.First());
            _repoEmpty.Verify(m => m.Insert(It.IsAny<Person>()), Times.Once);
        }

        [TestMethod]
        public void Svc_Get_returns_all_when_persons_present()
        {
            var service = new SearchService(_repoFilled.Object);
            var result = service.Get();
            Assert.AreEqual(PersonList.Count(), result.Count());
        }

        [TestMethod]
        public void Svc_Get_returns_empty_when_none_present()
        {
            var service = new SearchService(_repoEmpty.Object);
            var result = service.Get();
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void Svc_SearchByName_returns_persons_when_names_contains_search_value()
        {
            var service = new SearchService(_repoFilled.Object);
            var result = service.SearchBy("a");
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void Svc_SearchByName_returns_empty_when_names_not_contains_search_value()
        {
            var service = new SearchService(_repoFilled.Object);
            var result = service.SearchBy("Ba");
            Assert.AreEqual(0, result.Count());
        }


        #region privates

        private void SeedRepos()
        {
            _repoFilled.Setup(s => s.Select()).Returns(PersonList);
            _repoEmpty.Setup(s => s.Select()).Returns(new List<Person>());
            _repoEmpty.Setup(m => m.Insert(It.IsAny<Person>()));
        }

        private IEnumerable<Person> PersonList
        {
            get
            {
                var list = new List<Person>
                {
                    new Person { FirstName = "Johnny", LastName = "Walker"},
                    new Person { FirstName = "Pete", LastName = "Saunders"},
                    new Person { FirstName = "Bruce", LastName = "Willis"},
                    new Person { FirstName = "Steven", LastName = "Speigel"}
                };
                return list;
            }
        }

        #endregion
    }
}
