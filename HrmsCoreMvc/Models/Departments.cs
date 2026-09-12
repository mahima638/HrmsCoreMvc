namespace HrmsCoreMvc.Models
{
    public class Departments
    {

        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public int NoOfEmployee { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public List<Designation >designation { get; set; }
    }
}
