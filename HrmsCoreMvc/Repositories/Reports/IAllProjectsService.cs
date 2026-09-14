namespace HrmsCoreMvc.Repositories.Reports
{
    public interface IAllProjectsService
    {
        Task<int> fetchAllProjects();

        Task<int> fetchOnHoldProjects();

        Task<int> fetchOverdueProjects();


    }
}
