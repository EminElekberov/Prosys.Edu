using InterView.Application.Models;
using InterView.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Application.Interfaces.User
{
    public interface IUserRepository
    {
        Task<ApiResponse> UsersGetList();
        Task<ApiResponse> CreateUser(UsersDto usersDto);
    }
}
