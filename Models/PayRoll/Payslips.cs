using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.PayRoll
{
    public class Payslips
    {
        [Key]
        public int PayslipId { get; set; }

        [Required]

        [ForeignKey("UserId")]
        
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public string Month { get; set; }

        [Required]
        public int Year { get; set; }

        
        public string PayslipPath { get; set; }

        [Required]
        public DateTime GeneratedOn { get; set; }
    }
}
