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


        [Required(ErrorMessage = "Lütfen Staj Başlangıç Tarihi Seçiniz")]
        [UIHint("Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Lütfen Staj Bitiş Tarihi Seçiniz")]
        [UIHint("Date")]
        public DateTime EndDate { get; set; }

        public int KurumId { get; set; }

        [ForeignKey("KurumId")]
        public Kurum KurumPk { get; set; }


        public bool IsOk { get; set; }

        public string Note { get; set; }
        public string Info { get; set; }

        public int TotalDays { get; set; }

        public string OkDays { get; set; }
        public int InternId { get; set; }

        [ForeignKey("InternId")]
        public Intern InternPk { get; set; }

        public byte[] DetailDocument { get; set; }

        [UIHint("Date")]
        public DateTime CreateDate { get; set; }
    }
}
