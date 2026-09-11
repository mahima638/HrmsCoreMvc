using HrmsCoreMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Role> role { get; set; }


    }
}
