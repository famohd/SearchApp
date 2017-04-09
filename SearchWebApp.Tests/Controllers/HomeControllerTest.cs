using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchWebApp.Controllers;

namespace SearchWebApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Home_Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
