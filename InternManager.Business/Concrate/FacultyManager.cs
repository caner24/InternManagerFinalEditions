using InternManager.Business.Abstract;
using InternManager.DataAcces.Abstract;
using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InternManager.Business.Concrate
{
    public class FacultyManager : IFacultyManager
    {
        IFacultyDal _facultyDal;
        public FacultyManager(IFacultyDal facultyDal)
        {
            _facultyDal = facultyDal;
        }

        public void Add(Faculty entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Faculty entity)
        {
            throw new NotImplementedException();
        }

        public List<Faculty> GetAll()
        {
           return _facultyDal.GetAll();
        }

        public Faculty GetById(string id)
        {
            return _facultyDal.GetById(i=>i.Id==Convert.ToInt32(id));
        }

        public void Update(Faculty entity)
        {
            _facultyDal.Update(entity);
        }
    }
}
