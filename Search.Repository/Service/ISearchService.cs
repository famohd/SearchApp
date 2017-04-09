using Search.Repository.Model;
using System.Collections.Generic;

namespace Search.Repository.Service
{
    public interface ISearchService
    {
        void AddPerson(Person person);
        IEnumerable<Person> Get();
        IEnumerable<Person> SearchBy(string nameExpr);
    }
}
