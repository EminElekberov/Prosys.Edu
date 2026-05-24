using InterView.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Domain.Entities
{
    public class Lesson : TenantBaseClass<Guid>
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Users Users { get; set; }
        public Guid ClassId { get; set; }
        public Dictionaries DictionariesClass { get; set; }
        public ICollection<Exam> Exam { get; set; } = new List<Exam>();

    }
}
