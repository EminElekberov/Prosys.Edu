using DotNetCore.AspNetCore;
using InterView.Application.Interfaces.Dictionary;
using InterView.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterView.WebAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class DictionaryController : BaseController
    {
        private readonly IDictionaryRepository _dictionaryRepository;

        public DictionaryController(IDictionaryRepository dictionaryRepository)
        {
            _dictionaryRepository = dictionaryRepository;
        }
        [HttpGet]
        [Route(nameof(GetList))]
        public IActionResult GetList() => _dictionaryRepository.GetListDictionary().ApiResult();


        [HttpPost]
        [Route(nameof(CreateOrUpdateDictianary))]
        public IActionResult CreateOrUpdateDictianary(DictionariesDto dictionariesDto) => _dictionaryRepository.CreateOrUpdateDictianary(dictionariesDto).ApiResult();

    }
}
