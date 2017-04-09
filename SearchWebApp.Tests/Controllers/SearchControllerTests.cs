using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SearchWebApp.Controllers;
using SearchWebApp.Service;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SearchWebApp.Tests.Controllers
{
    [TestClass]
    public class SearchControllerTests
    {
        Mock<ISearchApi> _searchApi;

        [TestInitialize]
        public void Setup()
        {
            _searchApi = new Mock<ISearchApi>();
        }


        [TestMethod]
        public async Task WebApp_Index_returns_all_persons_when_present()
        {
            _searchApi.Setup(m => m.Get(It.IsAny<string>())).Returns(Task.FromResult(IndexSuccessResponse));
            var controller = new SearchController(_searchApi.Object);

            var result = await controller.Index();

            _searchApi.Verify(m => m.Get(It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.AreEqual(PeopleList, ((JsonResult)result).Data);
        }

        [TestMethod]
        public async Task WebApp_Index_returns_error_message_on_wep_api_error()
        {
            _searchApi.Setup(m => m.Get(It.IsAny<string>())).Returns(Task.FromResult(IndexFailureResponse));
            var controller = new SearchController(_searchApi.Object);

            var result = await controller.Index();

            _searchApi.Verify(m => m.Get(It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
            Assert.AreEqual(ErrorMessage, ((HttpNotFoundResult)result).StatusDescription);
        }


        [TestMethod]
        public async Task WebApp_ByName_returns_matching_persons_when_present()
        {
            _searchApi.Setup(m => m.GetByName(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(ByNameSuccessResponse));
            var controller = new SearchController(_searchApi.Object);

            var result = await controller.ByName("j");

            _searchApi.Verify(m => m.GetByName(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.AreEqual(ThreePeople, ((JsonResult)result).Data);
        }

        [TestMethod]
        public async Task WebApp_ByName_returns_error_message_on_wep_api_error()
        {
            _searchApi.Setup(m => m.GetByName(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(FailureResponse));
            var controller = new SearchController(_searchApi.Object);

            var result = await controller.ByName("vv");

            _searchApi.Verify(m => m.GetByName(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
            Assert.AreEqual(ErrorMessage, ((HttpNotFoundResult)result).StatusDescription);
        }

        [TestMethod]
        public async Task WebApp_ByName_returns_no_content_status_when_no_parameter_passed()
        {
            _searchApi.Setup(m => m.GetByName(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(NoContentResponse));
            var controller = new SearchController(_searchApi.Object);

            var result = await controller.ByName("");

            _searchApi.Verify(m => m.GetByName(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(EmptyResult));
        }



        [TestMethod]
        public async Task WebApp_Create_returns_ok_on_successful_insert()
        {
            _searchApi.Setup(m => m.Add(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(OkResponse));
            var controller = new SearchController(_searchApi.Object);

            var result = await controller.Create(JohnSmith);

            _searchApi.Verify(m => m.Add(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(ContentResult));
            Assert.AreEqual(Ok, ((ContentResult)result).Content);
        }


        [TestMethod]
        public async Task WebApp_Create_returns_error_on_insert_failure()
        {
            _searchApi.Setup(m => m.Add(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(FailureResponse));
            var controller = new SearchController(_searchApi.Object);

            var result = await controller.Create("");

            _searchApi.Verify(m => m.Add(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
            Assert.AreEqual(ErrorMessage, ((HttpNotFoundResult)result).StatusDescription);
        }

        #region privates


        private string JohnSmith
        {
            get
            {
                return "{\"Id\":1,\"FirstName\":\"John\",\"LastName\":\"Smith\",\"Age\":32,\"Address\":\"123 Oak street\",\"Interests\":\"Hiking\",\"Picture\":null}";
            }
        }

        private string ThreePeople
        {
            get
            {
                return "[{\"Id\":1,\"FirstName\":\"John\",\"LastName\":\"Smith\",\"Age\":32,\"Address\":\"123 Oak street\",\"Interests\":\"Hiking\",\"Picture\":null,{\"Id\":3,\"FirstName\":\"John\",\"LastName\":\"Ferrow\",\"Age\":41,\"Address\":\"833 Plymount ln\",\"Interests\":\"Voluntering\",\"Picture\":null},{\"Id\":4,\"FirstName\":\"Joan\",\"LastName\":\"Hadley\",\"Age\":26,\"Address\":\"5484 ParkView stret\",\"Interests\":\"Dancing\",\"Picture\":null}]";
            }
        }

        private string PeopleList
        {
            get
            {
                return "[{\"Id\":1,\"FirstName\":\"John\",\"LastName\":\"Smith\",\"Age\":32,\"Address\":\"123 Oak street\",\"Interests\":\"Hiking\",\"Picture\":null},{\"Id\":2,\"FirstName\":\"Brian\",\"LastName\":\"Dexter\",\"Age\":25,\"Address\":\"7283 Palm Dr\",\"Interests\":\"Travel\",\"Picture\":null},{\"Id\":3,\"FirstName\":\"John\",\"LastName\":\"Ferrow\",\"Age\":41,\"Address\":\"833 Plymount ln\",\"Interests\":\"Voluntering\",\"Picture\":null},{\"Id\":4,\"FirstName\":\"Joan\",\"LastName\":\"Hadley\",\"Age\":26,\"Address\":\"5484 ParkView stret\",\"Interests\":\"Dancing\",\"Picture\":null},{\"Id\":5,\"FirstName\":\"Brett\",\"LastName\":\"Schumaker\",\"Age\":38,\"Address\":\"9803 Marriot court\",\"Interests\":\"Movies\",\"Picture\":null}]";
            }
        }

        private string ErrorMessage
        {
            get
            {
                return "An error occured";
            }
        }

        private string Ok
        {
            get
            {
                return "OK";
            }
        }

        private HttpResponseMessage IndexSuccessResponse
        {
            get
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                response.Content = new StringContent(PeopleList, Encoding.UTF8, "application/json");
                return response;
            }
        }

        private HttpResponseMessage IndexFailureResponse
        {
            get
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                response.Content = new StringContent(ErrorMessage);
                return response;
            }
        }


        private HttpResponseMessage ByNameSuccessResponse
        {
            get
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                response.Content = new StringContent(ThreePeople, Encoding.UTF8, "application/json");
                return response;
            }
        }

        private HttpResponseMessage FailureResponse
        {
            get
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                response.Content = new StringContent(ErrorMessage);
                return response;
            }
        }

        private HttpResponseMessage NoContentResponse
        {
            get
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
                return response;
            }
        }


        private HttpResponseMessage OkResponse
        {
            get
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                response.Content = new StringContent(Ok);
                return response;
            }
        }

        #endregion
    }
}
