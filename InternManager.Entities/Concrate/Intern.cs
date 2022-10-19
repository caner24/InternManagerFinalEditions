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
        [UIHint("Date")]
        public DateTime RecStart { get; set; }
        [UIHint("Date")]
        public DateTime RecEnd { get; set; }
        [UIHint("Date")]
        public DateTime RecStart2 { get; set; }
        [UIHint("Date")]
        public DateTime RecEnd2 { get; set; }
    }
}
