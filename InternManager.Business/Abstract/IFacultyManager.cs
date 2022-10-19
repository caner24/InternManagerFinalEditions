using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Abstract
{
    public interface IFacultyManager
    {
        List<Faculty> GetAll();
        Faculty GetById(string id);
        void Add(Faculty entity);
        void Delete(Faculty entity);
        void Update(Faculty entity);
    }
}
