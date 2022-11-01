using InternManager.Business.Abstract;
using InternManager.Entities.Concrate;
using InternManager.WebUI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using InternManager.Business.Concrate;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;

namespace InternManager.WebUI.Controllers
{
    public class KomisyonController : Controller
    {

        private IPersonManager _personManager;
        private IStudentManager _studentManager;
        private ITeacherManager _teacherManager;
        private IBossManager _bossManager;
        private IFacultyManager _facultyManager;
        private IHostingEnvironment Environment;
        private IInternManager _internManager;
        private IKomisyonManager _komisyonManager;
        private IIntern1Manager _intern1Manager;
        private IIntern2Manager _intern2Manager;
        private IConfiguration Configuration;
        private IISEManager _iseManager;
        private static string myPath;
        private static string myId;
        private static byte[] imageArray;
        private static string myPath2;
        private static bool _stageInfo;
        private static Teacher myTeacher;
        private static int _studentId;
        private static string _nameSurname;
        private Student selectedStudent = new Student();
        private static BossModel myBoss = new BossModel();
        public static Student DetailStudent = new Student();

        public KomisyonController(IHostingEnvironment _environmentIConfiguration, IConfiguration _configuration, IIntern1Manager intern1Manager, IIntern2Manager intern2Manager, IISEManager iseManager, IKomisyonManager komisyonManager, IInternManager internManager, IFacultyManager facultyManager, IBossManager boss, ITeacherManager teacher, IPersonManager personManager, IStudentManager studentManager)
        {
            _environmentIConfiguration = Environment;
            _configuration = Configuration;
            _intern1Manager = intern1Manager;
            _intern2Manager = intern2Manager;
            _iseManager = iseManager;
            _komisyonManager = komisyonManager;
            _internManager = internManager;
            _facultyManager = facultyManager;
            _teacherManager = teacher;
            _bossManager = boss;
            _personManager = personManager;
            _studentManager = studentManager;
        }

        [HttpGet]
        public IActionResult ListTeachers()
        {
            ViewData["path"] = myPath;
            ViewData["Name"] = myBoss.Persons.NameSurname;

            BossModel model = new BossModel();
            var studentList = _teacherManager.GetAll();
            foreach (var item in studentList)
            {
                var person = _personManager.Get(item.PersonId.ToString());
                if (person.Image != null)
                {
                    ViewData["mPhotos"] = BossModel.ByteArrayToImageAsync(person.Image);
                }
            }
            model.TeacherList = studentList;
            return View(model);
        }



        [HttpGet]
        public IActionResult TeacherDetails(string id)
        {
            var student = _studentManager.GetAll();
            var yetkiliOgretmen = _teacherManager.GetById(id);

            List<Student> myStudent = new List<Student>();
            foreach (var item in student)
            {
                if (_intern1Manager.GetById(item.Id.ToString()) == null)
                {
                    myStudent.Add(item);
                }
            }
            ViewBag.Students = new SelectList(myStudent, "Id", "StudentNumber");
            ViewBag.Categories = new SelectList(_facultyManager.GetAll(), "Id", "FacultyName");
            BossModel model = new BossModel();
            model.Teacher = yetkiliOgretmen;
            model.Persons = _personManager.Get(yetkiliOgretmen.PersonId.ToString());
            ViewData["StudentPath"] = myPath;
            ViewData["Name"] = _nameSurname;
            ViewBag.isSuperBoss = _bossManager.GetById(myTeacher.Id.ToString()).IsSuper == true ? true : false;
            if (model.Persons.Image != null)
            {
                myPath2 = BossModel.ByteArrayToImageAsync(model.Persons.Image);
                ViewData["Path"] = myPath2;
            }
            else
            {
                ViewData["path"] = " ";
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult TeacherDetails(BossModel model)
        {

            selectedStudent = _studentManager.Get(model.Student.Id.ToString());
            if (_intern1Manager.GetById(model.Student.Id.ToString()) != null)
            {
                var myStaj1 = _intern1Manager.GetById(model.Student.Id.ToString());
                myStaj1.TeacherId = model.Teacher.Id;
                _intern1Manager.Update(myStaj1);
            }
            else
            {
                Intern1 myStajSelect = new Intern1();
                myStajSelect.Student_Id = model.Student.Id;
                myStajSelect.TeacherId = model.Teacher.Id;
                myStajSelect.InternId = _internManager.GetById("Staj1").Id;
                _intern1Manager.Add(myStajSelect);
            }
            BossModel.SendMail2(selectedStudent.StudentMail, "Öğretmen Ataması Yapıldı Sunum Yapacağınız Öğretmen : " + _personManager.Get(myTeacher.PersonId.ToString()).NameSurname);
            TempData["Eklendi"] = "Öğrenci-Öğretmen Atandı";
            return RedirectToAction("ListTeachers", "Komisyon", model.Teacher.Id);
        }

        [HttpGet]
        public IActionResult TeacherDetailsIme(string id)
        {
            var student = _studentManager.GetAll();
            var yetkiliOgretmen = _teacherManager.GetById(id);

            List<Student> myStudent = new List<Student>();
            foreach (var item in student)
            {
                if (_iseManager.GetById(item.Id.ToString()) == null)
                {
                    myStudent.Add(item);
                }
            }
            ViewBag.Students = new SelectList(myStudent, "Id", "StudentNumber");
            ViewBag.Categories = new SelectList(_facultyManager.GetAll(), "Id", "FacultyName");
            BossModel model = new BossModel();
            model.Teacher = yetkiliOgretmen;
            model.Persons = _personManager.Get(yetkiliOgretmen.PersonId.ToString());
            ViewData["StudentPath"] = myPath;
            ViewData["Name"] = _nameSurname;
            ViewBag.isSuperBoss = _bossManager.GetById(myTeacher.Id.ToString()).IsSuper == true ? true : false;
            if (model.Persons.Image != null)
            {
                myPath2 = BossModel.ByteArrayToImageAsync(model.Persons.Image);
                ViewData["Path"] = myPath2;
            }
            else
            {
                ViewData["path"] = " ";
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult TeacherDetailsIme(BossModel model)
        {

            selectedStudent = _studentManager.Get(model.Student.Id.ToString());
            if (_iseManager.GetById(model.Student.Id.ToString()) != null)
            {
                var myStaj1 = _iseManager.GetById(model.Student.Id.ToString());
                myStaj1.TeacherId = model.Teacher.Id;
                _iseManager.Update(myStaj1);
            }
            else
            {
                ISE myStajSelect = new ISE();
                myStajSelect.Student_Id = model.Student.Id;
                myStajSelect.TeacherId = model.Teacher.Id;
                myStajSelect.IsOk = false;
                myStajSelect.IsOk2 = false;
                myStajSelect.InternId = _internManager.GetById("ISE").Id;
                _iseManager.Add(myStajSelect);
            }
            BossModel.SendMail2(selectedStudent.StudentMail, "Öğretmen Ataması Yapıldı Sunum Yapacağınız Öğretmen : " + _personManager.Get(myTeacher.PersonId.ToString()).NameSurname);
            TempData["Eklendi"] = "Öğrenci-Öğretmen Atandı";
            return RedirectToAction("ListTeachers", "Komisyon", model.Teacher.Id);
        }


        [HttpGet]
        public IActionResult TeacherDetailsStaj2(string id)
        {
            var student = _studentManager.GetAll();
            var yetkiliOgretmen = _teacherManager.GetById(id);

            List<Student> myStudent = new List<Student>();
            foreach (var item in student)
            {
                if (_intern2Manager.GetById(item.Id.ToString()) == null)
                {
                    myStudent.Add(item);
                }
            }
            ViewBag.Students = new SelectList(myStudent, "Id", "StudentNumber");
            ViewBag.Categories = new SelectList(_facultyManager.GetAll(), "Id", "FacultyName");
            BossModel model = new BossModel();
            model.Teacher = yetkiliOgretmen;
            model.Persons = _personManager.Get(yetkiliOgretmen.PersonId.ToString());
            ViewData["StudentPath"] = myPath;
            ViewData["Name"] = _nameSurname;
            ViewBag.isSuperBoss = _bossManager.GetById(myTeacher.Id.ToString()).IsSuper == true ? true : false;
            if (model.Persons.Image != null)
            {
                myPath2 = BossModel.ByteArrayToImageAsync(model.Persons.Image);
                ViewData["Path"] = myPath2;
            }
            else
            {
                ViewData["path"] = " ";
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult TeacherDetailsStaj2(BossModel model)
        {
            selectedStudent = _studentManager.Get(model.Student.Id.ToString());
            if (_intern2Manager.GetById(model.Student.Id.ToString()) != null)
            {
                var myStaj1 = _intern2Manager.GetById(model.Student.Id.ToString());
                myStaj1.TeacherId = model.Teacher.Id;
                _intern2Manager.Update(myStaj1);
            }
            else
            {
                Intern2 myStajSelect = new Intern2();
                myStajSelect.Student_Id = model.Student.Id;
                myStajSelect.TeacherId = model.Teacher.Id;
                myStajSelect.InternId = _internManager.GetById("Staj2").Id;
                _intern2Manager.Add(myStajSelect);
            }
            BossModel.SendMail2(selectedStudent.StudentMail, "Öğretmen Ataması Yapıldı Sunum Yapacağınız Öğretmen : " + _personManager.Get(myTeacher.PersonId.ToString()).NameSurname);
            TempData["Eklendi"] = "Öğrenci-Öğretmen Atandı";
            return RedirectToAction("ListTeachers", "Komisyon", model.Teacher.Id);
        }

        [HttpGet]
        public IActionResult Index(Teacher teacher)
        {
            myTeacher = teacher;
            BossModel model = new BossModel();
            model.Teacher = myTeacher;
            model.Persons = _personManager.Get(teacher.PersonId.ToString());
            myBoss.Persons = model.Persons;
            myPath = BossModel.ByteArrayToImageAsync(model.Persons.Image);
            _nameSurname = model.Persons.NameSurname;
            ViewData["path"] = myPath;
            ViewData["Name"] = model.Persons.NameSurname;
            ViewData["TeacherCount"] = _teacherManager.GetAll().Count;
            ViewData["StudentCount"] = _studentManager.GetAll().Count;
            ViewData["gDate"] = DateTime.Now.ToString("d");
            var teacherList = _teacherManager.GetAll();
            var person = new Person();
            var personList = new List<Person>();

            foreach (var item in teacherList)
            {
                if (_komisyonManager.GetTeachId(item.Id.ToString()) != null)
                {
                    person = _personManager.Get(item.PersonId.ToString());
                    person.Civilization = BossModel.ByteArrayToImageAsync(person.Image);
                    personList.Add(person);
                }
            }
            model.PersonList = personList;

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

            return View(model);
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
        public IActionResult Staj1Date()
        {
            BossModel myModel = new BossModel();
            ViewData["path"] = myPath;
            ViewData["Name"] = _nameSurname;
            var model = _internManager.GetById("Staj1");
            if (model != null)
            {
                myModel.Interns = model;
                ViewData["tarihDurum"] = model.RecEnd.ToString("d");
                return View(myModel);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Staj1Date(BossModel model)
        {
            var myModel = _internManager.GetById("Staj1");
            if (myModel != null)
            {
                myModel.RecStart = model.Interns.RecStart;
                myModel.RecEnd = model.Interns.RecEnd;
                myModel.RecFileStart = model.Interns.RecFileStart;
                myModel.RecFileEnd = model.Interns.RecFileEnd;
                myModel.RecFileStart2 = model.Interns.RecFileStart2;
                myModel.RecFileEnd2 = model.Interns.RecFileEnd2;

                _internManager.Update(myModel);
                return RedirectToAction("Index", "Komisyon", myTeacher);
            }
            else
            {
                model.Interns.Dönem = "Yaz";
                _internManager.Add(model.Interns);
                return RedirectToAction("Index", "Komisyon", myTeacher);
            }
        }

        [HttpGet]
        public IActionResult Staj2Date()
        {
            BossModel myModel = new BossModel();
            ViewData["path"] = myPath;
            ViewData["Name"] = _nameSurname;
            var model = _internManager.GetById("Staj2");
            if (model != null)
            {
                myModel.Interns = model;
                ViewData["tarihDurum"] = model.RecEnd.ToString("d");
                return View(myModel);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Staj2Date(BossModel model)
        {
            var myModel = _internManager.GetById("Staj2");
            if (myModel != null)
            {
                model.Interns.Dönem = "Yaz";
                myModel.RecStart = model.Interns.RecStart;
                myModel.RecEnd = model.Interns.RecEnd;
                myModel.RecFileStart = model.Interns.RecFileStart;
                myModel.RecFileEnd = model.Interns.RecFileEnd;
                myModel.RecFileStart2 = model.Interns.RecFileStart2;
                myModel.RecFileEnd2 = model.Interns.RecFileEnd2;

                _internManager.Update(myModel);
                return RedirectToAction("Index", "Komisyon", myTeacher);
            }
            else
            {
                model.Interns.Dönem = "Yaz";
                _internManager.Add(model.Interns);
                return RedirectToAction("Index", "Komisyon", myTeacher);
            }
        }

        [HttpGet]
        public IActionResult ListStudents()
        {
            ViewBag.isBoss = _bossManager.IsSuperBoss(myTeacher.Id);
            ViewData["path"] = myPath;
            ViewData["Name"] = myBoss.Persons.NameSurname;

            BossModel model = new BossModel();
            var studentList = _studentManager.GetAll();
            foreach (var item in studentList)
            {
                var person = _personManager.Get(item.PersonId.ToString());
                if (person.Image != null)
                {
                    ViewData["mPhotos"] = BossModel.ByteArrayToImageAsync(person.Image);
                }
            }
            model.StudentList = studentList;
            return View(model);
        }


        [HttpGet]
        public IActionResult ISEDate()
        {
            BossModel myModel = new BossModel();
            ViewData["path"] = myPath;
            ViewData["Name"] = _nameSurname;
            var model = _internManager.GetById("IME");
            if (model != null)
            {
                myModel.Interns = model;
                ViewData["tarihDurum"] = model.RecEnd.ToString("d");
                return View(myModel);
            }
            return View();
        }
        [HttpPost]
        public IActionResult ISEDate(BossModel model)
        {
            var myModel = _internManager.GetById("IME");
            if (myModel != null)
            {
                if (model.IME.Dönem == "Güz")
                {
                    model.Interns.Dönem = "Güz";
                }
                else
                {
                    model.Interns.Dönem = "Bahar";
                }
                myModel.RecStart = model.Interns.RecStart;
                myModel.RecEnd = model.Interns.RecEnd;
                myModel.RecFileStart = model.Interns.RecFileStart;
                myModel.RecFileEnd = model.Interns.RecFileEnd;
                myModel.RecFileStart2 = model.Interns.RecFileStart2;
                myModel.RecFileEnd2 = model.Interns.RecFileEnd2;

                _internManager.Update(myModel);
                return RedirectToAction("Index", "Komisyon", myTeacher);
            }
            else
            {
                model.Interns.Dönem = "Yaz";
                _internManager.Add(model.Interns);
                return RedirectToAction("Index", "Komisyon", myTeacher);
            }
        }

        [HttpGet]
        public IActionResult ListStudent()
        {
            ViewData["path"] = myPath;
            ViewData["Name"] = myBoss.Persons.NameSurname;

            BossModel model = new BossModel();
            var studentList = _studentManager.GetAll();
            foreach (var item in studentList)
            {
                var person = _personManager.Get(item.PersonId.ToString());
                if (person.Image != null)
                {
                    ViewData["mPhotos"] = BossModel.ByteArrayToImageAsync(person.Image);
                }
            }
            model.StudentList = studentList;
            return View(model);
        }

        [HttpGet]
        public IActionResult Staj1Detail(int id)
        {
            DetailStudent = _studentManager.GetById(id.ToString());
            var myIntern = _intern1Manager.GetById(DetailStudent.Id.ToString());
            var intern = _internManager.GetById("Staj1");

            ViewData["path"] = myPath;
            ViewData["Name"] = myBoss.Persons.NameSurname;

            if (myIntern != null)
            {
                ViewBag.Staj1 = myIntern.DetailDocument == null ? false : true;
                DateTime fileStart = intern.RecFileStart;
                DateTime fileEnd = intern.RecFileEnd;

                DateTime fileStart2 = intern.RecFileStart;
                DateTime fileEnd2 = intern.RecFileEnd;

                if (DateTime.Now >= fileStart && DateTime.Now <= fileEnd)
                {
                    ViewBag.tarihOk = true;
                }
                else
                {
                    ViewBag.tarihOk = false;
                }

                if (myIntern.IsOk == true)
                {
                    ViewBag.Durum = true;
                }
                else
                {
                    ViewBag.Durum = false;
                }
                ViewBag.Durum = myIntern.IsOk;
                var student = _studentManager.Get(id.ToString());
                BossModel model = new BossModel();
                model.Student = student;
                model.Staj1 = myIntern;


                return View(model);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Staj2Detail(int id)
        {
            DetailStudent = _studentManager.GetById(id.ToString());
            var myIntern = _intern2Manager.GetById(DetailStudent.Id.ToString());
            var intern = _internManager.GetById("Staj2");

            ViewData["path"] = myPath;
            ViewData["Name"] = myBoss.Persons.NameSurname;

            if (myIntern != null)
            {
                ViewBag.Staj1 = myIntern.DetailDocument == null ? false : true;
                DateTime fileStart = intern.RecFileStart;
                DateTime fileEnd = intern.RecFileEnd;

                DateTime fileStart2 = intern.RecFileStart;
                DateTime fileEnd2 = intern.RecFileEnd;

                if (DateTime.Now >= fileStart && DateTime.Now <= fileEnd)
                {
                    ViewBag.tarihOk = true;
                }
                else
                {
                    ViewBag.tarihOk = false;
                }

                if (myIntern.IsOk == true)
                {
                    ViewBag.Durum = true;
                }
                else
                {
                    ViewBag.Durum = false;
                }
                ViewBag.Durum = myIntern.IsOk;
                var student = _studentManager.Get(id.ToString());
                BossModel model = new BossModel();
                model.Student = student;
                model.Staj2 = myIntern;


                return View(model);
            }

            return View();
        }

        [HttpGet]
        public IActionResult IMEDetail(int id)
        {
            DetailStudent = _studentManager.GetById(id.ToString());
            var myIntern = _iseManager.GetById(DetailStudent.Id.ToString());
            var intern = _internManager.GetById("IME");

            ViewData["path"] = myPath;
            ViewData["Name"] = myBoss.Persons.NameSurname;

            if (myIntern != null)
            {
                ViewBag.Staj1 = myIntern.DetailDocument == null ? false : true;
                DateTime fileStart = intern.RecFileStart;
                DateTime fileEnd = intern.RecFileEnd;

                DateTime fileStart2 = intern.RecFileStart;
                DateTime fileEnd2 = intern.RecFileEnd;

                if (DateTime.Now >= fileStart && DateTime.Now <= fileEnd)
                {
                    ViewBag.tarihOk = true;
                }
                else
                {
                    ViewBag.tarihOk = false;
                }

                if (myIntern.IsOk == true)
                {
                    ViewBag.Durum = true;
                }
                else
                {
                    ViewBag.Durum = false;
                }
                ViewBag.Durum = myIntern.IsOk;
                var student = _studentManager.Get(id.ToString());
                BossModel model = new BossModel();
                model.Student = student;
                model.IME = myIntern;


                return View(model);
            }

            return View();
        }

        [HttpPost]
        public IActionResult download(string myFileseas)
        {
            var mIntern = _intern1Manager.GetById(DetailStudent.Id.ToString());
            var getStudent = DetailStudent;
            if (mIntern.DetailDocument != null)
            {
                byte[] byteArr = mIntern.DetailDocument;
                string namePipe = "application/pdf";
                return new FileContentResult(byteArr, namePipe)
                {
                    FileDownloadName = $"{DetailStudent.StudentNumber}_StajBaşvuruFormu(Staj1).pdf"
                };
            }
            return RedirectToAction("Intern1Main", "Boss", DetailStudent);
        }
        [HttpPost]
        public IActionResult downloadStaj2(string myFileseas)
        {
            var mIntern = _intern2Manager.GetById(DetailStudent.Id.ToString());
            var getStudent = _studentManager.Get(DetailStudent.Id.ToString());
            if (mIntern.DetailDocument != null)
            {
                byte[] byteArr = mIntern.DetailDocument;
                string namePipe = "application/pdf";
                return new FileContentResult(byteArr, namePipe)
                {
                    FileDownloadName = $"{DetailStudent.StudentNumber}_StajBaşvuruFormu(Staj2).pdf"
                };
            }
            return RedirectToAction("Intern1Main", "Boss", DetailStudent);
        }
        [HttpPost]
        public IActionResult downloadIme(string myFileseas)
        {
            var mIntern = _iseManager.GetById(DetailStudent.Id.ToString());
            var getStudent = _studentManager.Get(DetailStudent.Id.ToString());
            if (mIntern.DetailDocument != null)
            {
                byte[] byteArr = mIntern.DetailDocument;
                string namePipe = "application/pdf";
                return new FileContentResult(byteArr, namePipe)
                {
                    FileDownloadName = $"{DetailStudent.StudentNumber}_StajBaşvuruFormu(Staj2).pdf"
                };
            }
            return RedirectToAction("Intern1Main", "Boss", DetailStudent);
        }
        [HttpPost]
        public IActionResult change(BossModel model)
        {
            var mIntern = _intern1Manager.GetById(DetailStudent.Id.ToString());
            var getStudent = _studentManager.Get(DetailStudent.Id.ToString());
            mIntern.IsOk = model.Staj1.IsOk;
            if (model.Staj1.Info != null)
            {
                mIntern.Info = model.Staj1.Info;
                BossModel.SendMail2(getStudent.StudentMail, "Staj Dosyalarinizda Bir Değişiklik Meydana Geldi");
            }
            _intern1Manager.Update(mIntern);
            return RedirectToAction("Index", "Komisyon", myTeacher);
        }
        [HttpPost]
        public IActionResult changeStaj2(BossModel model)
        {
            var mIntern = _intern2Manager.GetById(DetailStudent.Id.ToString());
            var getStudent = _studentManager.Get(DetailStudent.Id.ToString());
            mIntern.IsOk = model.Staj1.IsOk;
            if (model.Staj1.Info != null)
            {
                mIntern.Info = model.Staj1.Info;
                BossModel.SendMail2(getStudent.StudentMail, "Staj Dosyalarinizda Bir Değişiklik Meydana Geldi");
            }
            _intern2Manager.Update(mIntern);
            return RedirectToAction("Index", "Komisyon", myTeacher);
        }
        [HttpPost]
        public IActionResult changeIme(BossModel model)
        {
            var mIntern = _intern2Manager.GetById(DetailStudent.Id.ToString());
            var getStudent = _studentManager.Get(DetailStudent.Id.ToString());
            mIntern.IsOk = model.Staj1.IsOk;
            if (model.Staj1.Info != null)
            {
                mIntern.Info = model.Staj1.Info;
                BossModel.SendMail2(getStudent.StudentMail, "Staj Dosyalarinizda Bir Değişiklik Meydana Geldi");
            }
            _intern2Manager.Update(mIntern);
            return RedirectToAction("Index", "Komisyon", myTeacher);
        }

        [HttpPost]
        public IActionResult imeSec(BossModel model)
        {

            var myIses = _internManager.GetById("IME");
            var mIntern = _iseManager.GetById(DetailStudent.Id.ToString());
            var getStudent = _studentManager.Get(DetailStudent.Id.ToString());
            if (mIntern == null)
            {

                ISE myTempIse = new ISE();
                myTempIse.Student_Id = model.Student.Id;
                myTempIse.TeacherId = model.Teacher.Id;
                myTempIse.IsOk = false;
                myTempIse.IsOk2 = false;
                myTempIse.InternId = _internManager.GetById("ISE").Id;
                _iseManager.Add(myTempIse);

                if (model.IME.Dönem == "Güz")
                {
                    myTempIse.Dönem = "Güz";
                    BossModel.SendMail2(getStudent.StudentMail, "İmeye Seçildiniz !. Döneminiz : " + model.IME.Dönem);
                }
                else
                {
                    myTempIse.Dönem = "Bahar";
                    BossModel.SendMail2(getStudent.StudentMail, "İmeye Seçildiniz !. Döneminiz : " + model.IME.Dönem);
                }
                _iseManager.Update(myTempIse);
            }
            else
            {
                if (model.IME.Dönem == "Güz")
                {
                    mIntern.Dönem = "Güz";
                    BossModel.SendMail2(getStudent.StudentMail, "İmeye Seçildiniz !. Döneminiz : " + model.IME.Dönem);
                }
                else
                {
                    mIntern.Dönem = "Bahar";
                    BossModel.SendMail2(getStudent.StudentMail, "İmeye Seçildiniz !. Döneminiz : " + model.IME.Dönem);
                }
                _iseManager.Update(mIntern);
            }

            return RedirectToAction("Index", "Komisyon", myTeacher);
        }

        [HttpGet]
        public IActionResult StudentDetail(string Id)
        {
            _studentId = Convert.ToInt32(Id);
            TeacherModel model = new TeacherModel();
            var student = _studentManager.GetById(Id);
            var person = _personManager.GetById(student.PersonId.ToString());
            ViewData["path"] = myPath;
            ViewData["Name"] = _nameSurname;
            ViewData["pathV2"] = BossModel.ByteArrayToImageAsync(person.Image);
            ViewData["Name2"] = person.NameSurname;
            StudentModel studentModel = new StudentModel();
            studentModel.Students = student;
            studentModel.Persons = person;
            return View(model);
        }

        [HttpPost]
        public IActionResult StudentDetail(string myFile, string studentId)
        {

            var mIntern = _intern1Manager.GetById(studentId.ToString());
            var myStudents = _studentManager.Get(studentId.ToString());
            if (mIntern.DetailDocument != null)
            {
                ViewData["Staj1"] = "Dolu";
                if (mIntern.IsOk == false)
                {
                    ViewData["Staj1"] = "Onaylanmadi";
                    ; byte[] byteArr = mIntern.DetailDocument;
                    string namePipe = "application/pdf";
                    return new FileContentResult(byteArr, namePipe)
                    {
                        FileDownloadName = $"{myStudents.StudentNumber}_StajDosyası.pdf"
                    };
                }
                else
                {
                    ViewBag["Staj1"] = "Onaylanmadi";
                }

            }
            TeacherModel model = new TeacherModel();
            var student = _studentManager.GetById(studentId);
            var person = _personManager.GetById(student.PersonId.ToString());
            ViewData["path"] = myPath;
            ViewData["Name"] = _nameSurname;
            ViewData["pathV2"] = BossModel.ByteArrayToImageAsync(person.Image);
            ViewData["Name2"] = person.NameSurname;
            StudentModel studentModel = new StudentModel();
            studentModel.Students = student;
            studentModel.Persons = person;
            return RedirectToAction("StudentDetail", "Komisyon", _studentId);
        }
        [HttpPost]
        public IActionResult StudentDetail(string studentId, string onay, string red)
        {
            var mIntern = _intern1Manager.GetById(studentId.ToString());
            var myStudents = _studentManager.Get(studentId.ToString());
            if (onay == "1")
            {
                mIntern.IsOk = true;
                _intern1Manager.Update(mIntern);
            }
            else
            {
                mIntern.IsOk = false;
                _intern1Manager.Update(mIntern);
            }
            TeacherModel model = new TeacherModel();
            var student = _studentManager.GetById(studentId);
            var person = _personManager.GetById(student.PersonId.ToString());
            ViewData["path"] = myPath;
            ViewData["Name"] = _nameSurname;
            ViewData["pathV2"] = BossModel.ByteArrayToImageAsync(person.Image);
            ViewData["Name2"] = person.NameSurname;
            StudentModel studentModel = new StudentModel();
            studentModel.Students = student;
            studentModel.Persons = person;
            return RedirectToAction("StudentDetail", "Komisyon", _studentId);
        }
    }
}
