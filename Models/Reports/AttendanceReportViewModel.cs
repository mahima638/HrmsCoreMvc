using Microsoft.Identity.Client;

namespace HrmsCoreMvc.Models.Reports
{
    public class AttendanceReportViewModel
    {
        public int AttendanceId { get; set; }

        public string profilephoto { get; set; }

        public string AttendanceName { get; set; }

        public DateTime Date { get; set; }

        public DateTime? CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public DateTime? LunchIn { get; set; }

        public DateTime? LunchOut { get; set; }

        public decimal WorkingHours { get; set; }

        public decimal ProductionHours { get; set; }

        public decimal OvertimeHours { get; set; }

        public decimal BreakHours { get; set; }

        public int Late { get; set; }

        public string Status { get; set; }

    }
}
