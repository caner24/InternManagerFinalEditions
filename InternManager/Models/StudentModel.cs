using InternManager.Entities.Concrate;
using System.Collections.Generic;

namespace InternManager.WebUI.Models
{
    public class StudentModel
    {
        public Person Persons { get; set; }
        public Student Students { get; set; }
        public Intern1 Intern1s { get; set; }
        public Intern2 Intern2s { get; set; }
        public ISE Ises { get; set; }
        public Faculty Faculties { get; set; }

        public List<Student> StudentsList { get; set; }

    }
}
