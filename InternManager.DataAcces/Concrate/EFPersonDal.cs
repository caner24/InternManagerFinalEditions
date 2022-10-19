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
    public class EFPersonDal : IPersonDal
    {
        private InternContext context;

        public EFPersonDal(InternContext _context)
        {
            context = _context;
        }
        public void Add(Person entity)
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(Person entity)
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public Person Get(Expression<Func<Person, bool>> filter)
        {
            return context.Set<Person>().SingleOrDefault(filter);
        }

        public List<Person> GetAll(Expression<Func<Person, bool>> filter = null)
        {
            return filter==null ?  context.Set<Person>().ToList():context.Set<Person>().Where(filter).ToList();
        }

        public Person GetById(Expression<Func<Person, bool>> filter)
        {
            return context.Set<Person>().SingleOrDefault(filter);
        }


        public void Update(Person entity)
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
