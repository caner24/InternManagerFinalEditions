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


        [Required(ErrorMessage = "Lütfen Medeniyetinizi  Girin")]
        public string Civilization { get; set; }

        [Required(ErrorMessage = "Lütfen Adınızı ve Soyadınızı Girin")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "Lütfen Tc Kimlik No Girin")]
        public string IdentyNumber { get; set; }

        [Required(ErrorMessage = "Lütfen Tel No Girin")]
        public string PhoneNumber { get; set; }

        public byte[] Image { get; set; }

        [Required(ErrorMessage = "Lütfen Cinsiyet Girin E veya K")]
        public char Gender { get; set; }

    }
}
