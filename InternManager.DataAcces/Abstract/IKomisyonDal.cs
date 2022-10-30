using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InternManager.DataAcces.Abstract
{
    public interface IKomisyonDal:IEntityRepository<Komisyon>
    {
        Komisyon GetTeachId(Expression<Func<Komisyon, bool>> filter);
    }
}
