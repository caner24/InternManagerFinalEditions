using InternManager.Business.Abstract;
using InternManager.DataAcces.Abstract;
using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InternManager.Business.Concrate
{
    public class BossManager : IBossManager
    {
        IBoosDal _bossManager;
        public BossManager(IBoosDal bossManager)
        {
            _bossManager = bossManager;
        }
        public void Add(Boss entity)
        {
            _bossManager.Add(entity);
        }

        public void Delete(Boss boss)
        {
            _bossManager.Delete(boss);
        }

        public List<Boss> GetAll()
        {
          return  _bossManager.GetAll();
        }

        public Boss GetById(string id)
        {
           return _bossManager.GetById(i=>i.Id==Convert.ToInt32(id));
        }

        public bool IsSuperBoss(int teacherId)
        {
            return _bossManager.IsSuperBoss(teacherId);
        }
        public void Update(Boss entity)
        {
            _bossManager.Update(entity);
        }
    }
}
