using InternManager.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InternManager.Entities.Concrate
{
    public class Boss:IEntity
    {
        [Key]
        public int Id { get; set; }
        public bool IsSuper { get; set; }
        [Required(ErrorMessage ="Lütfen Yönetici Yapilmak İstenilen Kişiyi Seçin")]
        public int TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher TeacherPk { get; set; }

    }
}
