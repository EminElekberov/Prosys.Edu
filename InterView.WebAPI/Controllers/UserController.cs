using DotNetCore.AspNetCore;
using InterView.Application.Interfaces.Dictionary;
using InterView.Application.Interfaces.User;
using InterView.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterView.WebAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route(nameof(GetList))]
        public IActionResult GetList() => _userRepository.UsersGetList().ApiResult();


        [HttpPost]
        [Route(nameof(CreateUser))]
        public IActionResult CreateUser(UsersDto usersDto) => _userRepository.CreateUser(usersDto).ApiResult();


    }
}
