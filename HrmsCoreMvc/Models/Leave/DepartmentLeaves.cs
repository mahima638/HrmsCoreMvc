using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.Leave
{
    public class DepartmentLeaves
    {
        [Key]
        public int DepartmentLeavesId { get; set; }

        [ForeignKey("DepartmentId")]
        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }
        public Departments? Department { get; set; }

        [ForeignKey("MasterLeaveType")]
        [Required(ErrorMessage = "Leave Type is required")] 
        public int LeaveTypeId { get; set; }
        public MasterLeaveType? MasterLeaveType { get; set; }
      
        [Required(ErrorMessage = "Leave Count is required")]    
        public int LeavesCount { get; set; }
        public string? Status { get; set; }
    }
}
