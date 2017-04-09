using Search.Repository.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search.Repository.Context
{
    public class SearchDataInitializer : DropCreateDatabaseIfModelChanges<SearchContext>
    {
        protected override void Seed(SearchContext context)
        {
            context.Persons.Add(GetPerson("John", "Smith", 32, "123 Oak street", "Hiking", null));
            context.Persons.Add(GetPerson("Brian", "Dexter", 25, "7283 Palm Dr", "Travel", null));
            context.Persons.Add(GetPerson("John", "Ferrow", 41, "833 Plymount ln", "Voluntering", null));
            context.Persons.Add(GetPerson("Joan", "Hadley", 26, "5484 ParkView stret", "Dancing", null));
            context.Persons.Add(GetPerson("Brett", "Schumaker", 38, "9803 Marriot court", "Movies", null));
            context.SaveChanges();
        }

        private static Person GetPerson(string firstName, string lastName, int age, string address, string interests, string picture)
        {
            return new Person { FirstName = firstName, LastName = lastName, Age = age, Address = address, Interests = interests, Picture = picture };
        }

    }
}
