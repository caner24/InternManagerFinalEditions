using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.DataAcces.Abstract
{
    public interface IStudentDal : IEntityRepository<Student>
    {
        Student getPerson(string number, string password);

    }
}
