using DL.Entities;
using System.Data.Entity;

namespace DL
{
    public class Context : DbContext
    {
        internal Context() { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
