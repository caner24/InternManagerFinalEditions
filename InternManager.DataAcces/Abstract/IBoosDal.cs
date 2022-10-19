using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.DataAcces.Abstract
{
    public interface IBoosDal :IEntityRepository<Boss>
    {
        bool IsSuperBoss(int teacherId);
    }
}
