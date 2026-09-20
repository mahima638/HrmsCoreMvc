namespace HrmsCoreMvc.Models.Reports
{
    public class TaskChartDto
    {
        public List<string> ChartLabels { get; set; } = new List<string>();

        public List<double> ChartCompleted { get; set; } = new List<double>();
        public List<double> ChartPending { get; set; } = new List<double>();
        public List<double> ChartInProgress { get; set; } = new List<double>();
        public List<double> ChartOnHold { get; set; } = new List<double>();

    }
}
