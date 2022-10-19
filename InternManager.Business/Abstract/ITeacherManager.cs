using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Abstract
{
    public interface ITeacherManager
    {
        List<Teacher> GetAll();
        Teacher GetById(string id);
        Teacher GetTeacher(string number, string password);
        void Add(Teacher entity);
        void Delete(Teacher entity);
        void Update(Teacher teacher);
    }
}
