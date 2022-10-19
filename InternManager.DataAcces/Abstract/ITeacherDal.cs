using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.DataAcces.Abstract
{
    public interface ITeacherDal:IEntityRepository<Teacher>
    {
        Teacher getPerson(string number, string password);
    }
}
