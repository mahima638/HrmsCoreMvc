using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.Projects
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "Task Title is required.")]
        public AllProjects? Project { get; set; }
        public string? Title { get; set; }
        [Required(ErrorMessage = "Task Description is required.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Task Status is required.")]
        public string? Status { get; set; }
        [Required(ErrorMessage = "Task Priority is required.")]
        public string? Priority { get; set; }
        [Required(ErrorMessage = "File Path is required.")]
        public string? FilePath { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}
