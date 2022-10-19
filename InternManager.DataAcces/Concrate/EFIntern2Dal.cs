using InternManager.DataAcces.Abstract;
using InternManager.Entities.Concrate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace InternManager.DataAcces.Concrate
{
    public class EFIntern2Dal : IIntern2Dal
    {
        private InternContext context;
        public EFIntern2Dal(InternContext _context)
        {
            context = _context;
        }

        public void Add(Intern2 entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Intern2 entity)
        {
            throw new NotImplementedException();
        }

        public List<Intern2> GetAll(Expression<Func<Intern2, bool>> filter = null)
        {
            return filter == null ? context.Set<Intern2>().ToList() : context.Set<Intern2>().Where(filter).ToList();
        }

        public Intern2 GetById(Expression<Func<Intern2, bool>> filter)
        {
            return context.Set<Intern2>().FirstOrDefault(filter);
        }

        public void Update(Intern2 entity)
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
