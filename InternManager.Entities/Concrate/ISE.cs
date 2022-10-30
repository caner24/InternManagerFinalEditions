using InternManager.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InternManager.Entities.Concrate
{
    public class ISE : IEntity
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Lütfen Staj Dosyasini Yükleyiniz")]
        public byte[] DetailDocument { get; set; }


        [Required(ErrorMessage = "Lütfen Staj Detay Dosyanızı Giriniz")]
        public byte[] DetailDocument2 { get; set; }
        public int Student_Id { get; set; }

        [Required(ErrorMessage = "Lütfen Staj Dosyasini Yükleyiniz")]
        [ForeignKey("Student_Id")]
        public Student StudentPk { get; set; }


        [Required(ErrorMessage = "Lütfen Onay Durumunu Giriniz (1) Geçer (0) Kalir")]
        public bool IsOk { get; set; }


        [Required(ErrorMessage = "Lütfen Onay Durumunu Giriniz (1) Geçer (0) Kalir")]
        public bool IsOk2 { get; set; }


        [Required(ErrorMessage = "G Geçer K kalır")]
        public string Note { get; set; }

        [Required(ErrorMessage = "Lütfen Belirtmek istediğiniz şeyleri yazınız")]
        public string Info { get; set; }


        [Required(ErrorMessage = "Lütfen onaylanan günleri yazınız ")]
        public string OkDays { get; set; }
        public int InternId { get; set; }

        [ForeignKey("InternId")]
        public Intern InternPk { get; set; }
        public int TeacherId { get; set; }

        public DateTime RecStart { get; set; }
        [UIHint("Date")]
        public DateTime RecEnd { get; set; }
        [UIHint("Date")]
        public DateTime RecFileStart { get; set; }
        [UIHint("Date")]
        public DateTime RecFileEnd { get; set; }
    }
}
