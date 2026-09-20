namespace HrmsCoreMvc.Models
{
    public class EmployeeGridViewModel
    {
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public int InactiveEmployees { get; set; }
        public int NewJoiners { get; set; }
        public List<EmployeeGridItem> EmployeeGridList { get; set; } = new List<EmployeeGridItem>();
    }

  
}