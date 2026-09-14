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
        public Task? Task { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
