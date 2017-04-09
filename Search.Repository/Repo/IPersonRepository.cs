using Search.Repository.Model;
using System.Collections.Generic;

namespace Search.Repository.Repos
{
    public interface IPersonRepository
    {
        void Insert(Person person);
        IEnumerable<Person> Select();

    }
}
