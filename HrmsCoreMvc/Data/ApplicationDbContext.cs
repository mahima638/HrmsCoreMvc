using HrmsCoreMvc.Models;
<<<<<<< Updated upstream
using Microsoft.EntityFrameworkCore;
=======
using HrmsCoreMvc.Models.Attendance;
using HrmsCoreMvc.Models.Events;
using HrmsCoreMvc.Models.Leave;
using HrmsCoreMvc.Models.PayRoll;
using HrmsCoreMvc.Models.Projects;
using HrmsCoreMvc.Models.Promotion;
using HrmsCoreMvc.Models.Resignation;
using HrmsCoreMvc.Models.Termination;
using Microsoft.EntityFrameworkCore;
using HrmsCoreMvc.Models;
using Task = HrmsCoreMvc.Models.Projects.Task;
>>>>>>> Stashed changes

namespace HrmsCoreMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Role> role { get; set; }
<<<<<<< Updated upstream
=======
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<DepartmentLeaves> DepartmentLeaves { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<MasterLeaveType> MasterLeaveTypes { get; set; }
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
        
        public DbSet<Deduction> Deduction { get; set; }
        public DbSet<DeductionType> DeductionType { get; set; }
        public DbSet<Earning> Earning { get; set; }
        public DbSet<EarningType> EarningType { get; set; }
        public DbSet<Payslips> Payslips { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<TrainingType> TraningType { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<Trainer> Trainer { get; set; }
        public DbSet<AdminDocuments> AdminDocuments { get; set; }
        public DbSet<AddEmpDocName> AddEmpDocName { get; set; }
        public DbSet<AddAdminDocName> AddAdminDocName { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TrainingType Status stored as string
            modelBuilder.Entity<TrainingType>()
                .Property(x => x.Status)
                .HasConversion<string>();

            // Configure the relationships and constraints for TaskMembers
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Project)
                .WithMany(p => p.tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Task>()
                .HasOne(t => t.TaskBoard)
                .WithMany(tb => tb.Tasks)
                .HasForeignKey(t => t.TaskBoardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskMembers>()
                .HasOne(tm => tm.User)
                .WithMany()
                .HasForeignKey(tm => tm.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Task>()
                .HasOne(t => t.TaskBoard)
                .WithMany(tb => tb.Tasks)
                .HasForeignKey(t => t.TaskBoardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.EventType)
                .WithMany(et => et.Events)
                .HasForeignKey(e => e.EventTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Deduction>()
                .HasOne(x => x.Designation)
                .WithMany()
                .HasForeignKey(x => x.DesignationId)
                .OnDelete(DeleteBehavior.NoAction);
>>>>>>> Stashed changes


    }
}
