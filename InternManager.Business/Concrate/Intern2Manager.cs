using InternManager.Business.Abstract;
using InternManager.DataAcces.Abstract;
using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Concrate
{
    public class Intern2Manager : IIntern2Manager
    {
        IIntern2Dal _intern2Manager;
        public Intern2Manager(IIntern2Dal intern2Manager)
        {
            _intern2Manager = intern2Manager;
        }
        public void Add(Intern2 entity)
        {
            _intern2Manager.Add(entity);    
        }

        public void Delete(Intern2 entity)
        {
            _intern2Manager.Delete(entity);
        }

        public List<Intern2> GetAll()
        {
          return  _intern2Manager.GetAll();
        }

        public Intern2 GetById(string id)
        {
      
            return _intern2Manager.GetById(i => i.Student_Id == Convert.ToInt32(id));
        }

        public void Update(Intern2 entity)
        {
            _intern2Manager.Update(entity);
        }
    }
}
