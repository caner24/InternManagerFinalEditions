using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Abstract
{
    public interface IKurumManager
    {
        List<Kurum> GetAll();
        Kurum GetById(string id);
        void Add(Kurum entity);
        void Delete(Kurum entity);
        void Update(Kurum entity);
    }
}
