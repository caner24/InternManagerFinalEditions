using InternManager.Business.Abstract;
using InternManager.DataAcces.Abstract;
using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Concrate
{
    public class StudentManager : IStudentManager
    {
        IStudentDal _studentManager;
        public StudentManager(IStudentDal studentManager)
        {
            _studentManager = studentManager;
        }

        public void Add(Student entity)
        {
            _studentManager.Add(entity);
        }
        public void Delete(Student entity)
        {
            _studentManager.Delete(entity);
        }

        public Student Get(string number)
        {
           return _studentManager.Get(i=>i.Id==Convert.ToInt32(number));
        }

        public List<Student> GetAll()
        {
            return _studentManager.GetAll();
        }

        public Student GetById(string number)
        {
            return _studentManager.GetById(i => i.StudentNumber == number);
        }

        public Student GetByPersonıd(string number)
        {
            return _studentManager.GetById(i => i.PersonId == Convert.ToInt32(number));
        }

        public Student getPerson(string number, string password)
        {
            return _studentManager.getPerson(number,password);
        }

        public void Update(Student entity)
        {
            _studentManager.Update(entity);
        }
    }
}
