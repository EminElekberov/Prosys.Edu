using InterView.Application.Interfaces.Dictionary;
using InterView.Application.Models;
using InterView.Application.Services;
using InterView.DataBase.BaseRepository;
using InterView.DataBase.UnitOfWork;
using InterView.Domain.Entities;
using InterView.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Application.Interfaces.Lesson
{
    public class LessonService : BaseService<InterView.Domain.Entities.Lesson>, ILessonRepository
    {
        private readonly IBaseRepository<InterView.Domain.Entities.Lesson> _lessonRepository;

        public LessonService(IMemoryUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IBaseRepository<Domain.Entities.Lesson> lessonRepository) : base(unitOfWork, httpContextAccessor)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<ApiResponse> CreateOrUpdate(LessonDto lessonDto)
        {
            try
            {
                if (lessonDto.id != Guid.Empty && lessonDto.id != null)
                {
                    var lessons = _lessonRepository.Queryable.Where(x => x.Id == lessonDto.id).FirstOrDefault();
                    if (lessons != null)
                    {
                        lessons.UserId = (Guid)lessonDto.TeacherId;
                        lessons.ClassId = (Guid)lessonDto.ClassId;
                        lessons.Name = lessonDto.name;
                        await _lessonRepository.UpdatePartialAsync(lessons);
                    }
                }
                else
                {
                    var entity = new InterView.Domain.Entities.Lesson
                    {
                        CreateDate = DateTime.UtcNow,
                        Status = Domain.Enum.StatusEnum.Active,
                        ClassId = (Guid)lessonDto.ClassId,
                        Name = lessonDto.name,
                        UserId = (Guid)lessonDto.TeacherId,
                    };
                    await _lessonRepository.AddAsync(entity);
                }
                await _unitOfWork.SaveChangesAsync();
                return new SuccessfulResponse(new ReturnApiDto
                {
                    isSuccess = true,
                });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ApiResponse> GetListLesson()
        {
            try
            {
                var data = _lessonRepository.Queryable.Where(x => x.Status != Domain.Enum.StatusEnum.Deleted).Select(z => new LessonDto
                {
                    id = z.Id,
                    ClassId = z.ClassId,
                    ClassName = z.DictionariesClass.Title,
                    name = z.Name,
                    TeacherId = z.UserId,
                    TeacherName = $"{z.Users.Name} {z.Users.Surname}"
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
