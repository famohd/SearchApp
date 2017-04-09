using LogWrapper;
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
        private readonly ICoreLogger _logger;

        public SearchController(ISearchApi searchApi, ICoreLogger logger)
        {
            _searchApi = searchApi;
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                Thread.Sleep(2000);
                var response = await _searchApi.Get("api/v1/search");
                return await GetResponse(response);
            }
            catch (HttpResponseException hrex)
            {
                _logger.Error(hrex);
                return HttpNotFound(string.Format("An error occured:", hrex.Message));
            }
        }


        [Route("name/{nameExpr}")]
        public async Task<ActionResult> ByName(string nameExpr)
        {
            try
            {
                Thread.Sleep(2000);
                var response = await _searchApi.GetByName("api/v1/search", nameExpr);
                return await GetResponse(response);
            }
            catch(HttpResponseException hrex)
            {
                _logger.Error(hrex);
                return HttpNotFound(string.Format("An error occured:", hrex.Message));
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("create")]
        [Route("{personData}")]
        public async Task<ActionResult> Create([FromBody]string personData)
        {
            try
            {
                Thread.Sleep(2000);
                var response = await _searchApi.Add("api/v1/search", personData);
                if (response.Content == null)
                {
                    _logger.Warn("No contents returned on response.");
                    return new EmptyResult();
                }
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return Content(response.StatusCode.ToString());
                }
                _logger.Error(string.Format("Error on response: {0}", response.ReasonPhrase));
                return HttpNotFound("An error occured");
            }
            catch (HttpResponseException hrex)
            {
                _logger.Error(hrex);
                return HttpNotFound(string.Format("An error occured:" ,hrex.Message));
            }
        }



        private async Task<ActionResult> GetResponse(System.Net.Http.HttpResponseMessage response)
        {
            if (response.Content == null)
            {
                _logger.Warn("No contents returned on response.");
                return new EmptyResult();
            }
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            _logger.Error(string.Format("Error on response: {0}", response.ReasonPhrase));
            return HttpNotFound("An error occured");
        }
    }
}