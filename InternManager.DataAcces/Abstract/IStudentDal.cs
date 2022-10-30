using InternManager.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InternManager.DataAcces.Abstract
{
    public interface IStudentDal : IEntityRepository<Student>
    {
        Student getPerson(string number, string password);
        Student GetByPersonıd(string number);

        Student Get(Expression<Func<Student, bool>> filter);

    }
}
