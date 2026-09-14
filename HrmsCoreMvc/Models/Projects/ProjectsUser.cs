using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrmsCoreMvc.Models.Projects
{
    public class ProjectsUser
    {
        [Key]
        public int ProjectsProjectId { get; set; }

        [ForeignKey("UsersUserId")]
        public int UsersUserId { get; set; }

        public List<User> projectusers { get; set; }
    }
}
