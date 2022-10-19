using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using InternManager.Entities.Concrate;
using InternManager.WebUI.Models;
using InternManager.Business.Abstract;
using System.Text;
using InternManager.Entities.Abstract;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata;
using static System.Net.WebRequestMethods;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using InternManager.Business.Concrate;

namespace InternManager.WebUI.Controllers
{
    public class StudentController : Controller
    {
        private IPersonManager _personManager;
        private IStudentManager _studentManager;
        private IIntern1Manager _intern1Manager;
        private IIntern2Manager _intern2Manager;
        private IInternManager _internManager;
        private IKurumManager _kurumManager;
        private IFacultyManager _facultyManager;
        private IHostingEnvironment Environment;
        private static string myId;

        public StudentController(IFacultyManager facultyManager, IInternManager internManager, IIntern2Manager intern2Manager, IKurumManager kurumManager, IIntern1Manager intern1Manager, IHostingEnvironment _environment, IPersonManager personManager, IStudentManager studentManager)
        {
            _facultyManager = facultyManager;
            _internManager = internManager;
            _kurumManager = kurumManager;
            _intern1Manager = intern1Manager;
            _personManager = personManager;
            _studentManager = studentManager;
            Environment = _environment;
        }

        private const string GoogleRecaptchaAddress = "https://www.google.com/recaptcha/api/siteverify";

        [HttpGet]
        public IActionResult Index(Student student)
        {
            var person = _personManager.Get(student.PersonId.ToString());
            string myPath = BossModel.ByteArrayToImageAsync(person.Image);
            ViewData["path"] = myPath;
            ViewData["Name"] = person.NameSurname;
            ViewData["Id"] = student.StudentNumber;
            myId = student.StudentNumber;

            var intern1Det = _internManager.GetById("Staj1");
            var intern2Det = _internManager.GetById("Staj2");
            var iseDET = _internManager.GetById("ISE");

            if (intern1Det != null)
            {
                ViewData["Staj1"] = intern1Det.RecEnd.ToString("d");
                ViewData["Staj1V2"] = intern1Det.RecEnd2.ToString("d");
            }
            if (intern2Det != null)
            {
                ViewData["Staj2"] = intern2Det.RecEnd.ToString("d");
                ViewData["Staj2V2"] = intern2Det.RecEnd2.ToString("d");
            }
            if (iseDET != null)
            {
                ViewData["ISE"] = iseDET.RecEnd.ToString("d");
                ViewData["ISEV2"] = iseDET.RecEnd2.ToString("d");
            }

            return View(student);
        }

        [HttpPost]
        public IActionResult Index(string number, string password)
        {
            var model = _studentManager.GetById(number);


            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            model.StudentPassword = strBuilder.ToString();
            model.IsFirstPassword = true;

            _studentManager.Update(model);

            return RedirectToAction("StudentLogin", "Home");
        }

        [HttpGet]
        public IActionResult SuccesPanel(Student students)
        {

            return View(students);
        }

        [HttpGet]
        public IActionResult Intern1Page()
        {
            if (myId != null)
            {
                StudentModel newModel = new StudentModel();

                newModel.Students = _studentManager.GetById(myId);
                ViewData["FacultyNumber"] = newModel.Students.FacultyId;
                newModel.Persons = _personManager.Get(newModel.Students.PersonId.ToString());
                ViewData["IdentyNumber"] = newModel.Persons.IdentyNumber;
                ViewData["NameSurname"] = newModel.Persons.NameSurname;
                ViewData["StudentMail"] = newModel.Students.StudentMail;
                ViewData["PhoneNumber"] = newModel.Persons.PhoneNumber;
                ViewData["OgrNumber"] = newModel.Students.StudentNumber;
                newModel.Faculties = _facultyManager.GetById(newModel.Students.FacultyId.ToString());
                ViewData["Bölüm"] = newModel.Faculties.FacultyName;
                ViewData["Uyruk"] = newModel.Persons.Civilization;
                if (_intern1Manager.GetById(newModel.Students.Id.ToString()) != null)
                {
                    newModel.Intern1s = _intern1Manager.GetById(newModel.Students.Id.ToString());
                    ViewData["IsNullOr"] = "Geldi";
                    newModel.Kurums = _kurumManager.GetById(newModel.Intern1s.KurumId.ToString());
                    return View(newModel);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public IActionResult Intern1Page(StudentModel model)
        {
            _kurumManager.Add(model.Kurums);
            _intern1Manager.Add(model.Intern1s);
            return View();
        }

        [HttpPost]
        public IActionResult Intern2Page(StudentModel model)
        {
            _kurumManager.Add(model.Kurums);
            _intern2Manager.Add(model.Intern2s);
            return View();
        }

        [HttpGet]
        public IActionResult Intern2Page()
        {
            if (myId != null)
            {
                StudentModel newModel = new StudentModel();

                newModel.Students = _studentManager.GetById(myId);
                TempData["MyId"] = newModel.Students.FacultyId;
                newModel.Persons = _personManager.GetById(newModel.Students.PersonId.ToString());
                if (_intern1Manager.GetById(newModel.Students.Id.ToString()) != null)
                {
                    newModel.Intern1s = _intern1Manager.GetById(newModel.Students.Id.ToString());
                    ViewData["IsNullOr"] = "Geldi";
                    newModel.Kurums = _kurumManager.GetById(newModel.Intern2s.KurumId.ToString());
                    return View(newModel);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public IActionResult Intern1FileMain()
        {
            var student = _studentManager.GetById(myId);
            var person = _personManager.Get(student.PersonId.ToString());
            string myPath = BossModel.ByteArrayToImageAsync(person.Image);
            ViewData["path"] = myPath;
            ViewData["Name"] = person.NameSurname;
            myId = student.StudentNumber;

            return View(student);
        }

        [HttpGet]
        public IActionResult Intern1FileMain(int id)
        {
            var student = _studentManager.GetById(myId);
            var person = _personManager.Get(student.PersonId.ToString());
            string myPath = BossModel.ByteArrayToImageAsync(person.Image);
            ViewData["path"] = myPath;
            ViewData["Name"] = person.NameSurname;
            myId = student.StudentNumber;

            return View(student);
        }
        byte[] bytes;
        [HttpPost]
        public async Task<IActionResult> Intern1FileMain(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("resim seçimi yapmadiniz");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\img",
                        file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                bytes = System.IO.File.ReadAllBytes(stream.ToString());
            }
            var intern = _intern1Manager.GetById(myId);
            intern.DetailDocument = bytes;
            var students = _studentManager.GetById(myId);
            return View(students);
        }

        [HttpGet]
        public IActionResult Intern1Main()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Intern1Main(Teacher teacher)
        {

            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(teacher.TeacherPassword));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            if (_teacherManager.GetTeacher(teacher.TeacherNumber, strBuilder.ToString()) != null)
            {
                return RedirectToAction("Index", "Teacher", _teacherManager.GetById(teacher.TeacherNumber));
            }
            else
            {
                TempData["LoginError"] = "Hatali Bir Giriş Yaptiniz !.";
                return View();
            }
        }

    }
}
