using InternManager.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InternManager.Entities.Concrate
{
    public class Faculty:IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Lütfen Fakülte Numarasi Seçiniz")]
        public int? FacultyNumber { get; set; }


        [Required(ErrorMessage = "Lütfen Fakülte Adi Giriniz")]

        public string FacultyName { get; set; }
    }
}
