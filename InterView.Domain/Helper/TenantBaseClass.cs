using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Domain.Helper
{
    public class TenantBaseClass<T> : BaseClass<T>
    {
        public long TenantId { get; set; }

    }
}
