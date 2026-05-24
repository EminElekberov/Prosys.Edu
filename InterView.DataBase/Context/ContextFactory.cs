using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.DataBase.Context
{
    public sealed class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            const string connectionString = "Server=DESKTOP-GJOFRNV\\SQLEXPRESS;Database=EduMigRation;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
            return new Context(new DbContextOptionsBuilder<Context>().UseSqlServer(connectionString).Options);

        }
    }
}
