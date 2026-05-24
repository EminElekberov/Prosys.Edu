using DotNetCore.AspNetCore;
using InterView.Application.Interfaces.Exam;
using InterView.Application.Interfaces.Lesson;
using InterView.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterView.WebAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : BaseController
    {
        private readonly IExamRepository _examRepository;

        public ExamController(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }


        [HttpGet]
        [Route(nameof(GetList))]
        public IActionResult GetList() => _examRepository.GetList().ApiResult();


        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(ExamDto model) => _examRepository.Create(model).ApiResult();

    }
}
