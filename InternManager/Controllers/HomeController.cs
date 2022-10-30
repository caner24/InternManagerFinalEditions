using InternManager.Entities.Concrate;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using InternManager.Business.Abstract;
using InternManager.Entities.Abstract;
using System.Security.Cryptography;
using InternManager.WebUI.Models;
using InternManager.WebUI.Controllers;

namespace InternManager.Controllers
{
    public class HomeController : Controller
    {
        private IPersonManager _personManager;
        private IStudentManager _studentManager;
        private ITeacherManager _teacherManager;
        private IBossManager _bossManager;
        private IKomisyonManager _komisyonManager;
        public HomeController(IKomisyonManager komisyonManager, IBossManager boss, ITeacherManager teacher, IPersonManager personManager, IStudentManager studentManager)
        {
            _komisyonManager = komisyonManager;
            _teacherManager = teacher;
            _bossManager = boss;
            _personManager = personManager;
            _studentManager = studentManager;
        }

        [HttpGet]
        public IActionResult Intern1Page()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }
        [HttpGet]
        public IActionResult StudentLogin()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }
        [HttpPost]
        public IActionResult StudentLogin([Bind(nameof(Student.StudentNumber), nameof(Student.StudentPassword))] Student student)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(student.StudentPassword));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            if (_studentManager.getPerson(student.StudentNumber, strBuilder.ToString()) != null)
            {
                return RedirectToAction("Index", "Student", _studentManager.GetById(student.StudentNumber));
            }
            else
            {
                TempData["LoginError"] = "Hatali Bir Giriş Yaptiniz !.";
                return View();
            }
        }


        [HttpGet]
        public IActionResult ResetPassword()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(string ogrNo, string identyNumber)
        {
            var student = _studentManager.GetById(ogrNo);
            var teacher = _teacherManager.GetById(ogrNo);
            if (_personManager.GetById(identyNumber) != null && student != null)
            {
                student.StudentPassword = BossModel.SendMail(student.StudentMail);
                student.IsFirstPassword = false;
                student.StudentPassword = BossModel.GetMd5(student.StudentPassword);
                _studentManager.Update(student);
                TempData["LoginError"] = "Şifreniz eposta adresinize gönderildi!.";
                return RedirectToAction("StudentLogin", "Home");
            }
            else if (_personManager.GetById(identyNumber) != null && teacher != null)
            {
                
                teacher.TeacherPassword = BossModel.SendMail(teacher.TeacherMail);
                teacher.IsFirstPassword = false;
                teacher.TeacherPassword = BossModel.GetMd5(teacher.TeacherPassword);
                _teacherManager.Update(teacher);
                TempData["LoginError"] = "Şifreniz eposta adresinize gönderildi!.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["LoginError"] = "Hatali Bir Giriş Yaptiniz !.";
                return View();
            }

        }

        [HttpGet]
        public IActionResult TeacherLogin()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }
        [HttpPost]
        public IActionResult TeacherLogin(Teacher teacher)
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
                var myTeacher = _teacherManager.GetById(teacher.TeacherNumber);
                if (_komisyonManager.GetTeachId(myTeacher.Id.ToString())!=null)
                {
                    return RedirectToAction("Index", "Komisyon", _teacherManager.GetById(teacher.TeacherNumber));
                }
                return RedirectToAction("Index", "Teacher", _teacherManager.GetById(teacher.TeacherNumber));
            }
            else
            {
                TempData["LoginError"] = "Hatali Bir Giriş Yaptiniz !.";
                return View();
            }
        }
        [HttpGet]
        public IActionResult BossLogin()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }
        [HttpPost]
        public IActionResult BossLogin(Teacher _teacher)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(_teacher.TeacherPassword));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            var _myTeaches = _teacherManager.GetTeacher(_teacher.TeacherNumber, strBuilder.ToString());
            if (_myTeaches != null)
            {
                if (_bossManager.GetById(_myTeaches.Id.ToString()) != null)
                {
                    return RedirectToAction("Index", "Boss", _myTeaches);
                }
                else
                {
                    TempData["LoginError"] = "Hatali Bir Giriş Yaptiniz !.";
                    return View();
                }
            }
            else
            {
                TempData["LoginError"] = "Hatali Bir Giriş Yaptiniz !.";
                return View();
            }
        }

    }
}
