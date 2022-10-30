using InternManager.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InternManager.Entities.Concrate
{
    public class Student:IEntity
    {
        [Key]
        public int Id { get; set; }

        public int FacultyId { get; set; }

        public Faculty FacultyPk { get; set; }

        [Required(ErrorMessage = "Lütfen Numaranızı Girin")]
        public string StudentNumber { get; set; }

        [Required(ErrorMessage = "Lütfen Şifrenizi Girin")]
        public string StudentPassword { get; set; }

        public bool IsFirstPassword { get; set; }

        [Required(ErrorMessage = "Lütfen Mailinizi Girin")]
        public string StudentMail { get; set; }

        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public Person PersonPk { get; set; }

    }
}
