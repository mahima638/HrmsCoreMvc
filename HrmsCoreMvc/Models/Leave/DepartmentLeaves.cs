using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.Leave
{
    public class DepartmentLeaves
    {
        [Key]
        public int DepartmentLeavesId { get; set; }

        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
        public Departments Department { get; set; }

        [ForeignKey("LeaveTypeId")]
        public int LeaveTypeId { get; set; }
        public MasterLeaveType MasterLeaveType { get; set; }
      
        public int LeavesCount { get; set; }
        public string Status { get; set; }
    }
}
