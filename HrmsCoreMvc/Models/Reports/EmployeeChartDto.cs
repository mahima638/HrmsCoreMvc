namespace HrmsCoreMvc.Models.Reports
{
    public class EmployeeChartDto
    {
        public List<string> Labels { get; set; } = new List<string>();
        public List<int> ActiveData { get; set; }= new List<int>();

        public List<int> InactiveData { get; set; } = new List<int>();
    }
}
