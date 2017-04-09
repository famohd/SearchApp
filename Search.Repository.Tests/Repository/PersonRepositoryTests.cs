
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Search.Repository.Context;
using Search.Repository.Model;
using Search.Repository.Repos;
using System.Data.Entity;

namespace Search.Repository.Tests.Repository
{
    [TestClass]
    public class PersonRepositoryTests
    {
        [TestMethod]
        public void Repo_Insert_adds_to_persons()
        {
            var mockPersons = new Mock<DbSet<Person>>();
            var mockContext = new Mock<SearchContext>();
            mockContext.Setup(m => m.Persons).Returns(mockPersons.Object);
            var personRepo = new PersonRepository(mockContext.Object);

            personRepo.Insert(TedTurner);

            mockPersons.Verify(m => m.Add(It.IsAny<Person>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        private Person TedTurner
        {
            get
            {
                return new Person { FirstName = "Ted", LastName = "Turner", Age = 34, Address = "1212 Turning Point Dr", Interests = "Driving" };
            }
        }
    }
}
