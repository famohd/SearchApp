using Search.Repository.Model;
using Search.Repository.Repos;
using System.Collections.Generic;
using System.Linq;

namespace Search.Repository.Service
{
    public class SearchService : ISearchService
    {
        private readonly IPersonRepository _personRepository;

        public SearchService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public void AddPerson(Person person)
        {
            _personRepository.Insert(person);
        }

        public IEnumerable<Person> Get()
        {
            return _personRepository.Select();
        }

        public IEnumerable<Person> SearchBy(string nameExpr)
        {
            var persons = _personRepository.Select(); 
            var lowerExpr = nameExpr.ToLower();
            return from person in persons
                   where person.FirstName.ToLower().Contains(lowerExpr) ||
                         person.LastName.ToLower().Contains(lowerExpr)
                   select person;
        }
    }
}
