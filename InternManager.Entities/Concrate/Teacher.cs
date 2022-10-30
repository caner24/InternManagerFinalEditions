using InternManager.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InternManager.Entities.Concrate
{
    public class Teacher:IEntity
    {
      [Key]
        public int Id { get; set; }

        public int FacultyNumber { get; set; }

        [ForeignKey("FacultyNumber")]
        public Faculty FacultyPk { get; set; }

        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public Person PersonPk { get; set; }

        [Required(ErrorMessage = "Lütfen Numara Girin")]
        public string TeacherNumber { get; set; }
        [Required(ErrorMessage = "Lütfen Şifrenizi Girin")]
        public string TeacherPassword { get; set; }
        public bool IsFirstPassword { get; set; }

        [Required(ErrorMessage = "Lütfen Öğretmen mailini Girin")]
        public string TeacherMail { get; set; }
    }
}
