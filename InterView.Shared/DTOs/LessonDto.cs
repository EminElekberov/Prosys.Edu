using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Shared.DTOs
{
    public class LessonDto
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
        public string? ClassName { get; set; }
        public string? TeacherName { get; set; }
        public Guid? ClassId { get; set; }
        public Guid? TeacherId { get; set; }
    }
}
