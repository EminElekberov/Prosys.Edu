using InterView.Domain.Enum;
using InterView.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Domain.Entities
{
    public class Users : TenantBaseClass<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Guid UserTypeId { get; set; }
        public Dictionaries DictionariesUserType { get; set; }


        public Guid? UserClassId { get; set; }
        public Dictionaries DictionariesUserClass { get; set; }


        #region
        public ICollection<Lesson> Lesson { get; set; } = new List<Lesson>();
        public ICollection<Exam> Exam { get; set; } = new List<Exam>();


        #endregion
    }
}
