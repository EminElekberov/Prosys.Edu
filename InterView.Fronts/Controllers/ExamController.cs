using InterView.Front.Controller;
using InterView.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InterView.Front.Controllers
{
    public class ExamController : MainController
    {
        public ExamController(RemoteServer.RemoteServer remoteServer) : base(remoteServer)
        {
        }





        public async Task<IActionResult> GetList()
        {
            var exams = (List<ExamDto>)await _remoteServer.SendAsync<List<ExamDto>>("api/Exam/GetList", HttpMethod.Get);
            return View(exams);
        }
        [HttpGet]
        public async Task<IActionResult> GetCreatePartial()
        {
            var users = (List<UsersDto>)await _remoteServer.SendAsync<List<UsersDto>>("api/User/GetList", HttpMethod.Get);
            ViewBag.student = users
                .Where(x => !string.IsNullOrEmpty(x.UserTypeName) && x.UserTypeName.Contains("Student"))
                .Select(z => new SelectListItem
                {
                    Value = z.Id.ToString(),
                    Text = z.Name
                })
                .ToList();

            var LessonList = (List<LessonDto>)await _remoteServer.SendAsync<List<LessonDto>>("api/Lesson/GetListLesson", HttpMethod.Get);
            ViewBag.Lesson = LessonList.Select(s => new SelectListItem
            {
                Value = s.id.ToString(),
                Text = s.name,
            }).ToList();
            return PartialView("_AddExamModalPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Create(ExamDto model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Zəhmət olmasa bütün mütləq sahələri tam doldurun." });
            var result = (ReturnApiDto)await _remoteServer.SendAsync<ReturnApiDto>("api/Exam/Create", HttpMethod.Post, model);


            if ((bool)result.isSuccess)
                return Json(new { success = true, message = "İmtahan balı uğurla sistemə daxil edildi!" });

            return Json(new { success = false, message = "Xeta" });
        }
    }
}
