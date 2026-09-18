using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.Leave
{
    public class LeaveBalance
    {
        [Key]
        public int LeaveBalanceId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("DepartmentLeavesId")]
        public int DepartmentLeavesId { get; set; }
        public DepartmentLeaves DepartmentLeaves { get; set; }

        [ForeignKey("LeaveTypeId")]
        public int LeaveTypeId { get; set; }
        public MasterLeaveType MasterLeaveType { get; set; }

        public int TotalLeaves { get; set; }

        public int UsedLeaves { get; set; }

        
    }
}
