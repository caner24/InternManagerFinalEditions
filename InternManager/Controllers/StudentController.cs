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
            _intern2Manager = intern2Manager;
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
                ViewData["Staj1TarihV1"] = intern1Det.RecFileStart2.ToString("d");
                ViewData["Staj1TarihV2"] = intern1Det.RecFileStart2.ToString("d");
            }
            if (intern2Det != null)
            {
                ViewData["Staj2Type"] = intern2Det.Type;
                ViewData["Staj2Tarih"] = intern2Det.RecStart.ToString("d");
                ViewData["Staj2Tarih2"] = intern2Det.RecEnd.ToString("d");
                ViewData["Staj2Tarih2V1"] = intern2Det.RecFileStart2.ToString("d");
                ViewData["Staj2Tarih2V2"] = intern2Det.RecFileStart2.ToString("d");
            }
            if (iseDET != null)
            {
                ViewData["IseType"] = iseDET.Type;
                ViewData["ISE"] = iseDET.RecStart.ToString("d");
                ViewData["ISE2"] = iseDET.RecEnd.ToString("d");
                ViewData["ISE2V1"] = iseDET.RecFileStart2.ToString("d");
                ViewData["ISE2V2"] = iseDET.RecFileStart2.ToString("d");
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

                if (_internManager.GetById("Staj1") != null)
                {
                    var myTimes = _internManager.GetById("Staj1");
                    ViewData["StartDate"] = myTimes.RecStart;
                    ViewData["FinishDate"] = myTimes.RecEnd;
                }
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

                return View(newModel);
            }
            else
            {
                return View();
            }

        }


        [HttpGet]
        public IActionResult ISEPage()
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


        // -------------------------------------------------------- STAJ 1 --------------------------------------------------- // 
        [HttpPost]
        public IActionResult download(string myFileseas)
        {
            var mIntern = _intern1Manager.GetById(myStudents.Id.ToString());
            var getStudent = _studentManager.Get(myStudents.Id.ToString());
            if (mIntern.DetailDocument != null)
            {
                byte[] byteArr = mIntern.DetailDocument;
                string namePipe = "application/pdf";
                return new FileContentResult(byteArr, namePipe)
                {
                    FileDownloadName = $"{myStudents.StudentNumber}_StajBaşvuruFormu(Staj1).pdf"
                };
            }
            return RedirectToAction("Intern1Main", "Boss", myStudents);
        }

        [HttpPost]
        public IActionResult downloadDefter(string myFileseas)
        {
            var mIntern = _intern1Manager.GetById(myStudents.Id.ToString());
            var getStudent = _studentManager.Get(myStudents.Id.ToString());
            if (mIntern.DetailDocument2 != null)
            {
                byte[] byteArr = mIntern.DetailDocument2;
                string namePipe = "application/pdf";
                return new FileContentResult(byteArr, namePipe)
                {
                    FileDownloadName = $"{myStudents.StudentNumber}_StajDefteri(Staj1).pdf"
                };
            }
            return RedirectToAction("Intern1Main", "Boss", myStudents);
        }

        [HttpPost]
        public IActionResult upload(IFormFile file)
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
                else
                {
                    _intern1Manager.Add(intern1);
                }
            }
            return RedirectToAction("Intern1Main", "Student", myStudents);
        }

        [HttpPost]
        public IActionResult uploadDefter(IFormFile file)
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
                        intern1.DetailDocument2 = target.ToArray();
                    }
                    _intern1Manager.Update(intern1);
                }
            }
            return RedirectToAction("Intern1Main", "Student", myStudents);
        }

        // -------------------------------------------------------- STAJ 1 --------------------------------------------------- // 



        // -------------------------------------------------------- STAJ 2 --------------------------------------------------- // 

        [HttpPost]
        public IActionResult downloadStaj2(string myFileseas)
        {
            var mIntern = _intern2Manager.GetById(myStudents.Id.ToString());
            var getStudent = _studentManager.Get(myStudents.Id.ToString());
            if (mIntern.DetailDocument != null)
            {
                byte[] byteArr = mIntern.DetailDocument;
                string namePipe = "application/pdf";
                return new FileContentResult(byteArr, namePipe)
                {
                    FileDownloadName = $"{myStudents.StudentNumber}_StajBaşvuruFormu(Staj2).pdf"
                };
            }
            return RedirectToAction("Intern2Main", "Boss", myStudents);
        }

        [HttpPost]
        public IActionResult downloadDefterStaj2(string myFileseas)
        {
            var mIntern = _intern2Manager.GetById(myStudents.Id.ToString());
            var getStudent = _studentManager.Get(myStudents.Id.ToString());
            if (mIntern.DetailDocument2 != null)
            {
                byte[] byteArr = mIntern.DetailDocument2;
                string namePipe = "application/pdf";
                return new FileContentResult(byteArr, namePipe)
                {
                    FileDownloadName = $"{myStudents.StudentNumber}_StajDefteri(Staj2).pdf"
                };
            }
            return RedirectToAction("Intern2Main", "Boss", myStudents);
        }

        [HttpPost]
        public IActionResult uploadStaj2(IFormFile file)
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
            else
            {
                _intern2Manager.Add(intern1);
            }
            return RedirectToAction("Intern2Main", "Student", myStudents);
        }


        [HttpPost]
        public IActionResult uploadDefterStaj2(IFormFile file)
        {

            var mIntern = _internManager.GetById("Staj2");
            var intern1 = _intern2Manager.GetById(myStudents.Id.ToString());
            if (file != null)
            {
                using (var target = new MemoryStream())
                {
                    file.CopyTo(target);
                    intern1.DetailDocument2 = target.ToArray();
                }
                _intern2Manager.Update(intern1);
            }
            return RedirectToAction("Intern1Main", "Student", myStudents);
        }


        // -------------------------------------------------------- STAJ 2 --------------------------------------------------- // 

        // -------------------------------------------------------- IME --------------------------------------------------- // 

        [HttpPost]
        public IActionResult downloaStajIME(string myFileseas)
        {
            var mIntern = _iseManager.GetById(myStudents.Id.ToString());
            var getStudent = _studentManager.Get(myStudents.Id.ToString());
            if (mIntern.DetailDocument != null)
            {
                byte[] byteArr = mIntern.DetailDocument;
                string namePipe = "application/pdf";
                return new FileContentResult(byteArr, namePipe)
                {
                    FileDownloadName = $"{myStudents.StudentNumber}_StajBaşvuruFormu(IME).pdf"
                };
            }
            return RedirectToAction("ISEMAIN", "Boss", myStudents);
        }

        [HttpPost]
        public IActionResult downloadDefterIME(string myFileseas)
        {
            var mIntern = _iseManager.GetById(myStudents.Id.ToString());
            var getStudent = _studentManager.Get(myStudents.Id.ToString());
            if (mIntern.DetailDocument2 != null)
            {
                byte[] byteArr = mIntern.DetailDocument2;
                string namePipe = "application/pdf";
                return new FileContentResult(byteArr, namePipe)
                {
                    FileDownloadName = $"{myStudents.StudentNumber}_StajDefteri(IME).pdf"
                };
            }
            return RedirectToAction("ISEMAIN", "Boss", myStudents);
        }

        [HttpPost]
        public IActionResult uploadIME(IFormFile file)
        {

            var mIntern = _internManager.GetById("IME");
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
            else
            {
                _iseManager.Add(intern1);
            }
            return RedirectToAction("ISEMAIN", "Student", myStudents);
        }


        [HttpPost]
        public IActionResult uploadDefterIME(IFormFile file)
        {


            var mIntern = _internManager.GetById("IME");
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
            return RedirectToAction("ISEMAIN", "Student", myStudents);
        }


        // -------------------------------------------------------- IME --------------------------------------------------- // 
        [HttpGet]
        public IActionResult Intern1Main()
        {
            var datesHere = _internManager.GetById("Staj1");
            var student = _studentManager.GetById(myId);
            var mInterns1 = _intern1Manager.GetById(myStudents.Id.ToString());
            var person = _personManager.Get(student.PersonId.ToString());


            string myPath = BossModel.ByteArrayToImageAsync(person.Image);
            ViewData["path"] = myPath;
            ViewData["Name"] = person.NameSurname;
            ViewData["Id"] = student.StudentNumber;


            if (mInterns1 != null)
            {

                DateTime fileStart = datesHere.RecFileStart;
                DateTime fileEnd = datesHere.RecFileEnd;

                DateTime fileStart2 = datesHere.RecFileStart;
                DateTime fileEnd2 = datesHere.RecFileEnd;

                if (DateTime.Now >= fileStart && DateTime.Now <= fileEnd)
                {
                    ViewBag.tarihOkey = true;
                }
                else
                {
                    ViewBag.tarihOkey = false;
                }

                if (DateTime.Now >= fileStart2 && DateTime.Now <= fileEnd2)
                {
                    ViewBag.tarihOkey2 = true;
                }

                else
                {
                    ViewBag.tarihOkey2 = false;
                }
                if (mInterns1.DetailDocument != null)
                {
                    ViewBag.documents = true;
                    if (mInterns1.Info != null)
                    {
                        ViewData["myError"] = mInterns1.Info;
                    }
                    if (mInterns1.DetailDocument2 != null)
                    {
                        ViewBag.documents2 = true;
                        if (mInterns1.IsOk2 == true)
                        {
                            ViewBag.defterOnay = true;
                            ViewData["DaysOk"] = mInterns1.OkDays;
                        }
                        else
                        {
                            ViewBag.defterOnay = false;
                        }
                    }
                }
                if (mInterns1.IsOk)
                {
                    ViewBag.isOkeyTrue = true;
                }
            }
            else
            {
                return Content("Hoca ataması yapılmamış !. Atama yapılana kadar bekleyin.");
            }
            myId = student.StudentNumber;
            StudentModel model = new StudentModel();
            model.Students = student;
            return View(model);
        }

        [HttpGet]
        public IActionResult Intern2Main()
        {
            var datesHere = _internManager.GetById("Staj2");
            var student = _studentManager.GetById(myId);
            var yapildiMi = _intern1Manager.GetById(myStudents.Id.ToString());
            var person = _personManager.Get(student.PersonId.ToString());

            string myPath = BossModel.ByteArrayToImageAsync(person.Image);

            ViewData["path"] = myPath;
            ViewData["Name"] = person.NameSurname;
            ViewData["Id"] = student.StudentNumber;

            if (yapildiMi != null)
            {
                if (yapildiMi.IsOk == true)
                {

                    if (_intern2Manager.GetById(myStudents.Id.ToString()) != null)
                    {
                        ViewBag.stajTamamlandi = true;
                        var mInterns1 = _intern2Manager.GetById(myStudents.Id.ToString());
                        DateTime fileStart = datesHere.RecFileStart;
                        DateTime fileEnd = datesHere.RecFileEnd;

                        DateTime fileStart2 = datesHere.RecFileStart;
                        DateTime fileEnd2 = datesHere.RecFileEnd;

                        if (DateTime.Now >= fileStart && DateTime.Now <= fileEnd)
                        {
                            ViewBag.tarihOkey = true;
                        }
                        else
                        {
                            ViewBag.tarihOkey = false;
                        }

                        if (DateTime.Now >= fileStart2 && DateTime.Now <= fileEnd2)
                        {
                            ViewBag.tarihOkey2 = true;
                        }

                        else
                        {
                            ViewBag.tarihOkey2 = false;
                        }
                        if (mInterns1.DetailDocument != null)
                        {
                            ViewBag.documents = true;
                            if (mInterns1.Info != null)
                            {
                                ViewData["myError"] = mInterns1.Info;
                            }
                            if (mInterns1.DetailDocument2 != null)
                            {
                                ViewBag.documents2 = true;
                                if (mInterns1.IsOk2 == true)
                                {
                                    ViewBag.defterOnay = true;
                                    ViewData["DaysOk"] = mInterns1.OkDays;
                                }
                                else
                                {
                                    ViewBag.defterOnay = false;
                                }
                            }
                        }
                        if (mInterns1.IsOk)
                        {
                            ViewBag.isOkeyTrue = true;
                        }

                    }
                }
                else
                {
                    return Content("Öğretmen Atamasi Yapilmamiş lütfen mailinizi kontrol edin !.");
                }
            }
            else
            {
                ViewBag.stajTamamlandi = false;
            }

            myId = student.StudentNumber;
            StudentModel model = new StudentModel();
            model.Students = student;
            return View(model);
        }

        [HttpGet]
        public IActionResult ISEMAIN(IFormFile file, string myFile)
        {

            var datesHere = _internManager.GetById("IME");
            var student = _studentManager.GetById(myId);
            var yapildiMi = _intern1Manager.GetById(myStudents.Id.ToString());
            var mInterns1 = _iseManager.GetById(myStudents.Id.ToString());
            var person = _personManager.Get(student.PersonId.ToString());

            string myPath = BossModel.ByteArrayToImageAsync(person.Image);

            ViewData["path"] = myPath;
            ViewData["Name"] = person.NameSurname;
            ViewData["Id"] = student.StudentNumber;

            if (yapildiMi != null)
            {
                if (yapildiMi.OkDays == "30")
                {
                    ViewBag.stajTamamlandi = true;
                    if (mInterns1 != null)
                    {
                        DateTime fileStart = datesHere.RecFileStart;
                        DateTime fileEnd = datesHere.RecFileEnd;

                        DateTime fileStart2 = datesHere.RecFileStart;
                        DateTime fileEnd2 = datesHere.RecFileEnd;

                        if (DateTime.Now >= fileStart && DateTime.Now <= fileEnd)
                        {
                            ViewBag.tarihOkey = true;
                        }
                        else
                        {
                            ViewBag.tarihOkey = false;
                        }

                        if (DateTime.Now >= fileStart2 && DateTime.Now <= fileEnd2)
                        {
                            ViewBag.tarihOkey2 = true;
                        }

                        else
                        {
                            ViewBag.tarihOkey2 = false;
                        }
                        if (mInterns1.DetailDocument != null)
                        {
                            ViewBag.documents = true;
                            if (mInterns1.Info != null)
                            {
                                ViewData["myError"] = mInterns1.Info;
                            }
                            if (mInterns1.DetailDocument2 != null)
                            {
                                ViewBag.documents2 = true;
                                if (mInterns1.IsOk2 == true)
                                {
                                    ViewBag.defterOnay = true;
                                    ViewData["DaysOk"] = mInterns1.OkDays;
                                }
                                else
                                {
                                    ViewBag.defterOnay = false;
                                }
                            }
                        }
                        if (mInterns1.IsOk)
                        {
                            ViewBag.isOkeyTrue = true;
                        }
                    }
                    else
                    {
                        return Content("Hoca ataması yapılmamış !. Atama yapılana kadar bekleyin.");
                    }
                }
                else
                {
                    ViewBag.stajTamamlandi = false;
                }
            }
            else
            {
                ViewBag.stajTamamlandi = false;
            }

            myId = student.StudentNumber;
            StudentModel model = new StudentModel();
            model.Students = student;
            return View(model);
        }


    }

}
