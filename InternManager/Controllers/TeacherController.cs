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
using System.Linq;
using Microsoft.AspNetCore.DataProtection;

namespace InternManager.WebUI.Controllers
{
    public class TeacherController : Controller
    {

        private IPersonManager _personManager;
        private ITeacherManager _teacherManager;
        private IInternManager _internManager;
        private IIntern1Manager _intern1Manager;
        private IIntern2Manager _intern2Manager;
        private IISEManager _iseManager;
        private IStudentManager _studentManager;
        private IHostingEnvironment Environment;
        private IConfiguration Configuration;
        public static string myId;
        public static string myPath;
        public static string myStudentsId;
        public static Person _person;
        public string[] _studentPass = new string[1];
        public static Teacher teacher;
        public static Teacher myTeacher;
        public static List<Person> mPersons;
        public static Student myStudents = new Student();
        public TeacherController(IISEManager iseManager,IIntern2Manager intern2Manager,  IIntern1Manager intern1Manager, IInternManager internManager, IHostingEnvironment _environment, IConfiguration _configuration, IStudentManager studentManager, IPersonManager personManager, ITeacherManager teacherManager)
        {
            _iseManager = iseManager;
            _intern2Manager = intern2Manager;
            _intern1Manager = intern1Manager;
            _internManager = internManager;
            Environment = _environment;
            Configuration = _configuration;
            _studentManager = studentManager;
            _personManager = personManager;
            _teacherManager = teacherManager;
        }


        [HttpGet]
        public IActionResult Index(Teacher teacher)
        {

            var person = _personManager.Get(teacher.PersonId.ToString());
            myTeacher = teacher;
            if (person.Image != null)
            {
                string myPath = BossModel.ByteArrayToImageAsync(person.Image);
                ViewData["path"] = myPath;
            }

            ViewData["Name"] = person.NameSurname;
            ViewData["Id"] = teacher.TeacherNumber;
            myId = teacher.TeacherNumber;

            ViewData["gDate"] = DateTime.Now.ToString("d");

            var intern1Det = _internManager.GetById("Staj1");
            var intern2Det = _internManager.GetById("Staj2");
            var iseDET = _internManager.GetById("ISE");

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

            myTeacher = _teacherManager.GetById(teacher.TeacherNumber.ToString());
            return View(teacher);
        }


        [HttpPost]
        public async Task<IActionResult> Index(string number, string password, IFormFile file = null)
        {
            var model = _teacherManager.GetById(number);
            var persons = _personManager.Get(model.PersonId.ToString());
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            model.TeacherPassword = strBuilder.ToString();
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
            _teacherManager.Update(model);

            return RedirectToAction("TeacherLogin", "Home");
        }

        [HttpGet]
        public IActionResult ListStaj1()
        {
            TeacherModel model = new TeacherModel();
            var internList = _intern1Manager.GetAll();
            model.Teacher = teacher;
            List<Student> tempList = new List<Student>();
            if (internList != null)
            {
                foreach (var item in internList)
                {
                    if (item.TeacherId == myTeacher.Id)
                    {
                        tempList.Add(_studentManager.Get(item.Student_Id.ToString()));
                    }
                }
            }
            model.StudentList = tempList;
            ViewData["path"] = myPath;
            ViewData["Name"] = _person.NameSurname;
            return View(model);
        }

        [HttpGet]
        public IActionResult Staj1Detail(string id)
        {
            myStudentsId = id;
            TeacherModel model = new TeacherModel();
            var internList = _intern1Manager.GetById(id);
            myStudents = _studentManager.GetById(id);
            if (internList.DetailDocument2!=null)
            {
                ViewBag.staj1Yüklendi = true;
                if (internList.IsOk==true)
                {
                    ViewBag.Onaylandi = true;
                }
                else
                {
                    ViewBag.Onaylandi = false;
                }
            }
            ViewData["path"] = myPath;
            ViewData["Name"] = _person.NameSurname;
            return View();

        }

        [HttpGet]
        public IActionResult Staj2Detail(string id)
        {
            myStudentsId = id;
            TeacherModel model = new TeacherModel();
            var internList = _intern2Manager.GetById(id);
            myStudents = _studentManager.GetById(id);
            if (internList.DetailDocument2 != null)
            {
                ViewBag.staj1Yüklendi = true;
                if (internList.IsOk == true)
                {
                    ViewBag.Onaylandi = true;
                }
                else
                {
                    ViewBag.Onaylandi = false;
                }
            }
            ViewData["path"] = myPath;
            ViewData["Name"] = _person.NameSurname;
            return View();

        }
        [HttpGet]
        public IActionResult IMEDetails(string id)
        {
            myStudentsId = id;
            TeacherModel model = new TeacherModel();
            var internList = _iseManager.GetById(id);
            myStudents = _studentManager.GetById(id);
            if (internList.DetailDocument2 != null)
            {
                ViewBag.staj1Yüklendi = true;
                if (internList.IsOk == true)
                {
                    ViewBag.Onaylandi = true;
                }
                else
                {
                    ViewBag.Onaylandi = false;
                }
            }
            ViewData["path"] = myPath;
            ViewData["Name"] = _person.NameSurname;
            return View();

        }

        [HttpPost]
        public IActionResult downloadDefterStaj1(string myFileseas)
        {
            var mIntern = _intern1Manager.GetById(myStudents.Id.ToString());
            var getStudent = _studentManager.Get(myStudents.Id.ToString());
            if (mIntern.DetailDocument != null)
            {
                byte[] byteArr = mIntern.DetailDocument;
                string namePipe = "application/pdf";
                return new FileContentResult(byteArr, namePipe)
                {
                    FileDownloadName = $"{myStudents.StudentNumber}_StajDefteri(Staj1).pdf"
                };
            }
            return RedirectToAction("Intern1Main", "Boss", myStudentsId);
        }
        [HttpPost]
        public IActionResult downloadDefterStaj2(string myFileseas)
        {
            var mIntern = _intern2Manager.GetById(myStudents.Id.ToString());
            var getStudent = _studentManager.Get(myStudents.Id.ToString());
            if (mIntern.DetailDocument != null)
            {
                byte[] byteArr = mIntern.DetailDocument;
                string namePipe = "application/pdf";
                return new FileContentResult(byteArr, namePipe)
                {
                    FileDownloadName = $"{myStudents.StudentNumber}_StajDefteri(Staj2).pdf"
                };
            }
            return RedirectToAction("Intern1Main", "Boss", myStudentsId);
        }
        [HttpPost]
        public IActionResult durumDegis(BossModel model)
        {
            var mIntern = _intern1Manager.GetById(myStudents.Id.ToString());
            var getStudent = _studentManager.Get(myStudents.Id.ToString());

            if (model.Staj1.IsOk2==true)
            {
                mIntern.IsOk2 = true;
            }
            else
            {
                mIntern.IsOk2 = false;
            }
            mIntern.OkDays = model.Staj1.OkDays;
            mIntern.Info = model.Staj1.Info;

            _intern1Manager.Update(mIntern);

            BossModel.SendMail2("Staj defterinizde değişiklik meydana geldi Lütfen Sisteme Giriş Yapip Kontrol Edin", getStudent.StudentMail);

            return RedirectToAction("Index", "Teacher", myStudents);
        }

        [HttpPost]
        public IActionResult durumDegisStaj2(BossModel model)
        {
            var mIntern = _intern2Manager.GetById(myStudents.Id.ToString());
            var getStudent = _studentManager.Get(myStudents.Id.ToString());

            if (model.Staj1.IsOk2 == true)
            {
                mIntern.IsOk2 = true;
            }
            else
            {
                mIntern.IsOk2 = false;
            }
            mIntern.OkDays = model.Staj1.OkDays;
            mIntern.Info = model.Staj1.Info;

            _intern2Manager.Update(mIntern);

            BossModel.SendMail2("Staj defterinizde değişiklik meydana geldi Lütfen Sisteme Giriş Yapip Kontrol Edin", getStudent.StudentMail);

            return RedirectToAction("Index", "Teacher", myStudents);
        }

        [HttpPost]
        public IActionResult durumDegisIme(BossModel model)
        {
            var mIntern = _iseManager.GetById(myStudents.Id.ToString());
            var getStudent = _studentManager.Get(myStudents.Id.ToString());

            if (model.Staj1.IsOk2 == true)
            {
                mIntern.IsOk2 = true;
            }
            else
            {
                mIntern.IsOk2 = false;
            }
            mIntern.OkDays = model.Staj1.OkDays;
            mIntern.Info = model.Staj1.Info;

            _iseManager.Update(mIntern);

            BossModel.SendMail2("Staj defterinizde değişiklik meydana geldi Lütfen Sisteme Giriş Yapip Kontrol Edin", getStudent.StudentMail);

            return RedirectToAction("Index", "Teacher", myStudents);
        }

        [HttpPost]
        public IActionResult downloadDefterIME(BossModel model)
        {
            var mIntern = _iseManager.GetById(myStudents.Id.ToString());
            var getStudent = _studentManager.Get(myStudents.Id.ToString());

            if (model.Staj1.IsOk2 == true)
            {
                mIntern.IsOk2 = true;
            }
            else
            {
                mIntern.IsOk2 = false;
            }
            mIntern.OkDays = model.Staj1.OkDays;
            mIntern.Info = model.Staj1.Info;

            _iseManager.Update(mIntern);

            BossModel.SendMail2("Staj defterinizde değişiklik meydana geldi Lütfen Sisteme Giriş Yapip Kontrol Edin", getStudent.StudentMail);

            return RedirectToAction("IMEDetails", "Teacher", myStudentsId);
        }
    }
}
