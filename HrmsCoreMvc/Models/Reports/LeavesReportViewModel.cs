namespace HrmsCoreMvc.Models.Reports
{
    public class LeavesReportViewModel
    {
        public int attendanceId { get; set; }

        public string userName { get; set; }

        public string leaveType { get; set; }

        public string profilephoto { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Days { get; set; }

        public string Reason {  get; set; }

        public string ApprovedBy { get; set; }

        public string Status { get; set; }
        public string StatusHistory { get; set; }


    }
}
