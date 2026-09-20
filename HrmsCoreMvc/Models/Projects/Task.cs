using System.ComponentModel.DataAnnotations;

namespace HrmsCoreMvc.Models.Projects
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        public AllProjects? Project { get; set; }

        [Required(ErrorMessage = "Task Title is required.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Task Description is required.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Task Status is required.")]
        public string? Status { get; set; }

        [Required(ErrorMessage = "Task Priority is required.")]
        public string? Priority { get; set; }

        [Required(ErrorMessage = "File Path is required.")]
        public string? FilePath { get; set; }
        public int? TaskBoardId { get; set; }
        public TaskBoard? TaskBoard { get; set; }
        public int ProjectId { get; set; }

        public List<TaskMembers>? TaskMembers { get; set; } 
        public DateTime? DueDate { get; set; }
    }
}