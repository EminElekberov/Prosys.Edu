using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.DataBase.UnitOfWork
{
    public interface IMemoryUnitOfWork
    {
        int SaveChanges();
        Context.Context GetContext();
        Task<int> SaveChangesAsync();
    }
}
