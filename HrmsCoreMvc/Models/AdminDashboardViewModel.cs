using HrmsCoreMvc.Models.Projects;

namespace HrmsCoreMvc.Models
{
    public class AdminDashboardViewModel
    {
        public User Admin { get; set; }
        public int PresentToday { get; set; }
        public int TotalEmployees { get; set; }
        public int NewHireThisMonth { get; set; }
        public int TotalProjects { get; set; }
        public int TotalClients { get; set; }
        public int TotalTasks { get; set; }

        
        public decimal Earnings { get; set; } = 0;
        public decimal ProfitThisWeek { get; set; } = 0;
        public int JobApplicants { get; set; } = 0;
        
        public List<DepartmentCount> DepartmentCounts { get; set; } = new List<DepartmentCount>();
        public List<ClockInEntry> TodayClockIns { get; set; } = new List<ClockInEntry>();
        public List<AllProjects> RecentProjects { get; set; } = new List<AllProjects>();
        public List<StatusCount> TaskStatusBreakdown { get; set; } = new List<StatusCount>();
        public List<EmployeeListItem> AllEmployeesList { get; set; } = new List<EmployeeListItem>();
        public List<EmployeeGridItem> EmployeeGridList { get; set; } = new List<EmployeeGridItem>();


        public int TotalTasksCompleted { get; set; }
        public decimal ProductionHoursPercent { get; set; }
        public decimal WorkingHoursPercent { get; set; }

    }
    public class DepartmentCount
    {
        public string DepartmentName { get; set; }
        public int Count { get; set; }
      
    }
    public class ClockInEntry
    {
        public string FullName { get; set; }
        public string DesignationName { get; set; }
        public string ProfilePicture { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public string ProductionHoursDisplay { get; set; }
        public bool IsLate { get; set; }
        public int LateMinutes { get; set; }

    }
    public class StatusCount
    {
        public string StatusName { get; set; }
        public int Count { get; set; }
    }
    public class EmployeeListItem
    {
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
        public string DepartmentName { get; set; }
    }
    public class EmployeeGridItem
    {
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
        public string DesignationName { get; set; }
        public int ProjectsCount { get; set; }
        public int DoneCount { get; set; }
        public int ProgressCount { get; set; }
        public decimal ProductivityPercent { get; set; }
    }
}
