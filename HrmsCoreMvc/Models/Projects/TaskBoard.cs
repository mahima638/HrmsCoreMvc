using System.ComponentModel.DataAnnotations;

namespace HrmsCoreMvc.Models.Projects
{
    public class TaskBoard
    {
        [Key]
        public int TaskBoardId { get; set; }
        [Required(ErrorMessage = "Task Board Name is required.")]
        public string? TaskBoardName { get; set; }
        public decimal Percentage { get; set; }
        [Required(ErrorMessage = "Due Date is required.")]
        public DateTime Duedate { get; set; }
        public int ProjectId { get; set; }
        public AllProjects? Project { get; set; }
        public List<Task>? Tasks { get; set; } 
    }
}