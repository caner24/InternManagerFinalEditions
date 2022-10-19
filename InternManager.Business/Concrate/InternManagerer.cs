using InternManager.Business.Abstract;
using InternManager.DataAcces.Abstract;
using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Concrate
{
    public class InternManagerer : IInternManager
    {

        IInternDal _internManager;
        public InternManagerer(IInternDal intern1Manager)
        {
            _internManager = intern1Manager;
        }
        public void Add(Intern entity)
        {
            _internManager.Add(entity);
        }

        public void Delete(Intern entity)
        {
            _internManager.Delete(entity);
        }

        public List<Intern> GetAll()
        {
            return _internManager.GetAll();
        }

        public Intern GetById(string id)
        {
          return  _internManager.GetById(i=>i.Type==id);
        }

        public void Update(Intern entity)
        {
            _internManager.Update(entity);
        }
    }
}
