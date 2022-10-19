using InternManager.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InternManager.Entities.Concrate
{
    public class Kurum:IEntity
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage ="Lütfen Kurum Adini Giriniz")]
        public string ResmiAd { get; set; }

        [Required(ErrorMessage = "Lütfen Faaliyet Alani Giriniz")]
        public string FaaliyetAlani { get; set; }


        [Required(ErrorMessage = "Lütfen Adres Giriniz")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Lütfen İlçe  Giriniz")]
        public string Town { get; set; }

        [Required(ErrorMessage = "Lütfen İl Giriniz")]
        public string City { get; set; }


        [Required(ErrorMessage = "Lütfen Posta Kodu Giriniz")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Lütfen Staj Sorumlusu Giriniz")]

        public string StajSorumlusu { get; set; }

        [Required(ErrorMessage = "Lütfen Devlet Katkisi Durumu İşaretleyiniz")]
        public bool DevletKatkisi { get; set; }

        [Required(ErrorMessage = "Lütfen Firma Yetkili Ad Soyad Giriniz")]

        public string FirmaYetkiliAdSoyad { get; set; }

        public string TelNo { get; set; }

        public string TelNo2 { get; set; }

        public string Fax { get; set; }

        public string KurumMail { get; set; }

        public bool Unvan { get; set; }
    }
}
