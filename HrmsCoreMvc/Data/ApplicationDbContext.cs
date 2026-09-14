using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Events;
using HrmsCoreMvc.Models.Projects;
using HrmsCoreMvc.Models.Promotion;
using HrmsCoreMvc.Models.Resignation;
using HrmsCoreMvc.Models.Termination;
using Microsoft.EntityFrameworkCore;
using Task = HrmsCoreMvc.Models.Projects.Task;
using HrmsCoreMvc.Models.PayRoll;
using HrmsCoreMvc.Models.Leave;
using HrmsCoreMvc.Models.Attendance;

namespace HrmsCoreMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Role> role { get; set; }
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the relationships and constraints for TaskMembers
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskMembers>()
                .HasOne(tm => tm.Task)
                .WithMany(t => t.TaskMembers)
                .HasForeignKey(tm => tm.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskMembers>()
                .HasOne(tm => tm.User)
                .WithMany()
                .HasForeignKey(tm => tm.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskBoard>()
                .HasOne(tb => tb.Task)
                .WithMany(t => t.TaskBoards)
                .HasForeignKey(tb => tb.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskBoard>()
                .HasOne(tb => tb.Project)
                .WithMany(p => p.TaskBoards)
                .HasForeignKey(tb => tb.ProjectId)
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


            modelBuilder.Entity<Deduction>()
                .HasOne(x => x.Department)
                .WithMany()
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Earning>()
               .HasOne(x => x.Designation)
               .WithMany()
               .HasForeignKey(x => x.DesignationId)
               .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Earning>()
                .HasOne(x => x.Department)
                .WithMany()
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<User>()
                .HasOne(x => x.designation)
                .WithMany()
                .HasForeignKey(x => x.DesignationId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<User>()
                .HasOne(x => x.departments)
                .WithMany()
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<LeaveBalance>()
                .HasOne(x => x.MasterLeaveType)
                .WithMany()
                .HasForeignKey(x => x.LeaveTypeId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<User>(u =>
            {
                u.HasOne(x => x.departments)
                 .WithMany(x => x.user)
                 .HasForeignKey(x => x.DepartmentId)
                 .OnDelete(DeleteBehavior.Restrict);


                u.HasOne(x => x.designation)
                .WithMany(x => x.user)
                .HasForeignKey(x => x.DesignationId)
                .OnDelete(DeleteBehavior.Restrict);

            });
            modelBuilder.Entity<Designation>(d =>
            {
                d.HasOne(x => x.departments)
                .WithMany(x => x.designation)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
        
        


        
    }
}


