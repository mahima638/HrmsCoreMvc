using HrmsCoreMvc.Models;
using Microsoft.EntityFrameworkCore;
using HrmsCoreMvc.Models.Attendance;
using HrmsCoreMvc.Models.Events;
using HrmsCoreMvc.Models.Leave;
using HrmsCoreMvc.Models.PayRoll;
using HrmsCoreMvc.Models.Projects;
using HrmsCoreMvc.Models.Promotion;
using HrmsCoreMvc.Models.Resignation;
using HrmsCoreMvc.Models.Termination;
using HrmsCoreMvc.Models.Ticketing;

using Task = HrmsCoreMvc.Models.Projects.Task;


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
        public DbSet<Ticket> tickets { get; set; }
        public DbSet<TicketComment> ticketcomments { get; set; }
        public DbSet<TicketResolution> ticketresolutions { get; set; }
        public DbSet<TicketAttachment> ticketattachments { get; set; }
        public DbSet<TicketHistory> tickethistories { get; set; }

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

            modelBuilder.Entity<Trainer>()
                .Property(x => x.Status)
                .HasConversion<string>();

            // Configure the relationships and constraints for TaskMembers
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
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



            modelBuilder.Entity<Promotion>(p =>
            {
                p.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Resignation>(r =>
            {
                r.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                r.HasOne(x => x.Departments)
                .WithMany()
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Termination>(t =>
            {
                t.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.RaisedByUser)
                .WithMany()
                .HasForeignKey(t => t.RaisedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.AssignedToUser)
                .WithMany()
                .HasForeignKey(t => t.AssignedTo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.AssignedByUser)
                .WithMany()
                .HasForeignKey(t => t.AssignedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TicketComment>()
                .HasOne(c => c.Ticket)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TicketResolution>()
                .HasOne(r => r.Ticket)
                .WithOne(t => t.Resolution)
                .HasForeignKey<TicketResolution>(r => r.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TicketAttachment>()
                .HasOne(a => a.Ticket)
                .WithMany(t => t.Attachments)
                .HasForeignKey(a => a.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TicketHistory>()
                .HasOne(h => h.Ticket)
                .WithMany()
                .HasForeignKey(h => h.TicketId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<ProjectsUser>().HasKey(pu => new
            {
                pu.ProjectsProjectId, pu.UsersUserId
            });

            modelBuilder.Entity<ProjectsUser>(pu =>
            {
                pu.HasOne(x => x.AllProjects)
                .WithMany(x => x.projectusers)
                .HasForeignKey(x => x.ProjectsProjectId)
                .OnDelete(DeleteBehavior.Restrict);

                pu.HasOne(x => x.user)
                .WithMany(x => x.projectsUser)
                .HasForeignKey(x => x.UsersUserId)
                .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<TaskMembers>()
                .HasOne(tm => tm.Task)
                .WithMany(t => t.TaskMembers)
                .HasForeignKey(tm => tm.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>(u =>
            {
                u.HasOne(x => x.Role)
                .WithMany(x => x.user)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
        
        


        
    }
}


