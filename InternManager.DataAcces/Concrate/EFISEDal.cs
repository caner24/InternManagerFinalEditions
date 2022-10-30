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
    public class EFISEDal :  IISEDal
    {
        private InternContext context;

        public EFISEDal(InternContext _context)
        {
            context = _context;
        }
        public void Add(ISE entity)
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(ISE entity)
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public List<ISE> Get(Expression<Func<ISE, bool>> filter = null)
        {
            return context.Set<ISE>().Where(filter).ToList();
        }

        public List<ISE> GetAll(Expression<Func<ISE, bool>> filter = null)
        {
            return filter == null ? context.Set<ISE>().ToList() : context.Set<ISE>().Where(filter).ToList();
        }

        public ISE GetById(Expression<Func<ISE, bool>> filter)
        {
            return context.Set<ISE>().FirstOrDefault(filter);
        }

        public void Update(ISE entity)
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }

    }
}
