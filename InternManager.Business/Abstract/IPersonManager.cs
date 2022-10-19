using InternManager.Business.Concrate;
using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Abstract
{
    public interface IPersonManager
    {
        List<Person> GetAll();
        Person GetById(string id);

        Person Get(string id);
        void Add(Person entity);
        void Delete(Person entity);
        void Update(Person entity);
    }
}
