using BL.Interfaces;
using DL;
using System;
using System.Collections.Generic;
using BL.Dto;
using System.Linq;

namespace BL.Services
{
    public abstract class BaseService : IDisposable
    {
        private Context _context { get; set; }
        private Context Context { get { if (_context == null) { _context = new Context(); } return _context; } }
        public BaseService(Context context) { _context = context; }


        protected void Service<TService>(Action<TService> action) where TService : IService
        {
            Transaction.Service(Context, action);
        }
        protected TOut Service<TService, TOut>(Func<TService, TOut> action) where TService : IService
        {
            return Transaction.Service(Context, action);
        }
        protected TOut Query<TOut>(Func<Context, TOut> action)
        {
            return action.Invoke(Context);
        }
        protected void Repository<TEntity>(Action<IRepository<TEntity>> action) where TEntity : class
        {
            action.Invoke(new Repository<TEntity>(Context));
        }
        protected TOut Repository<TEntity, TOut>(Func<IRepository<TEntity>, TOut> action) where TEntity : class
        {
            return action.Invoke(new Repository<TEntity>(Context));
        }


        protected IQueryable<IStudent> Students()
        {
            return (from a in Context.Students
                    join b in Context.Persons
                    on a.PersonId equals b.Id
                    join c in Context.ContactInfo
                    on b.ContactInfoId equals c.Id
                    select new StudentDto
                    {
                        PersonId = b.Id,
                        FirstName = b.FirstName,
                        LastName = b.LastName,
                        BirthDate = b.BirthDate,
                        Gender = b.Gender,
                        ContactInfoId = c.Id,
                        Email = c.Email,
                        Telephone = c.Telephone,
                        Mobile = c.Mobile,
                        Id = a.Id,
                        CourseId = a.CourseId,
                        Course = a.Course.Code,
                        Level = a.Level,
                        CreatedDate = a.CreatedDate,
                        Status = a.Status
                    });
        }
        protected IQueryable<IInstructor> Instructors()
        {
            return (from a in Context.Instrcutors
                    join b in Context.Persons
                    on a.PersonId equals b.Id
                    join c in Context.ContactInfo
                    on b.ContactInfoId equals c.Id
                    select new InstructorDto
                    {
                        Id = a.Id,
                        PersonId = b.Id,
                        FirstName = b.FirstName,
                        LastName = b.LastName,
                        BirthDate = b.BirthDate,
                        Gender = b.Gender,
                        ContactInfoId = c.Id,
                        Email = c.Email,
                        Telephone = c.Telephone,
                        Mobile = c.Mobile
                    });
        }
        protected IQueryable<ICourse> Courses()
        {
            return (from a in Context.Courses
                    select new CourseDto
                    {
                        Id = a.Id,
                        Code = a.Code,
                        Remarks = a.Remarks,
                        Status = a.Status
                    });
        }
        protected IQueryable<ISubject> Subjects()
        {
            return (from a in Context.Subjects
                    select new SubjectDto
                    {
                        Id = a.Id,
                        Code = a.Code,
                        Level = a.Level,
                        Remarks = a.Remarks,
                        Status = a.Status,
                        Units = a.Units
                    });
        }
        protected IQueryable<IClassSchedule> Classes()
        {
            return (from a in Context.Classes
                    select new ClassScheduleDto
                    {
                        Id = a.Id,
                        RoomId = a.RoomId,
                        Room = a.Room.Number,
                        TimeStart = a.TimeStart,
                        TimeEnd = a.TimeEnd,
                        Capacity = a.Capacity,
                        Day = a.Day,
                        InstructorId = a.InstructorId,
                        InstructorFirstName = a.Instructor.Person.FirstName,
                        InstructorLastName = a.Instructor.Person.LastName,
                        Remarks = a.Remarks,
                        SubjectId = a.SubjectId,
                        Subject = a.Subject.Code
                    });
        }
        protected IQueryable<IRoom> Rooms()
        {
            return (from a in Context.Rooms
                    select new RoomDto
                    {
                        Id = a.Id,
                        Number = a.Number,
                        Capacity = a.Capacity,
                        Remarks = a.Remarks,
                        Status = a.Status
                    });
        }
        protected IQueryable<IOption> Options()
        {
            return (from a in Context.Options
                    select new OptionDto
                    {
                        Value = a.Value,
                        Text = a.Text,
                        Group = a.Group,
                        Type = a.Type
                    });
        }

        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface IService : IDisposable
    {

    }
}
