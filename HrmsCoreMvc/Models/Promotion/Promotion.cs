using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.Promotion
{
    public class Promotion
    {
        [Key]
        [Display(Name = "Promotion ID")]
        public int PId { get; set; }


        [ForeignKey("User")]
        [Required]
        [Display(Name = "Employee Name")]
        public int UserId { get; set; }
        public User? User { get; set; }


        [Display(Name = "Designation From")]     
        [Required]
        public string? DesignationFrom{ get; set; }


        [Display(Name = "Designation To")]
        [Required]
        public string? DesignationTo { get; set; }



        [Required]
        [Display(Name = "Promotion Date")]
        public DateTime Date { get; set; }

    }
}
