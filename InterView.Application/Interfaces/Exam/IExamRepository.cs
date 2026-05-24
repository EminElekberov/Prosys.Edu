using InterView.Application.Models;
using InterView.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Application.Interfaces.Exam
{
    public interface IExamRepository
    {
        Task<ApiResponse> GetList();
        Task<ApiResponse> Create(ExamDto examDto);
    }
}
