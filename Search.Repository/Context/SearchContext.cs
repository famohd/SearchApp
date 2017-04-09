
using Search.Repository.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search.Repository.Context
{
    public class SearchContext : DbContext
    {
        public virtual DbSet<Person> Persons { get; set; }

        public SearchContext() : base("SearchContext")
        {
        }

    }
}
