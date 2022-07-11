using DeMozzWeb.Model;
using Microsoft.EntityFrameworkCore;

namespace DeMozzWeb.Data
{       
    public class DBConnection : DbContext
    {
        public DBConnection(DbContextOptions<DBConnection> options) : base(options) {}
        public DbSet<Category> Category { get; set; }

        public DbSet<CV> CV { get; set; }

    }
}
