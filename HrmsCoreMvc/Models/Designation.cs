using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models
{
    public class Designation
    {

        [Key]
        public int DesignationId { get; set; }

        public string Name { get; set; }

        public  int NoOfEmployee { get; set; }

        public string status { get; set; }

        public DateTime CreatedAt  { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; } = "Admin";

        public string ?  ModifiedBy { get; set; }

        public DateTime ?  ModifiedAt { get; set; }

        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Departments departments { get; set; }
        public List<User> user { get; set; }


    }
}
