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

        [ForeignKey("FacultyId")]
        public Faculty FacultyPk { get; set; }

        public string StudentNumber { get; set; }

        public string StudentPassword { get; set; }
        public bool IsFirstPassword { get; set; }

        public string StudentMail { get; set; }

        public string Adress { get; set; }

        public string City { get; set; }

        public string Town { get; set; }

        public string PostalCode { get; set; }

        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public Person PersonPk { get; set; }

    }
}
