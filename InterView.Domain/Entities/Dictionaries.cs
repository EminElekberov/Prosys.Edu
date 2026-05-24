using InterView.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Domain.Entities
{
    public class Dictionaries : TenantBaseClass<Guid>
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public Dictionaries ParentType { get; set; }
        public ICollection<Dictionaries> DictionariesChilds { get; set; }
        public Guid? ParentId { get; set; }

        #region Relation
        public ICollection<Users> Users { get; set; } = new List<Users>();
        public ICollection<Users> UsersClass { get; set; } = new List<Users>();
        public ICollection<Lesson> Lesson { get; set; } = new List<Lesson>();
        #endregion
    }
}
