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
            context.Persons.Add(GetPerson("John", "Smith", 32, "123 Oak street", "Hiking", @"http://b-i.forbesimg.com/bruceupbin/files/2013/03/Chris.jpg"));
            context.Persons.Add(GetPerson("Brian", "Dexter", 25, "7283 Palm Dr", "Travel", @"https://pbs.twimg.com/profile_images/1717956431/BP-headshot-fb-profile-photo_400x400.jpg"));
            context.Persons.Add(GetPerson("John", "Ferrow", 41, "833 Plymount ln", "Voluntering", @"http://i.imgur.com/aIUnF4P.jpg"));
            context.Persons.Add(GetPerson("Joan", "Hadley", 26, "5484 ParkView stret", "Dancing", @"http://www.firstpersonarts.org/wp-content/uploads/2010/08/Soledad-new-headshot-7-073.jpg"));
            context.Persons.Add(GetPerson("Brett", "Schumaker", 38, "9803 Marriot court", "Movies", @"https://cdn.pixabay.com/photo/2014/07/14/07/36/james-stewart-392932_640.jpg"));
            context.SaveChanges();
        }

        private static Person GetPerson(string firstName, string lastName, int age, string address, string interests, string picture)
        {
            return new Person { FirstName = firstName, LastName = lastName, Age = age, Address = address, Interests = interests, Picture = picture };
        }

    }
}
