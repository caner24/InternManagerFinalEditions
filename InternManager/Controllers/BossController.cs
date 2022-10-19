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

namespace InternManager.WebUI.Controllers
{
    public class BossController : Controller
    {
        private IPersonManager _personManager;
        private IStudentManager _studentManager;
        private ITeacherManager _teacherManager;
        private IBossManager _bossManager;
        private IFacultyManager _facultyManager;
        private IInternManager _internManager;
        public BossController(IInternManager internManager, IFacultyManager facultyManager, IBossManager boss, ITeacherManager teacher, IPersonManager personManager, IStudentManager studentManager)
        {
            _internManager = internManager;
            _facultyManager = facultyManager;
            _teacherManager = teacher;
            _bossManager = boss;
            _personManager = personManager;
            _studentManager = studentManager;
        }
        [HttpGet]
        public IActionResult Index(BossModel model)
        {
            var personList = _personManager.GetAll();
            foreach (var item in personList)
            {
                item.Civilization = BossModel.ByteArrayToImageAsync(item.Image);
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
                ViewData["Staj1TarihV1"] = intern1Det.RecStart2.ToString("d");
                ViewData["Staj1TarihV2"] = intern1Det.RecEnd2.ToString("d");
            }
            if (intern2Det != null)
            {
                ViewData["Staj2Type"] = intern2Det.Type;
                ViewData["Staj2Tarih"] = intern2Det.RecStart.ToString("d");
                ViewData["Staj2Tarih2"] = intern2Det.RecEnd.ToString("d");
                ViewData["Staj2TarihV1"] = intern2Det.RecStart2.ToString("d");
                ViewData["Staj2TarihV2"] = intern2Det.RecEnd2.ToString("d");
            }
            if (iseDET != null)
            {
                ViewData["IseType"] = iseDET.Type;
                ViewData["ISE"] = iseDET.RecStart.ToString("d");
                ViewData["ISE2"] = iseDET.RecEnd.ToString("d");
                ViewData["ISEV1"] = iseDET.RecStart2.ToString("d");
                ViewData["ISEV2"] = iseDET.RecEnd2.ToString("d");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult CreateStudent()
        {
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
            return View();
        }
        [HttpGet]
        public IActionResult CreateTeacher()
        {
            ViewBag.Categories = new SelectList(_facultyManager.GetAll(), "Id", "FacultyName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(BossModel model, IFormFile file)
        {
            string myPassword = BossModel.SendMail(model.Teacher.TeacherMail);
            model.Teacher.TeacherPassword = myPassword;

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

            model.Teacher.FacultyNumber = model.Faculty.Id;
            model.Teacher.TeacherPassword = BossModel.GetMd5(model.Teacher.TeacherPassword);
            model.Teacher.PersonId = model.Persons.Id;
            _teacherManager.Add(model.Teacher);
            return View();
        }
        [HttpGet]
        public IActionResult Intern()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Intern(BossModel model)
        {
            _internManager.Add(model.Interns);
            return View();
        }
        [HttpGet]
        public IActionResult InternDet(string type)
        {
            return View(_internManager.GetById(type));
        }
        [HttpPost]
        public IActionResult InternDet(Intern intern)
        {
            _internManager.Update(intern);
            return View();
        }
        [HttpGet]
        public IActionResult Intern2()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Intern2(BossModel model)
        {
            _internManager.Add(model.Interns);
            return View();
        }
        [HttpGet]
        public IActionResult Intern2Det(string type)
        {
            return View(_internManager.GetById(type));
        }
        [HttpPost]
        public IActionResult Intern2Det(Intern intern)
        {
            _internManager.Update(intern);
            return View();
        }
        [HttpGet]
        public IActionResult ISE()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ISE(BossModel model)
        {
            _internManager.Add(model.Interns);
            return View();
        }
        [HttpGet]
        public IActionResult ISEDet(string type)
        {
            return View(_internManager.GetById(type));
        }
        [HttpPost]
        public IActionResult ISEDet(Intern intern)
        {
            _internManager.Update(intern);
            return View();
        }
    }
}
