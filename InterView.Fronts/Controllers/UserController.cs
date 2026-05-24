using InterView.Front.Controller;
using InterView.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Immutable;

namespace InterView.Front.Controllers
{
    public class UserController : MainController
    {
        public UserController(RemoteServer.RemoteServer remoteServer) : base(remoteServer)
        {
        }



        public async Task<IActionResult> GetList()
        {
            var users = (List<UsersDto>)await _remoteServer.SendAsync<List<UsersDto>>("api/User/GetList", HttpMethod.Get);
            return View(users);
        }




        [HttpGet]
        public async Task<IActionResult> GetCreatePartial()
        {
            var dictianryList = (List<DictionariesDto>)await _remoteServer.SendAsync<List<DictionariesDto>>("api/Dictionary/GetList", HttpMethod.Get);
            ViewBag.usertypes = dictianryList
                .Where(x => !string.IsNullOrEmpty(x.Code) && x.Code.Contains("UserType"))
                .Select(z => new SelectListItem
                {
                    Value = z.Id.ToString(),
                    Text = z.Name
                })
                .ToList();


            ViewBag.userclass = dictianryList
               .Where(x => !string.IsNullOrEmpty(x.Code) && x.Code.Contains("Class"))
               .Select(z => new SelectListItem
               {
                   Value = z.Id.ToString(),
                   Text = z.Name
               })
               .ToList();

            return PartialView("_AddUserModalPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsersDto model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Zəhmət olmasa məlumatları tam doldurun." });

            var result = (ReturnApiDto)await _remoteServer.SendAsync<ReturnApiDto>("api/User/CreateUser", HttpMethod.Post, model);


            if ((bool)result.isSuccess)
                return Json(new { success = true, message = "Yeni istifadəçi uğurla qeydiyyatdan keçdi!" });

            return Json(new { success = false, message = "Xeta" });
        }
    }
}
