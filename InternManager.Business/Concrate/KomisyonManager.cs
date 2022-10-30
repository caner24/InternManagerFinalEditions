using InternManager.Business.Abstract;
using InternManager.DataAcces.Abstract;
using InternManager.DataAcces.Concrate;
using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InternManager.Business.Concrate
{
    public class KomisyonManager : IKomisyonManager
    {

        IKomisyonDal _komisyonDal;
        public KomisyonManager(IKomisyonDal komisyonDal)
        {
            _komisyonDal = komisyonDal;
        }

        public void Add(Komisyon entity)
        {
            _komisyonDal.Add(entity);
        }

        public void Delete(Komisyon entity)
        {
            _komisyonDal.Delete(entity);
        }

        public List<Komisyon> GetAll()
        {
           return _komisyonDal.GetAll();
        }

        public Komisyon GetById(string id)
        {
            return _komisyonDal.GetById(i => i.Id == Convert.ToInt32(id));
        }
        public Komisyon GetTeachId(string id)
        {
            return _komisyonDal.GetTeachId(i => i.TeacherId == Convert.ToInt32(id));
        }

        public void Update(Komisyon entity)
        {
            _komisyonDal.Update(entity);
        }
    }
}
