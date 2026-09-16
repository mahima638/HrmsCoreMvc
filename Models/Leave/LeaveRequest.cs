using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.Leave
{
    public class LeaveRequest
    {
        [Key]
        public int LeaveRequestId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("LeaveTypeId")]
        public int LeaveTypeId { get; set; }
        public MasterLeaveType MasterLeaveType { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int NumberOfDays { get; set; }

        public string Reason { get; set; }

        public string ApprovedBy { get; set; }

        public string Status { get; set; }
        public string StatusHistory { get; set; }
    }
}
