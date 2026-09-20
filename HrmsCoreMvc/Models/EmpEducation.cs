using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models
{
    public class EmpEducation
    {
        [Key]
        public int EducationDetailsId { get; set; }
        public string ? EducationType{ get; set; }

        public string ? UniversityName { get; set; }

        public int ? UserId { get; set; }

        [ForeignKey("UserId")]
        public User?  User { get; set; }

        public DateTime ?  startdate { get; set; }

        public DateTime ?  enddate { get; set; }
    }
}
