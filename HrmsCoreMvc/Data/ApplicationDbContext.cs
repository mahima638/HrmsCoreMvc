using HrmsCoreMvc.Models;


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


    }
}
