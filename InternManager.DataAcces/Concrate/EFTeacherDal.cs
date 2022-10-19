using InternManager.DataAcces.Abstract;
using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InternManager.DataAcces.Concrate
{
    public class EFTeacherDal : ITeacherDal
    {
        private InternContext context;

        public EFTeacherDal(InternContext _context)
        {
            context = _context;
        }

        public void Add(Teacher entity)
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(Teacher entity)
        {

            var addedEntity = context.Entry(entity);
            addedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChanges();
        }

        public List<Teacher> GetAll(Expression<Func<Teacher, bool>> filter = null)
        {
            return filter==null ?  context.Set<Teacher>().ToList():context.Set<Teacher>().Where(filter).ToList();
        }

        public Teacher GetById(Expression<Func<Teacher, bool>> filter)
        {
            return context.Set<Teacher>().SingleOrDefault(filter);
        }
        public Teacher getPerson(string number, string password)
        {
            return context.Set<Teacher>().SingleOrDefault(i => i.TeacherNumber == number && i.TeacherPassword == password);
        }
        public void Update(Teacher entity)
        {

            var addedEntity = context.Entry(entity);
            addedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
