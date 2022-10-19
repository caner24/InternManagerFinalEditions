using InternManager.Business.Abstract;
using InternManager.DataAcces.Abstract;
using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Concrate
{
    public class PersonManager : IPersonManager
    {
        IPersonDal _personDal;
        public PersonManager(IPersonDal personManager)
        {
            _personDal = personManager;
        }

        public void Add(Person entity)
        {
            _personDal.Add(entity);
        }

        public void Delete(Person entity)
        {
            throw new NotImplementedException();
        }

        public Person Get(string id)
        {
            return _personDal.Get(i => i.Id == Convert.ToInt32(id));
        }
        public List<Person> GetAll()
        {
            return _personDal.GetAll();
        }

        public Person GetById(string id)
        {
            return _personDal.GetById(i => i.IdentyNumber == id);
        }

        public void Update(Person entity)
        {
            _personDal.Update(entity);
        }
    }
}
