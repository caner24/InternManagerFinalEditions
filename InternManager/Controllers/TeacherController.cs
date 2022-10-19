using InternManager.Business.Abstract;
using InternManager.Business.Concrate;
using InternManager.Entities.Concrate;
using InternManager.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using ExcelDataReader;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;

namespace InternManager.WebUI.Controllers
{
    public class TeacherController : Controller
    {

        private IPersonManager _personManager;
        private ITeacherManager _teacherManager;
        private IStudentManager _studentManager;
        private IHostingEnvironment Environment;
        private IConfiguration Configuration;
        public static string myId;
        public static string myPath;
        public static Person _person;
        public string[] _studentPass = new string[1];
        public TeacherController(IHostingEnvironment _environment, IConfiguration _configuration, IStudentManager studentManager, IPersonManager personManager, ITeacherManager teacherManager)
        {
            Environment = _environment;
            Configuration = _configuration;
            _studentManager = studentManager;
            _personManager = personManager;
            _teacherManager = teacherManager;
        }

        [HttpGet]
        public IActionResult Index(TeacherModel teacher)
        {
            _person = _personManager.Get(teacher.Teacher.PersonId.ToString());
            myPath = BossModel.ByteArrayToImageAsync(_person.Image);
            ViewData["path"] = myPath;
            ViewData["Name"] = _person.NameSurname;
            return View(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(string number, string password, IFormFile file)
        {
            TeacherModel tModel = new TeacherModel();
            tModel.Teacher = _teacherManager.GetById(number);

            if (file == null || file.Length == 0)
                return Content("resim seçimi yapmadiniz");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\img",
                        file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var image = new Bitmap(path);

            tModel.Persons = _personManager.Get(tModel.Teacher.PersonId.ToString());
            tModel.Persons.Image = BossModel.ImageToByteArray(image);
            _personManager.Update(tModel.Persons);

            myId = tModel.Teacher.TeacherNumber;
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            tModel.Teacher.TeacherPassword = strBuilder.ToString();
            tModel.Teacher.IsFirstPassword = true;

            _teacherManager.Update(tModel.Teacher);

            return RedirectToAction("TeacherLogin", "Home");
        }

        [HttpGet]
        public IActionResult StudentDetail(string Id)
        {
            TeacherModel model = new TeacherModel();
            var student = _studentManager.GetById(Id);
            var person = _personManager.GetById(student.PersonId.ToString());
            ViewData["path"] = myPath;
            ViewData["Name"] = _person.NameSurname;
            ViewData["pathV2"] = BossModel.ByteArrayToImageAsync(person.Image);
            ViewData["Name2"] = person.NameSurname;
            StudentModel studentModel = new StudentModel();
            studentModel.Students = student;
            studentModel.Persons = person;
            return View(model);
        }

      
        [HttpGet]
        public IActionResult ExcellList(IFormCollection student)
        {
            return View(student);
        }

    }
}
