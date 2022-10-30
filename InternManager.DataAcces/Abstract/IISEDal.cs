using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InternManager.DataAcces.Abstract
{
    public interface IISEDal: IEntityRepository<ISE>
    {
      List<ISE> Get(Expression<Func<ISE, bool>> filter = null);
    }
}
