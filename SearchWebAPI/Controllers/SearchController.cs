using System.Linq;
using Search.Repository.Service;
using Search.Repository.Model;
using System.Web.Http;
using LogWrapper;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SearchAPI.Controllers
{
    [RoutePrefix("api/v1/search")]
    public class SearchController : ApiController
    {
        private readonly ICoreLogger _logger;
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService, ICoreLogger logger)
        {
            _searchService = searchService;
            _logger = logger;
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var list = _searchService.Get();

                if (list.Any())
                {
                    return Json(list);
                }
                else
                {
                    _logger.Warn("No data content received from database.");
                    return StatusCode(System.Net.HttpStatusCode.NoContent);
                }
            }
            catch(HttpResponseException hrex)
            {
                _logger.Error(hrex);
                return StatusCode(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        [Route("{name}")]
        public IHttpActionResult GetByName(string name)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(name))
                {
                    return StatusCode(System.Net.HttpStatusCode.NoContent);
                }

                var list = _searchService.SearchBy(name);

                if (list.Any())
                {
                    return Json(list);
                }
                else
                {
                    _logger.Warn("No data content received from database.");
                    return StatusCode(System.Net.HttpStatusCode.NoContent);
                }
            }
            catch (HttpResponseException hrex)
            {
                _logger.Error(hrex);
                return StatusCode(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.Error("Model state is inalid. Cannot post person create.");
                    return BadRequest();
                }

                if (person != null)
                {
                    _searchService.AddPerson(person);
                    return Ok();
                }
                return BadRequest();
            }
            catch (HttpResponseException hrex)
            {
                _logger.Error(hrex);
                return StatusCode(System.Net.HttpStatusCode.InternalServerError);
            }
        }

    }
}
