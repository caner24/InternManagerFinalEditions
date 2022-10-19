using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Abstract
{
    public interface IStudentManager
    {
        List<Student> GetAll();
        Student GetById(string number);
        Student getPerson(string number, string password);
        void Add(Student entity);
        void Delete(Student entity);
        void Update(Student entity);
    }
}
