using BL.Interfaces;
using DL;
using System;
using System.Collections.Generic;
using BL.Dto;
using System.Linq;

namespace BL.Services
{
    public abstract class BaseService
    {
        protected readonly Context _context;

        public BaseService(Context context) { _context = context; }

        protected void Service<TService>(Action<TService> action) where TService : IService, new()
        {
            Transaction.Service(_context, action);
        }

        protected TOut Service<TService, TOut>(Func<TService, TOut> action) where TService : IService, new()
        {
            return Transaction.Service(_context, action);
        }

        protected TOut Query<TOut>(Func<Context, TOut> action)
        {
            return Db.Context(_context, action);
        }

        protected TOut UnitOfWork<TOut>(Func<IUnitOfWork, TOut> action)
        {
            return Db.UnitOfWork(_context, action);
        }

        protected void UnitOfWork(Action<IUnitOfWork> action)
        {
            Db.UnitOfWork(_context, uow => { action.Invoke(uow); return string.Empty; });
        }

        protected IQueryable<IStudent> Students()
        {
            return (from a in _context.Students
                    join b in _context.Persons
                    on a.PersonId equals b.Id
                    join c in _context.ContactInfo
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
            return (from a in _context.Instrcutors
                    join b in _context.Persons
                    on a.PersonId equals b.Id
                    join c in _context.ContactInfo
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
            return (from a in _context.Courses
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
            return (from a in _context.Subjects
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
            return (from a in _context.Classes
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
            return (from a in _context.Rooms
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
            return (from a in _context.Options
                    select new OptionDto
                    {
                        Value = a.Value,
                        Text = a.Text,
                        Group = a.Group,
                        Type = a.Type
                    });
        }
    }

    public interface IService : IDisposable
    {

    }
}
