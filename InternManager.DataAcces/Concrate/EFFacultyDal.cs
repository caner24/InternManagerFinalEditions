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
    public class EFFacultyDal : IFacultyDal
    {
        private InternContext context;

        public EFFacultyDal(InternContext _context)
        {
            context = _context;
        }
        public void Add(Faculty entity)
        {
            var adedEntity = context.Entry(entity);
            adedEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(Faculty entity)
        {
            var adedEntity = context.Entry(entity);
            adedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public List<Faculty> GetAll(Expression<Func<Faculty, bool>> filter = null)
        {
            return filter == null ? context.Set<Faculty>().ToList() : context.Set<Faculty>().Where(filter).ToList();
        }

        public Faculty GetById(Expression<Func<Faculty, bool>> filter)
        {
            return context.Set<Faculty>().FirstOrDefault(filter);
        }

        public void Update(Faculty entity)
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
