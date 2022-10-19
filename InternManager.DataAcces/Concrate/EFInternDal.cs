using InternManager.DataAcces.Abstract;
using InternManager.Entities.Concrate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InternManager.DataAcces.Concrate
{
    public class EFInternDal : IInternDal
    {

        private InternContext context;
        public EFInternDal(InternContext _context)
        {
            context = _context;
        }
        public void Add(Intern entity)
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(Intern entity)
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public List<Intern> GetAll(Expression<Func<Intern, bool>> filter = null)
        {
            return filter == null ? context.Set<Intern>().ToList() : context.Set<Intern>().Where(filter).ToList();
        }

        public Intern GetById(Expression<Func<Intern, bool>> filter)
        {
            return context.Set<Intern>().SingleOrDefault(filter);
        }

        public void Update(Intern entity)
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
