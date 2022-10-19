using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Abstract
{
    public interface IInternManager
    {
        List<Intern> GetAll();
        Intern GetById(string id);
        void Add(Intern entity);
        void Delete(Intern entity);
        void Update(Intern entity);
    }
}
