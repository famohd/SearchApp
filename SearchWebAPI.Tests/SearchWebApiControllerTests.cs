using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchAPI.Controllers;
using Search.Repository.Service;
using Moq;
using System.Linq;
using Search.Repository.Model;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Results;
using System.Web.Http.ModelBinding;

namespace SearchWebAPI.Tests
{
    [TestClass]
    public class SearchWebApiControllerTests
    {
        Mock<ISearchService> _searchService;

        [TestInitialize]
        public void Setup()
        {
            _searchService = new Mock<ISearchService>();
        }

        [TestMethod]
        public void Api_Get_returns_all_persons_when_present()
        {
            _searchService.Setup(m => m.Get()).Returns(Persons);
            var controller = new SearchController(_searchService.Object);

            var result = controller.Get();
            var contents = ((JsonResult<IEnumerable<Person>>)result).Content as IEnumerable<Person>;

            _searchService.Verify(m => m.Get(), Times.Once);
            Assert.AreEqual(Persons.Count(), contents.Count());
        }

        [TestMethod]
        public void Api_Get_returns_empty_when_none_present()
        {
            _searchService.Setup(m => m.Get()).Returns(new List<Person>());
            var controller = new SearchController(_searchService.Object);

            var result = controller.Get();

            _searchService.Verify(m => m.Get(), Times.Once);
            Assert.AreEqual(HttpStatusCode.NoContent, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void Api_GetByName_returns_no_content_when_no_value_passed()
        {
            _searchService.Setup(m => m.Get()).Returns(Persons);
            var controller = new SearchController(_searchService.Object);

            var result = controller.GetByName("");

            _searchService.Verify(m => m.SearchBy(It.IsAny<string>()), Times.Never);
            Assert.AreEqual(HttpStatusCode.NoContent, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void Api_GetName_returns_match_when_value_passed()
        {
            var expectedCount = 2;
            _searchService.Setup(m => m.SearchBy(It.IsAny<string>())).Returns( Persons.Take(expectedCount));
            var controller = new SearchController(_searchService.Object);

            var result = controller.GetByName("j");
            var contents = ((JsonResult<IEnumerable<Person>>)result).Content as IEnumerable<Person>;

            _searchService.Verify(m => m.SearchBy(It.IsAny<string>()), Times.Once);
            Assert.AreEqual(expectedCount, contents.Count());
        }


        [TestMethod]
        public void Api_Post_ignores_when_no_person_provided()
        {
            _searchService.Setup(m => m.AddPerson(It.IsAny<Person>()));
            var controller = new SearchController(_searchService.Object);

            var result = controller.Post(null);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _searchService.Verify(m => m.AddPerson(It.IsAny<Person>()), Times.Never);
        }

        [TestMethod]
        public void Api_Post_returns_match_when_value_provided()
        {
            _searchService.Setup(m => m.AddPerson(It.IsAny<Person>()));
            var controller = new SearchController(_searchService.Object);

            var result = controller.Post(KevinBecker);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            _searchService.Verify(m => m.AddPerson(It.IsAny<Person>()), Times.Once);
        }


        private IEnumerable<Person> Persons
        {
            get
            {
                return new List<Person>
                {
                    new Person {FirstName = "Joe", LastName = "Miller" },
                    new Person {FirstName = "Jim", LastName = "Boise" },
                    new Person {FirstName = "Katie", LastName = "Lermann" }
                };
            }
        }

        public Person KevinBecker
        {
            get
            {
                return new Person { FirstName = "Kevin", LastName = "Becker" };
            }
        }


        public Person Sarah
        {
            get
            {
                return new Person { FirstName = "Sarah" };
            }
        }
    }
    
}
