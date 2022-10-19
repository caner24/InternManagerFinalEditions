using InternManager.DataAcces.Abstract;
using InternManager.Entities.Abstract;
using InternManager.Entities.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace InternManager.DataAcces.Concrate
{
    public class EFBossDal : IBoosDal
    {
        private InternContext context;

        public EFBossDal(InternContext _context)
        {
            context = _context;
        }

        public void Add(Boss entity)
        {
            var adedEntity = context.Entry(entity);
            adedEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(Boss entity)
        {
            var adedEntity = context.Entry(entity);
            adedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public List<Boss> GetAll(Expression<Func<Boss, bool>> filter = null)
        {
            return filter==null ? context.Set<Boss>().ToList():context.Set<Boss>().Where(filter).ToList();
        }

        public Boss GetById(Expression<Func<Boss, bool>> filter)
        {
            return context.Set<Boss>().FirstOrDefault(filter);
        }

        public bool IsSuperBoss(int teacherId)
        {
         return context.Set<Boss>().FirstOrDefault(i => i.TeacherId == teacherId)==null? false : true;
        }

        public void Update(Boss entity)
        {
            var adedEntity = context.Entry(entity);
            adedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
