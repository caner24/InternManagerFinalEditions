using InternManager.Business.Abstract;
using InternManager.Entities.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace InternManager.WebUI.ViewCompoments
{
    public class GetTeacherViewComponent : ViewComponent
    {
        private IPersonManager _personManager;
        private IStudentManager _studentManager;
        private ITeacherManager _teacherManager;
        private IBossManager _bossManager;
        public GetTeacherViewComponent(IBossManager boss, ITeacherManager teacher, IPersonManager personManager, IStudentManager studentManager)
        {
            _teacherManager = teacher;
            _bossManager = boss;
            _personManager = personManager;
            _studentManager = studentManager;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_teacherManager.GetById(id));
        }
    }
}
