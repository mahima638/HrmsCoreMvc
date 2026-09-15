using System.ComponentModel.DataAnnotations;

namespace HrmsCoreMvc.Models
{
    public class Departments

    {
        [Key]
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public int? NoOfEmployee { get; set; } = 0;

        public string Status { get; set; }

        public string CreatedBy { get; set; } = "Admin";

        public string ? ModifiedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime ? ModifiedAt { get; set; }

        public List<Designation> ? designation { get; set; }

        public List<User> ?  user { get; set; }

    }
}
