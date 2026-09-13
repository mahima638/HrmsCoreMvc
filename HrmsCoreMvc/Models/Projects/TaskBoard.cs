using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.Projects
{
    public class TaskBoard
    {
        [Key]
        public int TaskBoardId { get; set; }
        [ForeignKey("Task")]
        public int TaskId { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "Task Name is required.")]
        public AllProjects? Project { get; set; }
        public string? TaskName { get; set; }
        public decimal Percentage { get; set; }
        [Required(ErrorMessage = "Due Date is required.")]
        public DateTime Duedate { get; set; }
    }
}
