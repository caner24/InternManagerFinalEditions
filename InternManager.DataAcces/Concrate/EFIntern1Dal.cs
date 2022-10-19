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
    public class EFIntern1Dal :IIntern1Dal
    {
        private InternContext context;

        public EFIntern1Dal(InternContext _context)
        {
            context = _context;
        }

        public void Add(Intern1 entity)
        {
            var adedEntity = context.Entry(entity);
            adedEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(Intern1 entity)
        {
            var adedEntity = context.Entry(entity);
            adedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public List<Intern1> GetAll(Expression<Func<Intern1, bool>> filter = null)
        {
            return filter == null ? context.Set<Intern1>().ToList() : context.Set<Intern1>().Where(filter).ToList();
        }

        public Intern1 GetById(Expression<Func<Intern1, bool>> filter)
        {
            return context.Set<Intern1>().FirstOrDefault(filter);
        }

        public void Update(Intern1 entity)
        {
            var adedEntity = context.Entry(entity);
            adedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
