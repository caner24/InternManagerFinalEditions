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

namespace InternManager.WebUI.Controllers
{
    public class StudentController : Controller
    {
        private IPersonManager _personManager;
        private IStudentManager _studentManager;
        private IIntern1Manager _intern1Manager;
        private IIntern2Manager _intern2Manager;
        private IInternManager _internManager;
        private IFacultyManager _facultyManager;
        private IHostingEnvironment Environment;
        private IISEManager _iseManager;
        private static string myId;
        private static Student myStudents;
        private static StudentModel sModel = new StudentModel();
        private static string isOkeyDate;
        public StudentController(IISEManager iseManager, IFacultyManager facultyManager, IInternManager internManager, IIntern2Manager intern2Manager, IIntern1Manager intern1Manager, IHostingEnvironment _environment, IPersonManager personManager, IStudentManager studentManager)
        {
            _iseManager = iseManager;
            _facultyManager = facultyManager;
            _internManager = internManager;
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
            if (person.Image != null)
            {
                string myPath = BossModel.ByteArrayToImageAsync(person.Image);
                ViewData["path"] = myPath;
            }

            ViewData["Name"] = person.NameSurname;
            ViewData["Id"] = student.StudentNumber;
            myId = student.StudentNumber;

            var intern1Det = _internManager.GetById("Staj1");
            var intern2Det = _internManager.GetById("Staj2");
            var iseDET = _internManager.GetById("ISE");

            ViewData["gDate"] = DateTime.Now.ToString("d");

            if (intern1Det != null)
            {
                ViewData["Staj1Type"] = intern1Det.Type;
                ViewData["Staj1Tarih"] = intern1Det.RecStart.ToString("d");
                ViewData["Staj1Tarih2"] = intern1Det.RecEnd.ToString("d");
            }
            if (intern2Det != null)
            {
                ViewData["Staj2Type"] = intern2Det.Type;
                ViewData["Staj2Tarih"] = intern2Det.RecStart.ToString("d");
                ViewData["Staj2Tarih2"] = intern2Det.RecEnd.ToString("d");
            }
            if (iseDET != null)
            {
                ViewData["IseType"] = iseDET.Type;
                ViewData["ISE"] = iseDET.RecStart.ToString("d");
                ViewData["ISE2"] = iseDET.RecEnd.ToString("d");
            }

            myStudents = _studentManager.GetById(student.StudentNumber.ToString());
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string number, string password, IFormFile file = null)
        {
            var model = _studentManager.GetById(number);
            var persons = _personManager.Get(model.PersonId.ToString());
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
            if (file != null)
            {
                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot\\img",
                            file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var image = new Bitmap(path);
                persons.Image = BossModel.ImageToByteArray(image);
            }
            _personManager.Update(persons);
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

                newModel.Students = myStudents;
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
            var interns = _internManager.GetById("Staj1");
            if (interns.RecStart < model.Intern1s.RecStart && interns.RecFileEnd < model.Intern1s.RecFileEnd)
            {
                TempData["myError"] = "Staj Başlangıç Bİtiş Tarihlerini Doğru Giriniz";
                return View();
            }
            else
            {
                _intern1Manager.Add(model.Intern1s);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Intern2Page(StudentModel model)
        {
            _intern2Manager.Add(model.Intern2s);
            return View();
        }

        [HttpGet]
        public IActionResult Intern2Page()
        {

            if (true)
            {

            }
            if (myId != null)
            {
                StudentModel newModel = new StudentModel();

                newModel.Students = _studentManager.GetById(myId);
                TempData["MyId"] = newModel.Students.FacultyId;
                newModel.Persons = _personManager.GetById(newModel.Students.PersonId.ToString());
                if (_intern2Manager.GetById(newModel.Students.Id.ToString()) != null)
                {
                    newModel.Intern1s = _intern1Manager.GetById(newModel.Students.Id.ToString());
                    ViewData["IsNullOr"] = "Geldi";
                    if (newModel.Intern1s.IsOk == true)
                    {
                        ViewBag.stajDurum = true;
                    }
                    else
                    {
                        ViewBag.stajDurum = false;
                    }
                    if (newModel.Intern1s.IsOk == true)
                    {
                        ViewBag.Durum = true;
                    }
                    else
                    {
                        ViewBag.Durum = false;
                    }
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


        [HttpGet]
        public IActionResult Intern1FileMain()
        {
            var person = _personManager.Get(myStudents.PersonId.ToString());
            string myPath = BossModel.ByteArrayToImageAsync(person.Image);
            ViewData["path"] = myPath;
            ViewData["Name"] = person.NameSurname;
            ViewData["Id"] = myStudents.StudentNumber;
            ViewBag.documents = _intern1Manager.GetById(myStudents.Id.ToString()).DetailDocument == null ? false : true;
            ViewBag.durum = _intern1Manager.GetById(myStudents.Id.ToString()).IsOk == true ? true : false;
            ViewBag.stajTakip = true;
            myId = myStudents.StudentNumber;
            sModel.Students = myStudents;
            return View(sModel);
        }

        [HttpPost]
        public IActionResult Intern1Main(IFormFile file)
        {
            var mIntern = _internManager.GetById("Staj1");
            if (file != null)
            {
                var intern1 = _intern1Manager.GetById(myStudents.Id.ToString());

                using (var target = new MemoryStream())
                {
                    file.CopyTo(target);
                    intern1.DetailDocument = target.ToArray();

                }
                _intern1Manager.Update(intern1);
            }

            return RedirectToAction("Intern1Main", "Boss", myStudents);
        }
        [HttpPost]
        public IActionResult Intern1Main(string fakeNumber, IFormFile file)
        {
            var mIntern = _internManager.GetById("Staj1");
            if (file != null)
            {
                var intern1 = _intern1Manager.GetById(myStudents.Id.ToString());

                using (var target = new MemoryStream())
                {
                    file.CopyTo(target);
                    intern1.DetailDocument = target.ToArray();

                }
                _intern1Manager.Add(intern1);
            }

            return RedirectToAction("Intern1Main", "Boss", myStudents);
        }

        [HttpPost]
        public IActionResult Intern1Main(string myFile)
        {
            var mIntern = _intern1Manager.GetById(myStudents.Id.ToString());
            var getStudent = _studentManager.Get(myStudents.Id.ToString());
            if (mIntern.DetailDocument != null)
            {
                byte[] byteArr = mIntern.DetailDocument;
                string namePipe = "application/pdf";
                return new FileContentResult(byteArr, namePipe)
                {
                    FileDownloadName = $"{myStudents.StudentNumber}_StajDosyası(Staj1).pdf"
                };
            }
            return RedirectToAction("Intern1Main", "Boss", myStudents);
        }

        [HttpPost]
        public IActionResult Intern1Main(IFormFile file, string myFile)
        {

            if (_internManager.GetById("Staj1").RecStart >= Convert.ToDateTime(DateTime.Now.ToString("d")) && _internManager.GetById("Staj1").RecEnd <= Convert.ToDateTime(DateTime.Now.ToString("d")))
            {
                TempData["mError"] = "Giriş Tarihi Geçti !.";
                return RedirectToAction("Intern1Main", "Boss", sModel);
            }
            else
            {
                var mIntern = _internManager.GetById("Staj1");
                var intern1 = _intern1Manager.GetById(myStudents.Id.ToString());
                if (file != null)
                {
                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        intern1.DetailDocument = target.ToArray();
                    }
                    _intern1Manager.Update(intern1);
                }
            }
            return RedirectToAction("Intern1Main", "Boss", myStudents);
        }

        [HttpGet]
        public IActionResult Intern1Main()
        {

            if (_internManager.GetById("Staj1").RecFileStart <= DateTime.Now && _internManager.GetById("Staj1").RecFileEnd >= DateTime.Now)
            {
                ViewBag.isOk = true;
            }
            else
            {
                ViewBag.isOk = false;
            }

            if (_internManager.GetById("Staj1").RecFileStart2 <= DateTime.Now && _internManager.GetById("Staj1").RecFileEnd2 >= DateTime.Now)
            {
                ViewBag.isOk2 = true;
            }
            else
            {
                ViewBag.isOk2 = false;
            }

            var student = _studentManager.GetById(myId);
            var person = _personManager.Get(student.PersonId.ToString());
            string myPath = BossModel.ByteArrayToImageAsync(person.Image);
            ViewData["path"] = myPath;
            ViewData["Name"] = person.NameSurname;
            ViewData["Id"] = student.StudentNumber;
            var mInterns1 = _intern1Manager.GetById(myStudents.StudentNumber);
            if (mInterns1 != null)
            {
                if (mInterns1.DetailDocument != null)
                {
                    ViewBag.documents = true;
                    if (mInterns1.IsOk == false)
                    {
                        ViewBag.info = mInterns1.Info;
                    }
                    else
                    {

                        ViewBag.Durum2 = true;
                        if (mInterns1.DetailDocument2 != null)
                        {
                            ViewBag.documents2 = true;
                            if (mInterns1.IsOk2 == false)
                            {
                                ViewBag.info = mInterns1.Info;
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }

            myId = student.StudentNumber;
            return View(student);
        }




        [HttpPost]
        public IActionResult Intern2Main(IFormFile file)
        {

            if (_internManager.GetById("Staj2").RecStart > Convert.ToDateTime(DateTime.Now.ToString("d")) && _internManager.GetById("Staj2").RecEnd < Convert.ToDateTime(DateTime.Now.ToString("d")))
            {
                TempData["mError"] = "Giriş Tarihi Geçti !.";
                return RedirectToAction("Intern2Main", "Boss", sModel);
            }
            else
            {
                var mIntern = _internManager.GetById("Staj2");
                if (file != null)
                {
                    var intern1 = new Intern2();
                    intern1.Student_Id = myStudents.Id;
                    intern1.InternId = mIntern.Id;

                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        intern1.DetailDocument = target.ToArray();

                    }
                    _intern2Manager.Add(intern1);
                }
            }
            return RedirectToAction("Intern2Main", "Boss", myStudents);
        }

        [HttpPost]
        public IActionResult Intern2Main(IFormFile file, string myFile)
        {

            if (_internManager.GetById("Staj2").RecStart > Convert.ToDateTime(DateTime.Now.ToString("d")) && _internManager.GetById("Staj2").RecEnd < Convert.ToDateTime(DateTime.Now.ToString("d")))
            {
                TempData["mError"] = "Giriş Tarihi Geçti !.";
                return RedirectToAction("Intern2Main", "Boss", sModel);
            }
            else
            {
                var mIntern = _internManager.GetById("Staj2");
                var intern1 = _intern2Manager.GetById(myStudents.Id.ToString());
                if (file != null)
                {
                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        intern1.DetailDocument = target.ToArray();
                    }
                    _intern2Manager.Update(intern1);
                }
            }
            return RedirectToAction("Intern2Main", "Boss", myStudents);
        }

        [HttpPost]
        public IActionResult Intern2Main(string myFile)
        {
            var mIntern = _intern2Manager.GetById(myStudents.Id.ToString());
            var getStudent = _studentManager.Get(myStudents.Id.ToString());
            if (mIntern.DetailDocument != null)
            {
                byte[] byteArr = mIntern.DetailDocument;
                string namePipe = "application/pdf";
                return new FileContentResult(byteArr, namePipe)
                {
                    FileDownloadName = $"{myStudents.StudentNumber}_StajDosyası(Staj2).pdf"
                };
            }
            return RedirectToAction("Intern2Main", "Boss", myStudents);
        }

        [HttpGet]
        public IActionResult Intern2Main()
        {
            var student = _studentManager.GetById(myId);
            var person = _personManager.Get(student.PersonId.ToString());
            string myPath = BossModel.ByteArrayToImageAsync(person.Image);
            ViewData["path"] = myPath;
            ViewData["Name"] = person.NameSurname;
            ViewData["Id"] = student.StudentNumber;
            var mInterns1 = _intern2Manager.GetById(myStudents.StudentNumber);
            if (mInterns1.DetailDocument != null)
            {
                ViewBag.Durum = true;
                if (mInterns1.IsOk == false)
                {
                    ViewData["kabulRed"] = "Onaylandı";
                    ViewData["stajDurum"] = mInterns1.Info;
                }
                else
                {
                    ViewData["kabulRed"] = "Onaylanmadi";
                    ViewData["stajDurum"] = mInterns1.Info;
                }
            }
            myId = student.StudentNumber;
            return View(student);
        }



        [HttpPost]
        public IActionResult ISEMAIN(IFormFile file)
        {

            if (_internManager.GetById("ISE").RecStart > Convert.ToDateTime(DateTime.Now.ToString("d")) && _internManager.GetById("ISE").RecEnd < Convert.ToDateTime(DateTime.Now.ToString("d")))
            {
                TempData["mError"] = "Giriş Tarihi Geçti !.";
                return RedirectToAction("Intern2Main", "Boss", sModel);
            }
            else
            {
                var mIntern = _internManager.GetById("Staj2");
                if (file != null)
                {
                    var intern1 = new ISE();
                    intern1.Student_Id = myStudents.Id;
                    intern1.InternId = mIntern.Id;

                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        intern1.DetailDocument = target.ToArray();

                    }
                    _iseManager.Add(intern1);
                }
            }

            return RedirectToAction("ISEMAIN", "Boss", myStudents);
        }

        [HttpPost]
        public IActionResult ISEMAIN(IFormFile file, string myFile)
        {

            if (_internManager.GetById("ISE").RecStart > Convert.ToDateTime(DateTime.Now.ToString("d")) && _internManager.GetById("ISE").RecEnd < Convert.ToDateTime(DateTime.Now.ToString("d")))
            {
                TempData["mError"] = "Giriş Tarihi Geçti !.";
                return RedirectToAction("ISEMAIN", "Boss", sModel);
            }
            else
            {
                var mIntern = _internManager.GetById("Staj2");
                var intern1 = _iseManager.GetById(myStudents.Id.ToString());
                if (file != null)
                {
                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        intern1.DetailDocument = target.ToArray();
                    }
                    _iseManager.Update(intern1);
                }
            }
            return RedirectToAction("ISEMAIN", "Boss", myStudents);
        }

        [HttpPost]
        public IActionResult ISEMAIN(string myFile)
        {
            var mIntern = _iseManager.GetById(myStudents.Id.ToString());
            var getStudent = _studentManager.Get(myStudents.Id.ToString());
            if (mIntern.DetailDocument != null)
            {
                byte[] byteArr = mIntern.DetailDocument;
                string namePipe = "application/pdf";
                return new FileContentResult(byteArr, namePipe)
                {
                    FileDownloadName = $"{myStudents.StudentNumber}_StajDosyası(Staj2).pdf"
                };
            }
            return RedirectToAction("ISEMAIN", "Boss", myStudents);
        }

        [HttpGet]
        public IActionResult ISEMAIN()
        {
            var student = _studentManager.GetById(myId);
            var person = _personManager.Get(student.PersonId.ToString());
            string myPath = BossModel.ByteArrayToImageAsync(person.Image);
            ViewData["path"] = myPath;
            ViewData["Name"] = person.NameSurname;
            ViewData["Id"] = student.StudentNumber;
            var mInterns1 = _iseManager.GetById(myStudents.StudentNumber);
            if (mInterns1.DetailDocument != null)
            {
                ViewBag.Durum = true;
                if (mInterns1.IsOk == false)
                {
                    ViewData["kabulRed"] = "Onaylandı";
                    ViewData["stajDurum"] = mInterns1.Info;
                }
                else
                {
                    ViewData["kabulRed"] = "Onaylanmadi";
                    ViewData["stajDurum"] = mInterns1.Info;
                }
            }
            myId = student.StudentNumber;
            return View(student);
        }

    }
}
