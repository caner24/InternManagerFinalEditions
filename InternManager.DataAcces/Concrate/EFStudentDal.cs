using InternManager.DataAcces.Abstract;
using InternManager.Entities.Abstract;
using InternManager.Entities.Concrate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InternManager.DataAcces.Concrate
{
    public class EFStudentDal : IStudentDal
    {
        private InternContext context;

        public EFStudentDal(InternContext _context)
        {
            context = _context;
        }
        public void Add(Student entity)
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(Student entity)
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public Student Get(Expression<Func<Student, bool>> filter)
        {
            return context.Set<Student>().SingleOrDefault(filter);
        }

        public List<Student> GetAll(Expression<Func<Student, bool>> filter = null)
        {
            return filter == null ? context.Set<Student>().ToList() : context.Set<Student>().Where(filter).ToList();
        }

        public Student GetById(Expression<Func<Student, bool>> filter)
        {
            return context.Set<Student>().SingleOrDefault(filter);
        }

        public Student GetByPersonıd(string number)
        {
            return context.Set<Student>().SingleOrDefault(i => i.PersonId ==Convert.ToInt32(number));
        }

        public Student getPerson(string number, string password)
        {
            return context.Set<Student>().SingleOrDefault(i=>i.StudentNumber==number && i.StudentPassword==password);
        }

        public void Update(Student entity)
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
