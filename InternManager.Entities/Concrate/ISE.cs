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

        public byte[] DetailDocument { get; set; }
        public int Student_Id { get; set; }

        [ForeignKey("Student_Id")]
        public Student StudentPk { get; set; }

        [UIHint("Date")]
        public DateTime StartDate { get; set; }

        [UIHint("Date")]
        public DateTime EndDate { get; set; }
        public bool IsOk { get; set; }
        public string Note { get; set; }
        public string Info { get; set; }
        public int TotalDays { get; set; }
        public string OkDays { get; set; }

        public int KurumId { get; set; }

        [ForeignKey("KurumId")]
        public Kurum KurumPk { get; set; }


        [UIHint("Date")]
        public DateTime CreateDate { get; set; }

        public int InternId { get; set; }

        [ForeignKey("InternId")]
        public Intern InternPk { get; set; }
    }
}
