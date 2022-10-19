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
    public class EFKurumDal:IKurumDal
    {
        private InternContext context;

        public EFKurumDal(InternContext _context)
        {
            context = _context;
        }

        public void Add(Kurum entity)
        {
            var adedEntity = context.Entry(entity);
            adedEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(Kurum entity)
        {
            var adedEntity = context.Entry(entity);
            adedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public List<Kurum> GetAll(Expression<Func<Kurum, bool>> filter = null)
        {
            return filter == null ? context.Set<Kurum>().ToList() : context.Set<Kurum>().Where(filter).ToList();
        }

        public Kurum GetById(Expression<Func<Kurum, bool>> filter)
        {
            return context.Set<Kurum>().FirstOrDefault(filter);
        }

        public bool IsSuperBoss(int teacherId)
        {
            return context.Set<Boss>().FirstOrDefault(i => i.TeacherId == teacherId) == null ? false : true;
        }

        public void Update(Kurum entity)
        {
            var adedEntity = context.Entry(entity);
            adedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
