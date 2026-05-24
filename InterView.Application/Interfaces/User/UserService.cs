using InterView.Application.Interfaces.Lesson;
using InterView.Application.Models;
using InterView.Application.Services;
using InterView.DataBase.BaseRepository;
using InterView.DataBase.UnitOfWork;
using InterView.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Application.Interfaces.User
{
    public class UserService : BaseService<InterView.Domain.Entities.Users>, IUserRepository
    {
        private readonly IBaseRepository<InterView.Domain.Entities.Users> _usersRepository;

        public UserService(IMemoryUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IBaseRepository<Domain.Entities.Users> usersRepository) : base(unitOfWork, httpContextAccessor)
        {
            _usersRepository = usersRepository;
        }

        public async Task<ApiResponse> CreateUser(UsersDto usersDto)
        {
            try
            {
                var entity = new InterView.Domain.Entities.Users
                {
                    CreateDate = DateTime.UtcNow,
                    Status = Domain.Enum.StatusEnum.Active,
                    Name = usersDto.Name,
                    Surname = usersDto.Surname,
                    UserTypeId = (Guid)usersDto.UserTypeId,
                    UserClassId= (Guid)usersDto.UserClassId,
                };
                await _usersRepository.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                return new SuccessfulResponse(new ReturnApiDto
                {
                    isSuccess = true,
                });
            }
            catch (Exception ex)
            {
                return new ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UsersGetList()
        {
            try
            {
                var data = _usersRepository.Queryable.Where(x => x.Status != Domain.Enum.StatusEnum.Deleted).Select(z => new UsersDto
                {
                    Id = z.Id,
                    Name = z.Name,
                    Surname = z.Surname,
                    UserTypeId = z.UserTypeId,
                    UserTypeName = z.DictionariesUserType.Title,
                }).ToList();
                return new SuccessfulResponse(data);
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }
    }
}
