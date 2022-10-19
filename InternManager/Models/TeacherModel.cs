using InternManager.Entities.Concrate;
using System.Collections.Generic;

namespace InternManager.WebUI.Models
{
    public class TeacherModel
    {

        public List<Student> StudentList { get; set; }

        public Teacher Teacher { get; set; }

        public Person Persons { get; set; }
    }
}
