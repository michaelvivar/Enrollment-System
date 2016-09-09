using DL.Entities;
using System.Data.Entity;

namespace DL
{
    public class Context : DbContext
    {
        internal Context() { }

        public DbSet<ClassSchedule> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseSubjectMapping> CourseSubjectMapping { get; set; }
        public DbSet<Instrcutor> Instrcutors { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
