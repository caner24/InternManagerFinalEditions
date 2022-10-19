using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Abstract
{
    public interface IIntern2Manager
    {
        List<Intern2> GetAll();
        Intern2 GetById(string id);
        void Add(Intern2 entity);
        void Delete(Intern2 entity);
        void Update(Intern2 entity);
    }
}
