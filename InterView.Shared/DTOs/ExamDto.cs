using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Shared.DTOs
{
    public class ExamDto
    {
        public Guid? Id { get; set; }
        public Guid? LessonId { get; set; }
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public string? LessonName { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public int? Price { get; set; }
    }
}
