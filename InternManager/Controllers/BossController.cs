using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System;
using InternManager.WebUI.Models;
using InternManager.Business.Abstract;
using System.Security.Cryptography;
using System.Net.Mail;
using InternManager.Entities.Concrate;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileSystemGlobbing;

namespace InternManager.WebUI.Controllers
{
    public class BossController : Controller
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
        private static string myPath2;
        private static string myId;
        private static byte[] imageArray;
        private static bool _stageInfo;
        private static Teacher myTeacher;
        private static Teacher detailTeacher = new Teacher();
        private static int _studentId;
        private static string _nameSurname;

        private static BossModel myBoss = new BossModel();
        public BossController(IHostingEnvironment _environmentIConfiguration, IConfiguration _configuration, IIntern1Manager intern1Manager, IIntern2Manager intern2Manager, IISEManager iseManager, IKomisyonManager komisyonManager, IInternManager internManager, IFacultyManager facultyManager, IBossManager boss, ITeacherManager teacher, IPersonManager personManager, IStudentManager studentManager)
        {
            Environment = _environmentIConfiguration;
            Configuration = _configuration;
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
        public IActionResult Index(Teacher teacher)
        {
            ViewBag.isBoss = _bossManager.IsSuperBoss(teacher.Id);
            myTeacher = teacher;
            ViewBag.isBoss = _bossManager.IsSuperBoss(myTeacher.Id);
            BossModel model = new BossModel();
            model.Teacher = myTeacher;
            model.Persons = _personManager.Get(teacher.PersonId.ToString());
            myBoss.Persons = model.Persons;
            if (model.Persons.Image != null)
            {
                myPath = BossModel.ByteArrayToImageAsync(model.Persons.Image);
                ViewData["path"] = myPath;
            }
            _nameSurname = model.Persons.NameSurname;
            ViewData["Name"] = model.Persons.NameSurname;
            ViewData["TeacherCount"] = _teacherManager.GetAll().Count;
            ViewData["StudentCount"] = _studentManager.GetAll().Count;
            ViewData["gDate"] = DateTime.Now.ToString("d");
            var teacherList = _teacherManager.GetAll();
            var person = new Person();
            var personList = new List<Person>();
            foreach (var item in teacherList)
            {
                if (_komisyonManager.GetById(item.Id.ToString()) != null)
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

            return RedirectToAction("BossLogin", "Home");
        }


        [HttpGet]
        public IActionResult CreateStudent()
        {
            ViewBag.isBoss = _bossManager.IsSuperBoss(myTeacher.Id);
            ViewData["path"] = myPath;
            ViewData["Name"] = _nameSurname;
            ViewBag.Categories = new SelectList(_facultyManager.GetAll(), "Id", "FacultyName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent(RegisterModel model, IFormFile file)
        {
            string myPassword = BossModel.SendMail(model.Students.StudentMail);
            model.Students.StudentPassword = myPassword;

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
            model.Persons.Image = BossModel.ImageToByteArray(image);
            _personManager.Add(model.Persons);

            model.Students.FacultyId = model.Fakülte.Id;
            model.Students.PersonId = model.Persons.Id;
            model.Students.StudentPassword = BossModel.GetMd5(model.Students.StudentPassword);
            model.Students.PersonId = model.Persons.Id;

            _studentManager.Add(model.Students);
            return RedirectToAction("Index", "Boss", myTeacher);
        }
        [HttpGet]
        public IActionResult ListTeachers()
        {
            ViewBag.isBoss = _bossManager.IsSuperBoss(myTeacher.Id);
            ViewData["path"] = myPath;
            ViewData["Name"] = myBoss.Persons.NameSurname;

            BossModel model = new BossModel();
            model.TeacherList = new List<Teacher>();
            var studentList = _teacherManager.GetAll();
            foreach (var item in studentList)
            {
                if (_bossManager.GetById(item.Id.ToString()) == null)
                {
                    model.TeacherList.Add(item);
                }
                var person = _personManager.Get(item.PersonId.ToString());
                if (person.Image != null)
                {
                    ViewData["mPhotos"] = BossModel.ByteArrayToImageAsync(person.Image);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ListBoss()
        {
            ViewBag.isBoss = _bossManager.IsSuperBoss(myTeacher.Id);
            ViewData["path"] = myPath;
            ViewData["Name"] = myBoss.Persons.NameSurname;

            BossModel model = new BossModel();
            var studentList = _teacherManager.GetAll();
            var bossList = _bossManager.GetAll();
            List<Teacher> tempList = new List<Teacher>();
            foreach (var item in studentList)
            {
                foreach (var item2 in bossList)
                {
                    if (item2.TeacherId == item.Id)
                    {
                        if (_bossManager.GetById(item2.TeacherId.ToString()).IsSuper == false)
                        {
                            tempList.Add(item);
                        }
                        var person = _personManager.Get(item.PersonId.ToString());
                        if (person.Image != null)
                        {
                            ViewData["mPhotos"] = BossModel.ByteArrayToImageAsync(person.Image);
                        }
                    }
                    else
                    {

                    }
                }
            }
            model.TeacherList = tempList;
            return View(model);
        }

        [HttpGet]
        public IActionResult ListStudent()
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
        public IActionResult PersonDetails(string id)
        {
            var student = _studentManager.GetById(id);
            detailTeacher.Id = Convert.ToInt32(id);
            ViewBag.isBoss = _bossManager.IsSuperBoss(student.Id);
            ViewBag.Categories = new SelectList(_facultyManager.GetAll(), "Id", "FacultyName");
            BossModel model = new BossModel();

            model.Student = student;
            model.Persons = _personManager.Get(student.PersonId.ToString());
            ViewData["StudentPath"] = myPath;
            ViewData["Name"] = _nameSurname;
            ViewBag.isSuperBoss = _bossManager.GetById(myTeacher.Id.ToString()).IsSuper == true ? true : false;
            if (model.Persons.Image != null)
            {
                myPath2 = BossModel.ByteArrayToImageAsync(model.Persons.Image);
                ViewData["path"] = myPath2;
                imageArray = model.Persons.Image;
            }
            else
            {
                ViewData["path"] = " ";
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult TeacherDetails(string id)
        {
            detailTeacher.Id = Convert.ToInt32(id);
            if (_bossManager.GetById(myTeacher.Id.ToString()).IsSuper)
            {
                ViewBag.isSuperBoss = true;
            }
            ViewBag.isKomisyon = _komisyonManager.GetTeachId(id) == null ? false : true;
            ViewBag.isBoss = _bossManager.IsSuperBoss(myTeacher.Id);
            ViewBag.Categories = new SelectList(_facultyManager.GetAll(), "Id", "FacultyName");
            BossModel model = new BossModel();
            var student = _teacherManager.GetById(id);
            model.Teacher = student;
            model.Persons = _personManager.Get(student.PersonId.ToString());
            ViewData["StudentPath"] = myPath;
            ViewData["Name"] = _nameSurname;
            ViewBag.isSuperBoss = _bossManager.GetById(myTeacher.Id.ToString()).IsSuper == true ? true : false;
            if (model.Persons.Image != null)
            {
                myPath2 = BossModel.ByteArrayToImageAsync(model.Persons.Image);
                ViewData["path"] = myPath2;
                imageArray = model.Persons.Image;
            }
            else
            {
                ViewData["path"] = " ";
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult BossDetail(string id)
        {
            detailTeacher.Id = Convert.ToInt32(id);
            ViewBag.isKomisyon = _komisyonManager.GetTeachId(myTeacher.Id.ToString()) == null ? false : true;
            ViewBag.isBoss = _bossManager.IsSuperBoss(myTeacher.Id);
            ViewBag.Categories = new SelectList(_facultyManager.GetAll(), "Id", "FacultyName");
            BossModel model = new BossModel();
            var student = _teacherManager.GetById(id);
            model.Teacher = student;
            model.Persons = _personManager.Get(student.PersonId.ToString());
            ViewData["StudentPath"] = myPath;
            ViewData["Name"] = _nameSurname;
            ViewBag.isSuperBoss = _bossManager.GetById(myTeacher.Id.ToString()).IsSuper == true ? true : false;
            if (model.Persons.Image != null)
            {
                myPath2 = BossModel.ByteArrayToImageAsync(model.Persons.Image);
                ViewData["path"] = myPath2;
                imageArray = model.Persons.Image;
            }
            else
            {
                ViewData["path"] = " ";
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PersonDetails(BossModel model, IFormFile file)
        {
            if (_bossManager.GetById(myTeacher.Id.ToString()).IsSuper)
            {
                ViewBag.isSuperBoss = true;
            }
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
                model.Persons.Image = BossModel.ImageToByteArray(image);
            }
            else
            {
                model.Persons.Image = imageArray;
            }

            if (model.TestModels.ChosenText == "ögrtmn")
            {
                model.Student.PersonId = model.Persons.Id;
                model.Teacher = new Teacher();
                model.Teacher.TeacherNumber = model.Student.StudentNumber;
                model.Teacher.PersonId = model.Student.PersonId;
                model.Teacher.FacultyNumber = model.Faculty.Id;
                model.Teacher.TeacherPassword = model.Student.StudentPassword;
                model.Teacher.IsFirstPassword = model.Student.IsFirstPassword;
                model.Teacher.TeacherMail = model.Student.StudentMail;

                _studentManager.Delete(model.Student);
                _teacherManager.Add(model.Teacher);
                _personManager.Update(model.Persons);
            }
            else if (model.TestModels.ChosenText == "ögr")
            {
                model.Student.PersonId = model.Persons.Id;
                model.Student.FacultyId = model.Faculty.Id;

                _studentManager.Update(model.Student);
                _personManager.Update(model.Persons);
            }
            else if (model.TestModels.ChosenText == "kom")
            {
                model.Student.PersonId = model.Persons.Id;
                model.Teacher = new Teacher();
                model.Teacher.TeacherNumber = model.Student.StudentNumber;
                model.Teacher.PersonId = model.Student.PersonId;
                model.Teacher.FacultyNumber = model.Faculty.Id;
                model.Teacher.TeacherPassword = model.Student.StudentPassword;
                model.Teacher.IsFirstPassword = model.Student.IsFirstPassword;
                model.Teacher.TeacherMail = model.Student.StudentMail;

                _studentManager.Delete(model.Student);
                _teacherManager.Add(model.Teacher);
                _personManager.Update(model.Persons);

                var mTeacher = _teacherManager.GetById(model.Teacher.TeacherNumber);

                Komisyon boss = new Komisyon();
                boss.TeacherId = mTeacher.Id;
                boss.IsSuper = false;
                _komisyonManager.Add(boss);
            }
            else if (model.TestModels.ChosenText == "boss")
            {
                model.Student.PersonId = model.Persons.Id;
                if (_bossManager.GetAll().Count == 3)
                {
                    TempData["myError"] = " Daha fazla yönetici ekleyemezsiniz !.";
                    return RedirectToAction("ListStudent", "Boss");
                }
                model.Teacher = new Teacher();
                model.Teacher.TeacherNumber = model.Student.StudentNumber;
                model.Teacher.PersonId = model.Student.PersonId;
                model.Teacher.FacultyNumber = model.Faculty.Id;
                model.Teacher.TeacherPassword = model.Student.StudentPassword;
                model.Teacher.IsFirstPassword = model.Student.IsFirstPassword;
                model.Teacher.TeacherMail = model.Student.StudentMail;
                _studentManager.Delete(model.Student);
                _teacherManager.Add(model.Teacher);
                _personManager.Update(model.Persons);
                var mTeacher = _teacherManager.GetById(model.Teacher.TeacherNumber);
                Boss boss = new Boss();
                boss.TeacherId = mTeacher.Id;
                _bossManager.Add(boss);
            }
            return RedirectToAction("ListStudent", "Boss");
        }

        [HttpPost]
        public async Task<IActionResult> BossDetail(BossModel model, IFormFile file)
        {
            if (_bossManager.GetById(detailTeacher.Id.ToString()).IsSuper)
            {
                ViewBag.isSuperBoss = true;
            }
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
                model.Persons.Image = BossModel.ImageToByteArray(image);
            }
            else
            {
                model.Persons.Image = imageArray;
            }

            if (model.TestModels.ChosenText == "ögrtmn")
            {
                model.Student.PersonId = model.Persons.Id;
                model.Teacher = new Teacher();
                model.Teacher.TeacherNumber = model.Student.StudentNumber;
                model.Teacher.PersonId = model.Student.PersonId;
                model.Teacher.FacultyNumber = model.Faculty.Id;
                model.Teacher.TeacherPassword = model.Student.StudentPassword;
                model.Teacher.IsFirstPassword = model.Student.IsFirstPassword;
                model.Teacher.TeacherMail = model.Student.StudentMail;

                _studentManager.Delete(model.Student);
                _teacherManager.Add(model.Teacher);
                _personManager.Update(model.Persons);
            }
            else if (model.TestModels.ChosenText == "ögr")
            {
                model.Student = new Student();
                model.Student.PersonId = model.Persons.Id;

                model.Student.StudentNumber = model.Teacher.TeacherNumber;
                model.Student.PersonId = model.Teacher.PersonId;
                model.Student.FacultyId = model.Faculty.Id;
                model.Student.StudentPassword = model.Teacher.TeacherPassword;
                model.Student.IsFirstPassword = model.Teacher.IsFirstPassword;
                model.Student.StudentMail = model.Teacher.TeacherMail;

                _bossManager.Delete(_bossManager.GetById(model.Teacher.Id.ToString()));
                _teacherManager.Delete(model.Teacher);
                _studentManager.Add(model.Student);
                _personManager.Update(model.Persons);
            }
            else if (model.TestModels.ChosenText == "kom")
            {
                model.Teacher.PersonId = model.Persons.Id;
                model.Teacher.FacultyNumber = model.Faculty.Id;
                _bossManager.Delete(_bossManager.GetById(model.Teacher.Id.ToString()));

                Komisyon boss = new Komisyon();
                boss.TeacherId = model.Teacher.Id;
                _komisyonManager.Add(boss);
                _teacherManager.Update(model.Teacher);
                _personManager.Update(model.Persons);

            }
            else if (model.TestModels.ChosenText == "boss")
            {
                model.Teacher.PersonId = model.Persons.Id;

                model.Teacher.FacultyNumber = model.Faculty.Id;
                _teacherManager.Update(model.Teacher);
                _personManager.Update(model.Persons);
            }
            return RedirectToAction("ListStudent", "Boss");
        }

        [HttpPost]
        public async Task<IActionResult> TeacherDetails(BossModel model, IFormFile file)
        {
            bool durum = _komisyonManager.GetTeachId(detailTeacher.Id.ToString()) == null ? true : false;
            if (_bossManager.GetById(myTeacher.Id.ToString()).IsSuper)
            {
                ViewBag.isSuperBoss = true;
            }
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
                model.Persons.Image = BossModel.ImageToByteArray(image);
            }
            else
            {
                model.Persons.Image = imageArray;
            }

            if (model.TestModels.ChosenText == "ögrtmn")
            {
                model.Teacher.FacultyNumber = model.Faculty.Id;
                model.Teacher.PersonId = model.Persons.Id;
                _personManager.Update(model.Persons);
                _teacherManager.Update(model.Teacher);

            }
            else if (model.TestModels.ChosenText == "ögr")
            {
                model.Student.StudentNumber = model.Teacher.TeacherNumber;
                model.Student.PersonId = model.Teacher.PersonId;
                model.Student.StudentPassword = model.Teacher.TeacherPassword;
                model.Student.IsFirstPassword = model.Teacher.IsFirstPassword;
                model.Student.StudentMail = model.Teacher.TeacherMail;
                _teacherManager.Delete(model.Teacher);
                _personManager.Update(model.Persons);
                _studentManager.Add(model.Student);
            }
            else if (model.TestModels.ChosenText == "kom")
            {
                model.Teacher.FacultyNumber = model.Faculty.Id;
                model.Teacher.PersonId = model.Persons.Id;
                _personManager.Update(model.Persons);
                _teacherManager.Update(model.Teacher);
                if (durum == true)
                {
                    Komisyon boss = new Komisyon();
                    boss.TeacherId = model.Teacher.Id;
                    _komisyonManager.Add(boss);
                }
            }
            else if (model.TestModels.ChosenText == "boss")
            {
                if (_bossManager.GetAll().Count == 3)
                {
                    TempData["myError"] = " Daha fazla yönetici ekleyemezsiniz !.";
                    return RedirectToAction("ListStudent", "Boss");
                }
                model.Teacher.FacultyNumber = model.Faculty.Id;
                model.Teacher.PersonId = model.Persons.Id;
                _personManager.Update(model.Persons);
                _teacherManager.Update(model.Teacher);
                Boss boss = new Boss();
                if (_bossManager.GetById(model.Teacher.Id.ToString()) == null)
                {
                    boss.TeacherId = model.Teacher.Id;
                    _bossManager.Add(boss);
                }
            }
            return RedirectToAction("ListStudent", "Boss");
        }

        [HttpGet]
        public IActionResult OgrenciAktar(int id, IFormCollection student)
        {
            ViewBag.isBoss = _bossManager.IsSuperBoss(myTeacher.Id);
            ViewData["path"] = myPath;
            ViewData["Name"] = _nameSurname;
            return View();
        }
        public static List<Person> mPersons;
        [HttpPost]
        public IActionResult OgrenciAktar(IFormFile postedFile)
        {
            mPersons = new List<Person>();
            if (postedFile != null)
            {
                //Create a Folder.
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Save the uploaded Excel file.
                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                //Read the connection string for the Excel file.
                string conString = this.Configuration.GetConnectionString("ExcelConString");
                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            OleDbDataReader dr = cmdExcel.ExecuteReader();

                            while (dr.Read())
                            {
                                Person person = new Person();
                                person.IdentyNumber = dr["IdentyNumber"].ToString();
                                person.NameSurname = dr["StudentMail"].ToString();
                                person.PhoneNumber = dr["StudentNumber"].ToString();
                                mPersons.Add(person);
                            }
                            dr.Close();
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }

                //Insert the Data read from the Excel file to Database Table.
                conString = this.Configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.Persons";

                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("IdentyNumber", "IdentyNumber");
                        sqlBulkCopy.ColumnMappings.Add("Civilization", "Civilization");
                        sqlBulkCopy.ColumnMappings.Add("NameSurname", "NameSurname");
                        sqlBulkCopy.ColumnMappings.Add("Gender", "Gender");
                        sqlBulkCopy.ColumnMappings.Add("PhoneNumber", "PhoneNumber");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.Students";

                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("FacultyId", "FacultyId");
                        sqlBulkCopy.ColumnMappings.Add("StudentNumber", "StudentNumber");
                        sqlBulkCopy.ColumnMappings.Add("StudentMail", "StudentMail");
                        sqlBulkCopy.ColumnMappings.Add("PersonId", "PersonId");
                        sqlBulkCopy.ColumnMappings.Add("IsFirstPassword", "IsFirstPassword");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
                foreach (var item in mPersons)
                {
                    var person = _personManager.GetById(item.IdentyNumber);
                    var student = _studentManager.GetById(item.PhoneNumber);
                    student.StudentPassword = BossModel.SendMail(item.NameSurname);
                    student.StudentPassword = BossModel.GetMd5(student.StudentPassword);
                    student.PersonId = person.Id;
                    _studentManager.Update(student);
                }
            }
            return RedirectToAction("Index", "Boss", _teacherManager.GetById(myTeacher.TeacherNumber));
        }
    }
}
