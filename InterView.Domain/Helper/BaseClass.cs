using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Domain.Helper
{
    public class BaseClass<T> : BaseEntity<T>
    {
        public Guid? CreateUserId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public Guid? EditId { get; set; }
        public DateTime? EditDate { get; set; }
        public Guid? DeleteId { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
