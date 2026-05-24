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

namespace InterView.Application.Interfaces.Exam
{
    public class ExamService : BaseService<InterView.Domain.Entities.Exam>, IExamRepository
    {
        private readonly IBaseRepository<InterView.Domain.Entities.Exam> _examRepository;

        public ExamService(IMemoryUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IBaseRepository<Domain.Entities.Exam> examRepository) : base(unitOfWork, httpContextAccessor)
        {
            _examRepository = examRepository;
        }

        public async Task<ApiResponse> Create(ExamDto examDto)
        {
            try
            {
                var entity = new InterView.Domain.Entities.Exam
                {
                    CreateDate = DateTime.UtcNow,
                    Status = Domain.Enum.StatusEnum.Active,
                    LessonId = examDto.LessonId,
                    UserId = examDto.UserId,
                    RegistrationDate = (DateTime)examDto.RegistrationDate,
                    Price = examDto.Price,
                };
                await _examRepository.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                return new SuccessfulResponse(new ReturnApiDto { isSuccess = true });
            }
            catch (Exception ex)
            {
                return new ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetList()
        {
            try
            {
                var data = _examRepository.Queryable.Where(x => x.Status != Domain.Enum.StatusEnum.Deleted).Select(z => new ExamDto
                {
                    Id = z.Id,
                    UserId = z.UserId,
                    UserName = z.Users.Name,
                    LessonId = z.LessonId,
                    LessonName = z.Lesson.Name,
                    Price = z.Price,
                    RegistrationDate = z.RegistrationDate
                }).ToList();
                return new SuccessfulResponse(data);
            }
            catch (Exception ex)
            {
                return new ErrorResponse(ex.Message);
            }
        }
    }
}
