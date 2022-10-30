using InternManager.Business.Abstract;
using InternManager.DataAcces.Abstract;
using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Concrate
{
    public class Intern1Manager : IIntern1Manager
    {
        IIntern1Dal _intern1Manager;
        public Intern1Manager(IIntern1Dal intern1Manager)
        {
            _intern1Manager = intern1Manager;
        }

        public void Add(Intern1 entity)
        {
            _intern1Manager.Add(entity);
        }
        public void Delete(Intern1 entity)
        {
            _intern1Manager.Delete(entity);
        }

        public List<Intern1> Get(string id)
        {
            return _intern1Manager.Get(i => i.TeacherId == Convert.ToInt32(id));
        }

        public List<Intern1> GetAll()
        {
            return _intern1Manager.GetAll();
        }

        public Intern1 GetById(string id)
        {
            return _intern1Manager.GetById(i => i.Student_Id == Convert.ToInt32(id));
        }

        public void Update(Intern1 entity)
        {
            _intern1Manager.Update(entity);
        }
    }
}
