using InternManager.Business.Abstract;
using InternManager.DataAcces.Abstract;
using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Concrate
{
    public class KurumManager : IKurumManager
    {
        IKurumDal _kurumDal;
        public KurumManager(IKurumDal kurumDal)
        {
            _kurumDal = kurumDal;
        }

        public void Add(Kurum entity)
        {
            _kurumDal.Add(entity);
        }

        public void Delete(Kurum entity)
        {
            _kurumDal.Delete(entity);
        }

        public List<Kurum> GetAll()
        {
            return _kurumDal.GetAll();
        }

        public Kurum GetById(string id)
        {
            return _kurumDal.GetById(i => i.Id ==Convert.ToInt32(id));
        }

        public void Update(Kurum entity)
        {
            _kurumDal.Update(entity);
        }
    }
}
