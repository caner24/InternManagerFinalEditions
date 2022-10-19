using InternManager.Business.Abstract;
using InternManager.DataAcces.Abstract;
using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Business.Concrate
{
    public class ISEManager : IISEManager
    {
        IISEDal _iseManager;
        public ISEManager(IISEDal iseManager)
        {
            _iseManager = iseManager;
        }

        public void Add(ISE entity)
        {
            _iseManager.Add(entity);
        }

        public void Delete(ISE entity)
        {
             _iseManager.Delete(entity);
        }

        public List<ISE> GetAll()
        {
            return _iseManager.GetAll();
        }

        public ISE GetById(string id)
        {
  
            return _iseManager.GetById(i => i.Student_Id == Convert.ToInt32(id));
        }
        public void Update(ISE entity)
        {
            _iseManager.Update(entity);
        }
    }
}
