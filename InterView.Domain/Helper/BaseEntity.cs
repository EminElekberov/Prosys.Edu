using InterView.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Domain.Helper
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
        public StatusEnum Status { get; set; }
    }
}
