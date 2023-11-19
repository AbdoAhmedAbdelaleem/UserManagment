using Microsoft.EntityFrameworkCore;
using UserManagement.Entities;

namespace UserManagement.Infrastructure.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<User> User { get; set; }
    }
}
