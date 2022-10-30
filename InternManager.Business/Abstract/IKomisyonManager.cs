using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Abstract
{
    public interface IKomisyonManager
    {
        List<Komisyon> GetAll();
        Komisyon GetById(string id);

        Komisyon GetTeachId(string id);
        void Add(Komisyon entity);
        void Delete(Komisyon entity);
        void Update(Komisyon entity);
    }
}
