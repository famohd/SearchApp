using Search.Repository.Context;
using Search.Repository.Model;
using System.Collections.Generic;
using System.Linq;

namespace Search.Repository.Repos
{
    public class PersonRepository : IPersonRepository
    {
        private readonly SearchContext _context;

        public PersonRepository(SearchContext context)
        {
            _context = context;
        }

        public void Insert(Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
        }

        public IEnumerable<Person> Select()
        {
            return _context.Persons.ToList();
        }

    }
}
