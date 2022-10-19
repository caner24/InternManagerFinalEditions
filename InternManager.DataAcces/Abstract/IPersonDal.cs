using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InternManager.DataAcces.Abstract
{
    public interface IPersonDal:IEntityRepository<Person>
    {
        Person Get(Expression<Func<Person, bool>> filter);
    }
}
