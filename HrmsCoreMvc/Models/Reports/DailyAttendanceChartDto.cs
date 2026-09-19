namespace HrmsCoreMvc.Models.Reports
{
    public class DailyAttendanceChartDto
    {
        public List<string> ChartLabels { get; set; } = new List<string>();

        public int Chartpresent {  get; set; } 

        public int Chartabsent { get; set; } 
    }
}
