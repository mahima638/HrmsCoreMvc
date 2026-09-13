using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Events;
using HrmsCoreMvc.Models.Projects;
using HrmsCoreMvc.Models.Promotion;
using HrmsCoreMvc.Models.Resignation;
using HrmsCoreMvc.Models.Termination;
using Microsoft.EntityFrameworkCore;
using Task = HrmsCoreMvc.Models.Projects.Task;

namespace HrmsCoreMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Role> role { get; set; }
        public DbSet<Departments> department { get; set; }
        public DbSet<Designation> designation { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<EventType> eventtypes { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<AllProjects> AllProjects { get; set; }
        public DbSet<Task> tasks { get; set; }
        public DbSet<TaskMembers> taskmembers { get; set; }
        public DbSet<TaskBoard> taskboards { get; set; }
        public DbSet<Promotion> promotion { get; set; }
        public DbSet<Termination> terminations { get; set; }
        public DbSet<Resignation> resignations { get; set; }        

    }
}

