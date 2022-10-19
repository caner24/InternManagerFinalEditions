using InternManager.Business.Abstract;
using InternManager.DataAcces.Abstract;
using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Concrate
{
    public class TeacherManager : ITeacherManager
    {
        ITeacherDal _teacherManager;
        public TeacherManager(ITeacherDal teacherManager)
        {
            _teacherManager = teacherManager;
        }
        public void Add(Teacher entity)
        {
            _teacherManager.Add(entity);
        }
        public void Delete(Teacher entity)
        {
            _teacherManager.Delete(entity);
        }

        public List<Teacher> GetAll()
        {
            return _teacherManager.GetAll();
        }

        public Teacher GetById(string id)
        {
            return _teacherManager.GetById(i => i.TeacherNumber == id);
        }
        public Teacher GetTeacher(string number, string password)
        {
            return _teacherManager.getPerson(number, password);
        }

        public void Update(Teacher teacher)
        {
            _teacherManager.Update(teacher);
        }
    }
}
