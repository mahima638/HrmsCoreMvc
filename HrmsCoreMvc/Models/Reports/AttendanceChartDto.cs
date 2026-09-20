namespace HrmsCoreMvc.Models.Reports
{
    public class AttendanceChartDto
    {
        public List<string> Labels { get; set; } = new List<string>();
        public List<int> AbsentData { get; set; } = new List<int>();

        public List<int> PresentData { get; set; } = new List<int>();
    }
}
