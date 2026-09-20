namespace HrmsCoreMvc.Models
{
    public class EmployeeDashboardViewModel
    {
        public User Employee { get; set; }
        public int OnTime { get; set; }
        public int LateAttendance { get; set; }
        public int Absent { get; set; }
        public int SickLeave { get; set; }
        public int TotalLeaves { get; set; }
        public int Taken { get; set; }
        public int WorkedDays { get; set; }
        public int LossOfPay { get; set; }
        public decimal HoursToday { get; set; }
        public decimal HoursThisWeek { get; set; }
        public decimal HoursThisMonth { get; set; }
        public decimal OvertimeThisMonth { get; set; }
        public bool IsCheckedIn { get; set; }
        public DateTime? TodayCheckInTime { get; set; }
        public decimal TodayProductionHours { get; set; }
    }
}