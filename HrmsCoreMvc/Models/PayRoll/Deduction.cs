using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.PayRoll
{
    public class Deduction
    {
        [Key]
        public int DeductionId { get; set; } 

        [ForeignKey("DeductionTypeId")]
        public int DeductionTypeId { get; set; }

        [ForeignKey("DepartmentId")]

        public int DepartmentId { get; set; }

        [ForeignKey("DesignationId")]
        public int DesignationId { get; set; } 

        [Required]
        public decimal DeductionPercentage { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; } 

        public DateTime? ModifiedAt { get; set; }

        public string? ModifiedBy { get; set; } 

        
        public  DeductionType DeductionType { get; set; }
        public  Departments Department { get; set; }
        public  Designations Designation { get; set; }
    }
}
