namespace HrmsCoreMvc.Models.Reports
{
    public class TaskReportViewModel
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public string ProjectName { get; set; }

        public DateTime? DueDate { get; set; }

        public string Priority { get; set; }

        public string Status { get; set; }
    }
}
