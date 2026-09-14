namespace HrmsCoreMvc.Models.Reports
{
    public class ProjectReportsViewModel
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string Leader { get; set; }

        public List<string> Members { get; set; }

        public DateTime Deadline { get; set; }

        public string Priority { get; set; }

        public string Status { get; set; }

    }
}
