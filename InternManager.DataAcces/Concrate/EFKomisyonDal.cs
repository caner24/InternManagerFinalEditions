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
    public class EFKomisyonDal : IKomisyonDal
    {

        private InternContext context;

        public EFKomisyonDal(InternContext _context)
        {
            context = _context;
        }

        public void Add(Komisyon entity)
        {
            var adedEntity = context.Entry(entity);
            adedEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(Komisyon entity)
        {
            var adedEntity = context.Entry(entity);
            adedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public List<Komisyon> GetAll(Expression<Func<Komisyon, bool>> filter = null)
        {
            return filter == null ? context.Set<Komisyon>().ToList() : context.Set<Komisyon>().Where(filter).ToList();
        }

        public Komisyon GetById(Expression<Func<Komisyon, bool>> filter)
        {
            return context.Set<Komisyon>().FirstOrDefault(filter);
        }
        public Komisyon GetTeachId(Expression<Func<Komisyon, bool>> filter)
        {
            return context.Set<Komisyon>().FirstOrDefault(filter);
        }

        public void Update(Komisyon entity)
        {
            var adedEntity = context.Entry(entity);
            adedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
