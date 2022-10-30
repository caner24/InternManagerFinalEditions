using InternManager.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InternManager.Entities.Concrate
{
    public class Intern:IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }

        [Required(ErrorMessage = "Lütfen Staj Başlangıç Tarihi Seçiniz")]
        [UIHint("Date")]
        public DateTime RecStart { get; set; }

        [Required(ErrorMessage = "Lütfen Staj Bitiş Tarihi Seçiniz")]
        [UIHint("Date")]
        public DateTime RecEnd { get; set; }

        [Required(ErrorMessage = "Lütfen Önerilen  Evrak Yükleme Tarihi Seçiniz")]
        [UIHint("Date")]
        public DateTime RecFileStart { get; set; }

        [Required(ErrorMessage = "Lütfen Önerilen Evrak Bitiş Seçiniz")]
        [UIHint("Date")]
        public DateTime RecFileEnd { get; set; }

        [Required(ErrorMessage = "Lütfen Önerilen  Evrak Yükleme Tarihi Seçiniz")]
        [UIHint("Date")]
        public DateTime RecFileStart2 { get; set; }

        [Required(ErrorMessage = "Lütfen Önerilen Evrak Bitiş Seçiniz")]
        [UIHint("Date")]
        public DateTime RecFileEnd2 { get; set; }


        [Required(ErrorMessage = "Lütfen Dönem Seçiniz ")]

        public string Dönem { get; set; }

    }
}
