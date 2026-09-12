using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.PayRoll
{
    public class Earning
    {
        [Key]
        public int EarningsId { get; set; }

        
        [ForeignKey("EarntypeId")]
        public int EarntypeId { get; set; }
        public EarningType EarningType { get; set; }

        public decimal EarningsPercentage { get; set; }

       
        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
        public Departments Department { get; set; }

        
        [ForeignKey("DesignationId")]
        public int DesignationId { get; set; }
        public Designations Designation { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; } 
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
