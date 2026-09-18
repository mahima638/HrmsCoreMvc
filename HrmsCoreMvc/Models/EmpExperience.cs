using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models
{
    public class EmpExperience
    {
        [Key]
        public int ExperienceId { get; set; }
        public string ?  DesignationName { get; set; }

        public DateTime ?  FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User ?  User { get; set; }

        public string ?  CompanyName { get; set; }
    }
}
