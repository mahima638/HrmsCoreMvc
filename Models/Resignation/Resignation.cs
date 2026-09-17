using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.Resignation
{
    public class Resignation
    {
        [Key]        
        public int RId{ get; set; }


        [ForeignKey("User")]
        [Required]
        [Display(Name = "Resigning Employee")]
        public int UserId { get; set; }
        public User? User { get; set; }



        [ForeignKey("Departments")]
        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public Departments? Departments { get; set; }



        [Required]
        public string?  Reason{ get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime NoticeDate { get; set; }



        [Required]
        [DataType(DataType.Date)]
        public DateTime ResignDate { get; set; }

    }
}
