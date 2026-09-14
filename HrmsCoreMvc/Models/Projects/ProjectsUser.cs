using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.Projects
{
    public class ProjectsUser
    {
        [ForeignKey("ProjectsProjectId")]
        public int ProjectsProjectId { get; set; }

        public AllProjects AllProjects { get; set; }

        [ForeignKey("UsersUserId")]
        public int UsersUserId { get; set; }

        public User user { get; set; }
    }
}
