namespace HrmsCoreMvc.Models.Reports
{
    public class LeaveChartDto
    {
        public List<string> Labels { get; set; }= new List<string>();

        public List<int> PaidLeaves { get; set; } = new List<int>();
    }
}
