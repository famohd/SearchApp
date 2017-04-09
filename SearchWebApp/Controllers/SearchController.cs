using SearchWebApp.Service;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace SearchWebApp.Controllers
{
    [RoutePrefix("search")]
    public class SearchController : Controller
    {
        private readonly ISearchApi _searchApi;

        public SearchController(ISearchApi searchApi)
        {
            _searchApi = searchApi;
        }

        public async Task<ActionResult> Index()
        {
            Thread.Sleep(2000);
            var response = await _searchApi.Get("api/v1/search");
            if (response.Content == null)
            {
                return new EmptyResult();
            }
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound("An error occured");
            
        }

        [Route("name/{nameExpr}")]
        public async Task<ActionResult> ByName(string nameExpr)
        {
            Thread.Sleep(2000);
            var response = await _searchApi.GetByName("api/v1/search", nameExpr);
            if(response.Content == null)
            {
                return new EmptyResult();
            }
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound("An error occured");
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("create")]
        [Route("{personData}")]
        public async Task<ActionResult> Create([FromBody]string personData)
        {
            Thread.Sleep(2000);
            var response = await _searchApi.Add("api/v1/search", personData);
            if (response.Content == null)
            {
                return new EmptyResult();
            }
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return Content(response.StatusCode.ToString());
            }
            return HttpNotFound("An error occured");

        }
    }
}