using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InternManager.DataAcces.Abstract
{
    public interface IIntern2Dal: IEntityRepository<Intern2>
    {
      List<Intern2> Get(Expression<Func<Intern2, bool>> filter = null);
    }
}
