using InternManager.Entities.Concrate;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InternManager.WebUI.Models
{
    public class BossModel
    {
        public Person Persons { get; set; }
        public Teacher Teacher { get; set; }

        public Student Student { get; set; }

        public TestModel TestModels { get; set; } 

        public Intern Interns { get; set; }

        public Intern1 Staj1 { get; set; }

        public Intern2 Staj2 { get; set; }


        public ISE IME { get; set; }
        public List<Person> PersonList { get; set; }
        public List<Student> StudentList { get; set; }

        public List<Boss> BossList { get; set; }
        public List<Teacher> TeacherList { get; set; }
        public Faculty Faculty { get; set; }

        private static int randomNumber { get; set; }
        private static Random random = new Random();
        private static string myPassword { get; set; }
        private static string[] myArray = { "A", "B", "C", "Ç", "D", "E", "F", "G", "Ğ", "H", "İ", "I", "J", "K", "L", "M", "N", "O", "Ö", "P", "R", "S", "Ş", "T", "U", "Ü", "V", "Y", "Z" };
       public static string SendMail(string mail)
        {
            myPassword = "";
           MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential("koustajyonetim@gmail.com", "ilknuarpdakicwpg");
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            for (int i = 0; i < 4; i++)
            {
                randomNumber = random.Next(1, 100);

                myPassword += randomNumber.ToString() + myArray[random.Next(0, myArray.Length - 1)];
            }
            smtpClient.EnableSsl = true;
            message.To.Add(mail);
            message.From = new MailAddress("incibeyaz215@gmail.com");
            message.Subject = "Şifreniz H.K";
            message.Body = "Oluşturulan Şifreniz  : " + myPassword.ToString();
            smtpClient.Send(message);
            return myPassword;
        }

        public static string SendMail2(string mail,string content)
        {
            myPassword = "";
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential("koustajyonetim@gmail.com", "ilknuarpdakicwpg");
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            message.To.Add(mail);
            message.From = new MailAddress("incibeyaz215@gmail.com");
            message.Subject = "Şifreniz H.K";
            message.Body = content;
            smtpClient.Send(message);
            return myPassword;
        }
        public static string GetMd5(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public static byte[] ImageToByteArray(System.Drawing.Image image)
        {
            if (image != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
            else
            {
                return null;
            }
        }
        public static string ByteArrayToImageAsync(byte[] image)
        {
            string base64String = Convert.ToBase64String(image, 0, image.Length);

            return "data:image/png;base64," + base64String;
        }
    }
}
