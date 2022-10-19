using InternManager.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InternManager.Entities.Concrate
{
    public class Person : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string IdentyNumber { get; set; }

        public string Civilization { get; set; }
        public string NameSurname { get; set; }

        public string PhoneNumber { get; set; }

        public byte[] Image { get; set; }

        public char Gender { get; set; }

    }
}
