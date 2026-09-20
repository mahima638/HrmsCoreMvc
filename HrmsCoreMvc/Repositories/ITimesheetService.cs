using HrmsCoreMvc.Models.PayRoll;
using HrmsCoreMvc.Models.Projects;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task = System.Threading.Tasks.Task;

namespace HrmsCoreMvc.Repositories
{
    public interface ITimesheetService
    {
        public Task<List<Timesheet>> GetTimesheets();

        public Task<List<AllProjects>> GetProjects();

        public Task AddTimesheet(Timesheet t);

        Task SendForApproval(List<int> ids);

        Task ApproveSelected(List<int> ids);
        Task RejectSelected(List<int> ids);
    }
}
