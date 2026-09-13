using HrmsCoreMvc.Models.Projects;
using Microsoft.EntityFrameworkCore;
using HrmsCoreMvc.Data;

namespace HrmsCoreMvc.Services

{
    public class ProjectService
    {
        private readonly ApplicationDbContext db;

        public ProjectService(ApplicationDbContext cs)
        {
             db = cs;
        }
    }
}
