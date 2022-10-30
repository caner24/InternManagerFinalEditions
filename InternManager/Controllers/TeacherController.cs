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

namespace InternManager.WebUI.Controllers
{
    public class TeacherController : Controller
    {

        private IPersonManager _personManager;
        private ITeacherManager _teacherManager;
        private IIntern1Manager _internManager;
        private IStudentManager _studentManager;
        private IHostingEnvironment Environment;
        private IConfiguration Configuration;
        public static string myId;
        public static string myPath;
        public static Person _person;
        public string[] _studentPass = new string[1];
        public static Teacher teacher;
        public static Teacher myTeacher;
        public static List<Person> mPersons;
        public TeacherController(IIntern1Manager internManager, IHostingEnvironment _environment, IConfiguration _configuration, IStudentManager studentManager, IPersonManager personManager, ITeacherManager teacherManager)
        {
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
            if (person.Image != null)
            {
                string myPath = BossModel.ByteArrayToImageAsync(person.Image);
                ViewData["path"] = myPath;
            }

            ViewData["Name"] = person.NameSurname;
            ViewData["Id"] = teacher.TeacherNumber;
            myId = teacher.TeacherNumber;

            var intern1Det = _internManager.GetById("Staj1");
            var intern2Det = _internManager.GetById("Staj2");
            var iseDET = _internManager.GetById("ISE");

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
        public IActionResult ExcellToDb(int id, IFormCollection student)
        {

            return View();
        }

        [HttpPost]
        public IActionResult ExcellToDb(IFormFile postedFile)
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
                    student.StudentPassword =BossModel.SendMail(item.NameSurname);
                    student.StudentPassword = BossModel.GetMd5(student.StudentNumber);
                    student.PersonId = person.Id;
                    _studentManager.Update(student);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult ListStudents(string id)
        {
            TeacherModel model = new TeacherModel();
            var internList = _internManager.Get(id);
            model.Teacher = teacher;
            if (internList != null)
            {
                foreach (var item in internList)
                {
                    model.StudentList.Add(_studentManager.Get(item.Student_Id.ToString()));
                }
            }


            ViewData["path"] = myPath;
            ViewData["Name"] = _person.NameSurname;
            return View(model);
        }
    }
}
