using InterView.Front.Controller;
using InterView.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InterView.Front.Controllers
{
    public class LessonController : MainController
    {
        public LessonController(RemoteServer.RemoteServer remoteServer) : base(remoteServer)
        {
        }

        public async Task<IActionResult> GetList()
        {
            var data = (List<LessonDto>)await _remoteServer.SendAsync<List<LessonDto>>("api/Lesson/GetListLesson", HttpMethod.Get);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetCreatePartial()
        {
            var dictianryList = (List<DictionariesDto>)await _remoteServer.SendAsync<List<DictionariesDto>>("api/Dictionary/GetList", HttpMethod.Get);
            var userlist = (List<UsersDto>)await _remoteServer.SendAsync<List<UsersDto>>("api/User/GetList", HttpMethod.Get);
            ViewBag.ClassList = dictianryList
                .Where(x => !string.IsNullOrEmpty(x.Code) && x.Code.Contains("Class"))
                .Select(z => new SelectListItem
                {
                    Value = z.Id.ToString(),
                    Text = z.Name
                })
                .ToList();
            ViewBag.TeacherList = (userlist ?? new List<UsersDto>())
                .Where(x => !string.IsNullOrEmpty(x.UserTypeName)
                         && x.UserTypeName.Contains("teacher"))
                .Select(z => new SelectListItem
                {
                    Value = z.Id.ToString(),
                    Text = $"{z.Name} {z.Surname}"
                })
                .ToList(); return PartialView("_AddLessonModalPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Create(LessonDto model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Zəhmət olmasa bütün mütləq sahələri doldurun." });

            // API və ya Service sorğunuzu bura bağlayırsınız
            var result = (ReturnApiDto)await _remoteServer.SendAsync<ReturnApiDto>("api/Lesson/CreateOrUpdate", HttpMethod.Post,model);


            if ((bool)result.isSuccess)
                return Json(new { success = true, message = "Yeni dərs proqramı uğurla təyin edildi!" });

            return Json(new { success = false, message = "Xeta" });
        }
    }
}
