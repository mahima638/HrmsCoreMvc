using AttendanceModel= HrmsCoreMvc.Models.Attendance.Attendance;
namespace HrmsCoreMvc.Models.ViewModel
{
    public class AttendanceViewModel
    {
        public AttendanceModel TodayAttendance { get; set; }

        public List<AttendanceModel> AttendanceHistory { get; set; } = new();

        public string EmployeeName { get; set; }

        public string? ProfilePicture { get; set; }

        public decimal TotalHoursToday { get; set; }

        public decimal TotalHoursWeek { get; set; }

        public decimal TotalHoursMonth { get; set; }

        public decimal ProductionHours { get; set; }

        public decimal BreakHours { get; set; }

        public decimal OvertimeHours { get; set; }
    }
}
