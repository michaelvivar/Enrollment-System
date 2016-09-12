using DL.Entities;
using System.Data.Entity;
using Util.Enums;

namespace DL
{
    public class Context : DbContext
    {
        public Context() : base("name=EnSysContext")
        {
            Configuration.LazyLoadingEnabled = false;
            //Database.SetInitializer(new Initializer());
        }

        public DbSet<ClassSchedule> Classes { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseSubjectMapping> CourseSubjectMapping { get; set; }
        public DbSet<Option> Options { get; set; } 
        public DbSet<Instructor> Instrcutors { get; set; }
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

    public class Initializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context context)
        {
            context.Options.Add(new Entities.Option { Type = OptionType.Status, Text = "Active", Value = (int)Status.Active });
            context.Options.Add(new Entities.Option { Type = OptionType.Status, Text = "Inactive", Value = (int)Status.Inactive });

            context.Options.Add(new Entities.Option { Type = OptionType.Gender, Text = "Male", Value = (int)Gender.Male });
            context.Options.Add(new Entities.Option { Type = OptionType.Gender, Text = "Female", Value = (int)Gender.Female });

            context.Options.Add(new Entities.Option { Type = OptionType.YearLevel, Text = "1st Year", Value = (int)YearLevel.First });
            context.Options.Add(new Entities.Option { Type = OptionType.YearLevel, Text = "2nd Year", Value = (int)YearLevel.Second });
            context.Options.Add(new Entities.Option { Type = OptionType.YearLevel, Text = "3rd Year", Value = (int)YearLevel.Third });
            context.Options.Add(new Entities.Option { Type = OptionType.YearLevel, Text = "4th Year", Value = (int)YearLevel.Fourth });

            base.Seed(context);
        }
    }
}
