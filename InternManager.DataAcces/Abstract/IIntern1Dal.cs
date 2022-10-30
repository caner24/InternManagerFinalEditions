using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InternManager.DataAcces.Abstract
{
    public interface IIntern1Dal: IEntityRepository<Intern1>
    {
       List<Intern1> Get(Expression<Func<Intern1, bool>> filter = null);
    }
}
