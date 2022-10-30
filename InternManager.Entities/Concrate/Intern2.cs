using InternManager.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InternManager.Entities.Concrate
{
    public class Intern2 : IEntity
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Lütfen İlgili Dökümanı Yükleyiniz")]
        public byte[] DetailDocument { get; set; }


        [Required(ErrorMessage = "Lütfen Staj Detay Dosyanızı Giriniz")]
        public byte[] DetailDocument2 { get; set; }

        public int Student_Id { get; set; }

        [ForeignKey("Student_Id")]
        public Student StudentPk { get; set; }



        [Required(ErrorMessage = "Lütfen Onay Durumu Seçin")]
        public bool IsOk { get; set; }

        [Required(ErrorMessage = "Lütfen Onay Durumu Seçin")]
        public bool IsOk2 { get; set; }

        [Required(ErrorMessage = "Lütfen Geçme Durumunu Seçin (1) geçer (0) kalır")]
        public string Note { get; set; }

        [Required(ErrorMessage = "Lütfen Belirtmek istediğiniz durumu yazınız")]
        public string Info { get; set; }

        [Required(ErrorMessage = "Lütfen Tamamlanan Günleri Yaziniz")]
        public string OkDays { get; set; }

        public int InternId { get; set; }

        [ForeignKey("InternId")]
        public Intern InternPk { get; set; }

        [Required(ErrorMessage = "Lütfen Öğretmen Seçimi Yapiniz")]
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
