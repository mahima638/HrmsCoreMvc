using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.Projects
{
    public class TaskMembers
    {
        [Key]   
        public int TaskMembersId { get; set; }
        [ForeignKey("Task")]
        public int TaskId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
