using InterView.DataBase.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.DataBase.UnitOfWork
{
    public class MemoryUnitOfWork : IMemoryUnitOfWork
    {
        private readonly Context.Context _context;
        private ContextFactory context;

        public MemoryUnitOfWork(Context.Context context)
        {
            _context = context;
        }

        public MemoryUnitOfWork(ContextFactory context)
        {
            this.context = context;
        }

        public async void Dispose() => await _context.DisposeAsync();

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
        public Context.Context GetContext()
        {
            return _context;
        }


    }

}
