using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InternManager.Business.Abstract
{
    public interface IBossManager
    {
        List<Boss> GetAll();
        Boss GetById(string id);
        void Add(Boss entity);
        bool IsSuperBoss(int teacherId);
        void Delete(Boss entity);
        void Update(Boss entity);
    }
}
