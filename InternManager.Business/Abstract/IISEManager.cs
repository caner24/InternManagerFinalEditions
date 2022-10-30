using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Abstract
{
    public interface IISEManager
    {
        List<ISE> GetAll();
        ISE GetById(string id);
        void Add(ISE entity);
         List<ISE> Get(string id);
        void Delete(ISE entity);
        void Update(ISE entity);
    }
}
