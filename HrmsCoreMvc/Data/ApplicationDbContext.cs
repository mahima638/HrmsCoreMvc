using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Promotion;
using HrmsCoreMvc.Models.Resignation;
using HrmsCoreMvc.Models.Termination;
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

        public DbSet<Departments> department { get; set; }

        public DbSet<Designation> designation { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<Promotion> promotion { get; set; }
        public DbSet<Resignation> resignation { get; set; }
        public DbSet<Termination> termination { get; set; }

    }
}

