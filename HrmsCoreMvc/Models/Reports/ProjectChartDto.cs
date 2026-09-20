namespace HrmsCoreMvc.Models.Reports
{
    public class ProjectChartDto
    {
        public List<string> Chartlabels { get; set; } = new List<string>();

        public List<double> ActiveProjects { get; set; } = new List<double>();

        public List<double> InActiveProjects { get; set; } = new List<double>();

        public List<double> InProgressTasks { get; set; } = new List<double>();

        public List<double> CompletedTasks { get; set; } = new List<double>();
    }
}
