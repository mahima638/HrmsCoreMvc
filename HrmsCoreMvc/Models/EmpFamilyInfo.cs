using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models
{
    public class EmpFamilyInfo
    {
        [Key]
        public int FamilyDetailId { get; set; }
        public string ? Name{ get; set; }
        public string ?  Relation{ get; set; }

        public  DateTime ? DateOfBirth{ get; set; }

        public string? phone { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
