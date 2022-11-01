using InternManager.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InternManager.Entities.Concrate
{
    public class Intern1 : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int Student_Id { get; set; }

        [ForeignKey("Student_Id")]
        public Student StudentPk { get; set; }

        [Required(ErrorMessage = "Lütfen Onay Durumunu Seçiniz")]
        public bool IsOk { get; set; }

        [Required(ErrorMessage = "Lütfen Onay Durumunu Seçiniz")]
        public bool IsOk2 { get; set; }

        [Required(ErrorMessage = "Lütfen Geçip Geçmediğini Yazınız (G) geçti (K) Kaldı")]
        public string Note { get; set; }

        [Required(ErrorMessage = "Lütfen Belirtmek İstediğiniz Mesajı Yazınız")]
        public string Info { get; set; }


        [Required(ErrorMessage = "Lütfen Onaylanan Günleri Giriniz")]
        public string OkDays { get; set; }


        public int InternId { get; set; }

        [ForeignKey("InternId")]
        public Intern InternPk { get; set; }


        [Required(ErrorMessage = "Lütfen Staj Detay Dosyanızı Giriniz")]
        public byte[] DetailDocument { get; set; }


        [Required(ErrorMessage = "Lütfen Staj Detay Dosyanızı Giriniz")]
        public byte[] DetailDocument2 { get; set; }

        [Required(ErrorMessage = "Lütfen İlgili Öğretmeni Seçiniz")]
        public int TeacherId { get; set; }




    }
}
