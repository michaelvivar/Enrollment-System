using DL.Entities;
using System.Data.Entity;

namespace DL
{
    public class Context : DbContext
    {
        internal Context()
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<ClassSchedule> Classes { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseSubjectMapping> CourseSubjectMapping { get; set; }
        public DbSet<Instrcutor> Instrcutors { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentClassMapping> StudentClassMapping { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
