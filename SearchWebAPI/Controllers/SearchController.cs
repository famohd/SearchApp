using System.Linq;
using Search.Repository.Service;
using Search.Repository.Model;
using System.Web.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SearchAPI.Controllers
{
    [RoutePrefix("api/v1/search")]
    public class SearchController : ApiController
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
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
                    return StatusCode(System.Net.HttpStatusCode.NoContent);
                }
            }
            catch
            {
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
                    return StatusCode(System.Net.HttpStatusCode.NoContent);
                }
            }
            catch
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Person person)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (person != null)
            {
                _searchService.AddPerson(person);
                return Ok();
            }
            return BadRequest();
        }

    }
}
