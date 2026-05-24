using InterView.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Domain.Entities
{
    public class Exam : TenantBaseClass<Guid>
    {
        public Guid? LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public Guid? UserId { get; set; }
        public Users Users { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int? Price { get; set; }
    }
}
