namespace HrmsCoreMvc.Models.Reports
{
    public class DailyAttendanceChartDto
    {
        public List<string> ChartLabels { get; set; } = new List<string>();

        public List<int> Chartdata { get; set; } = new List<int>();
    }
}
