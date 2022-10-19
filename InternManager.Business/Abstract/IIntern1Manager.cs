using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Abstract
{
    public interface IIntern1Manager
    {
        List<Intern1> GetAll();
        Intern1 GetById(string id);
        void Add(Intern1 entity);
        void Delete(Intern1 entity);
        void Update(Intern1 entity);
    }
}
