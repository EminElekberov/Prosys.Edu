using DotNetCore.AspNetCore;
using InterView.Application.Interfaces.Dictionary;
using InterView.Application.Interfaces.Lesson;
using InterView.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterView.WebAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : BaseController
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonController(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }
        [HttpGet]
        [Route(nameof(GetListLesson))]
        public IActionResult GetListLesson() => _lessonRepository.GetListLesson().ApiResult();


        [HttpPost]
        [Route(nameof(CreateOrUpdate))]
        public IActionResult CreateOrUpdate(LessonDto lessonDto) => _lessonRepository.CreateOrUpdate(lessonDto).ApiResult();

    }
}
