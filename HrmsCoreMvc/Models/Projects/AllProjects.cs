using System.ComponentModel.DataAnnotations;

namespace HrmsCoreMvc.Models.Projects
{
    public class AllProjects
    {
        [Key]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Project Name is required.")]
        public string? ProjectName { get; set; }

        [Required(ErrorMessage = "Client Name is required.")]
        public string? ClientName { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Priority is required.")]  
        public string? Priority { get; set; }

        [Required(ErrorMessage = "Project Value is required.")]
        public double ProjectValue { get; set; }

        [Required(ErrorMessage = "Price Type is required.")]
        public string? PriceType { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string? Status { get; set; }

        [Required(ErrorMessage = "Manager Name is required.")]
        public string? ManagerName { get; set; }

        [Required(ErrorMessage = "Logo Path is required.")]
        public string? LogoPath { get; set; }

        [Required(ErrorMessage = "File Path is required.")]
        public string? FilePath { get; set; }

        public List<ProjectsUser> projectusers { get; set; }
        
    }
}
