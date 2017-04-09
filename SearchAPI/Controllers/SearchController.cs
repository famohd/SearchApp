using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Search.Repository.Service;
using Search.Repository.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SearchAPI.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return _searchService.Get();
        }

        //// GET api/values/jo
        [HttpGet("{name}")]
        public IEnumerable<Person> Get(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<Person>();

            return _searchService.SearchBy(name);
        }

        // POST api/values
        [HttpPost]
        public void Add([FromBody]Person person)
        {
            if(person != null)
            {
                _searchService.AddPerson(person);
            }
        }

        //// PUT api/values/5

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
