using InterView.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace InterView.Front.Controller
{
    public class DictionaryController : MainController
    {
        public DictionaryController(RemoteServer.RemoteServer remoteServer) : base(remoteServer)
        {
        }
        public async Task<IActionResult> GetList()
        {
            var data = (List<DictionariesDto>)await _remoteServer.SendAsync<List<DictionariesDto>>("api/Dictionary/GetList", HttpMethod.Get);
            data = data.Where(x => x.Parentid != null).ToList();
            return View(data);
        }


        [HttpGet]
        public async Task<IActionResult> GetCreatePartial()
        {
            var data = (List<DictionariesDto>)await _remoteServer.SendAsync<List<DictionariesDto>>("api/Dictionary/GetList", HttpMethod.Get);
            data = data.Where(x => x.Parentid == null).ToList();
            return PartialView("_AddDictionaryModalPartial", data);
        }
        [HttpPost]
        public async Task<IActionResult> Create(DictionariesDto model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Məlumatlar tam doldurulmayıb." });
            }

            try
            {
                // Baza əməliyyatın (insert) bura yazılır
                // await _dictionaryService.AddAsync(model);
                var data = (ReturnApiDto)await _remoteServer.SendAsync<ReturnApiDto>("api/Dictionary/CreateOrUpdateDictianary", HttpMethod.Post, model);
                return Json(new { success = data.isSuccess, message = data.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Xəta: " + ex.Message });
            }
        }
    }
}
