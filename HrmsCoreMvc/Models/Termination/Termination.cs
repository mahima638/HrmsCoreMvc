using HrmsCoreMvc.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.Termination
{
    public class Termination
    {
        [Key]
        public int TId { get; set; }

        [ForeignKey("User")]
        [Required]
        [Display(Name = "Employee Name")]
        public int UserId{ get; set; }
        public User User { get; set; }

        [Required]
        [Display(Name = "Termination Type")]
        public string TerminationType { get; set; }

        [Required]
        [Display(Name = "Notice Date")]
        public DateTime NoticeDate { get; set; }

        [Required]
        [Display(Name = "Resignation Date")]
        public DateTime ResignDate { get; set; }

        [Required]
        [Display(Name = "Reason")]
        public string Reason { get; set; }
    }
}

