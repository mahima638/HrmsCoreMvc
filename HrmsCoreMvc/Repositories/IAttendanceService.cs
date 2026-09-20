using HrmsCoreMvc.Models.Attendance;
using HrmsCoreMvc.Models.ViewModel;

namespace HrmsCoreMvc.Repositories
{
    public interface IAttendanceService
    {
        public Task<Attendance?> FetchAttendance(int userId);
        public Task<AttendanceViewModel> GetDashboard(int userId);

        public Task CheckIn(int userId);

        public Task LunchIn(int userId);

        public Task LunchOut(int userId);

        public Task CheckOut(int userId);
    }
}
