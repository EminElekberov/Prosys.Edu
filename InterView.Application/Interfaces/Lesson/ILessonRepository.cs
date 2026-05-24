using InterView.Application.Models;
using InterView.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Application.Interfaces.Lesson
{
    public interface ILessonRepository
    {
        Task<ApiResponse> GetListLesson();
        Task<ApiResponse> CreateOrUpdate(LessonDto lessonDto);
    }
}
