using DL.Entities;
using System;
using System.Data.Entity;
using Util.Enums;

namespace DL
{
    public class Context : DbContext
    {
        internal Context() : base("name=EnSysContext")
        {
            Configuration.LazyLoadingEnabled = false;
            //Database.SetInitializer(new Initializer());
        }

        public DbSet<ClassSchedule> Classes { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Course> Courses { get; set; }
        //public DbSet<CourseSubjectMapping> CourseSubjectMapping { get; set; }
        public DbSet<Option> Options { get; set; } 
        public DbSet<Instructor> Instrcutors { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Student> Students { get; set; }
        //public DbSet<StudentClassMapping> StudentClassMapping { get; set; }
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
            context.Options.Add(new Option { Type = OptionType.Status, Text = "Active", Value = (int)Status.Active });
            context.Options.Add(new Option { Type = OptionType.Status, Text = "Inactive", Value = (int)Status.Inactive });

            context.Options.Add(new Option { Type = OptionType.Gender, Text = "Male", Value = (int)Gender.Male });
            context.Options.Add(new Option { Type = OptionType.Gender, Text = "Female", Value = (int)Gender.Female });

            context.Options.Add(new Option { Type = OptionType.DayOfWeek, Text = "Monday", Value = (int)DayOfWeek.Monday });
            context.Options.Add(new Option { Type = OptionType.DayOfWeek, Text = "Tuesday", Value = (int)DayOfWeek.Tuesday });
            context.Options.Add(new Option { Type = OptionType.DayOfWeek, Text = "Wednesday", Value = (int)DayOfWeek.Wednesday });
            context.Options.Add(new Option { Type = OptionType.DayOfWeek, Text = "Thursday", Value = (int)DayOfWeek.Thursday });
            context.Options.Add(new Option { Type = OptionType.DayOfWeek, Text = "Friday", Value = (int)DayOfWeek.Friday });
            context.Options.Add(new Option { Type = OptionType.DayOfWeek, Text = "Saturday", Value = (int)DayOfWeek.Saturday });
            context.Options.Add(new Option { Type = OptionType.DayOfWeek, Text = "Sunday", Value = (int)DayOfWeek.Sunday });

            context.Options.Add(new Option { Type = OptionType.YearLevel, Text = "1st Year", Value = (int)YearLevel.First });
            context.Options.Add(new Option { Type = OptionType.YearLevel, Text = "2nd Year", Value = (int)YearLevel.Second });
            context.Options.Add(new Option { Type = OptionType.YearLevel, Text = "3rd Year", Value = (int)YearLevel.Third });
            context.Options.Add(new Option { Type = OptionType.YearLevel, Text = "4th Year", Value = (int)YearLevel.Fourth });

            context.Options.Add(new Option { Type = OptionType.Unit, Text = "1", Value = (int)Unit.One });
            context.Options.Add(new Option { Type = OptionType.Unit, Text = "2", Value = (int)Unit.Two });
            context.Options.Add(new Option { Type = OptionType.Unit, Text = "3", Value = (int)Unit.Three });

            base.Seed(context);
        }
    }
}
