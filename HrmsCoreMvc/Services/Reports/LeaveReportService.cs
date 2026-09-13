using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models.Attendance;
using HrmsCoreMvc.Models.Reports;
using HrmsCoreMvc.Repositories.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Services.Reports
{
    public class LeaveReportService : ILeaveReports
    {
        private readonly ApplicationDbContext db;

        public LeaveReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<int> fetchApprovedLeaves()
        {
            var data = await db.leaverequests.Where(lr => lr.Status == "Approved").SumAsync(lr => lr.NumberOfDays);
            return data;
        }

        public async Task<IEnumerable<LeavesReportViewModel>> fetchLeaves()
        {
            return await db.leaverequests.Include(lr => lr.MasterLeaveType).Include(lr => lr.User).Select(lr => new LeavesReportViewModel
            {
                attendanceId = lr.LeaveRequestId,
                userName = lr.User.FirstName + " " + lr.User.LastName,
                leaveType = lr.MasterLeaveType.LeaveType,
                profilephoto = lr.User.ProfilePicture,
                StartDate = lr.StartDate,
                EndDate = lr.EndDate,
                Days = lr.NumberOfDays,
                Reason = lr.Reason,
                ApprovedBy = lr.ApprovedBy,
                Status = lr.Status,
                StatusHistory = lr.StatusHistory,
            }).ToListAsync();
        }

        public async Task<int> fetchPendingLeaves()
        {
            var data = await db.leaverequests.Where(lr => lr.Status == "Pending").SumAsync(lr => lr.NumberOfDays);
            return data;
        }

        public async Task<int> fetchRejectedLeaves()
        {
            var data = await db.leaverequests.Where(lr => lr.Status == "Rejected").SumAsync(lr => lr.NumberOfDays);
            return data;
        }
        public async Task<int> fetchTotalLeaves()
        {
            var data = await db.leavebalances.SumAsync(lb=> lb.TotalLeaves);
            return data;

        }

        public async Task<IEnumerable<LeavesReportViewModel>> sortLeaves(string? datefilter, string? statusfilter, string? sortType)
        {
            var query = db.leaverequests.Include(lr => lr.User).Include(lr => lr.MasterLeaveType).AsQueryable();

            if (!string.IsNullOrEmpty(statusfilter))
            {
                query = query.Where(lr => lr.Status == statusfilter);
            }

            if (sortType == "Ascending")
            {
                query = query.OrderBy(lr => lr.StartDate);
            }
            else if (sortType == "Descending")
            {
                query = query.OrderByDescending(lr => lr.StartDate);

            }
            else if (sortType == "Last 7 days")
            {
                query = query.Where(lr => lr.StartDate >= DateTime.Now.AddDays(-7));
            }
            else if (sortType == "Last Month")
            {
                query = query.Where(lr => lr.StartDate >= DateTime.Now.AddMonths(-1));
            }
            else if (sortType == "Recently Added")
            {
                query = query.Where(lr => lr.StartDate >= DateTime.Now.AddDays(-1));

            }
            if (datefilter == "Last 7 Days")
            {
                query = query.Where(lr => lr.StartDate >= DateTime.Now.AddDays(-7));
            }
            else if (datefilter == "Last 30 Days")
            {
                query = query.Where(lr => lr.StartDate >= DateTime.Now.AddDays(-30));
            }
            else if (datefilter == "This Year")
            {
                query = query.Where(lr => lr.StartDate.Year == DateTime.Now.Year);
            }
            else if (datefilter == "Last Year")
            {
                query = query.Where(lr => lr.StartDate >= DateTime.Now.AddYears(-1));
            }
            else if (datefilter == "Yesterday")
            {
                query = query.Where(lr => lr.StartDate >= DateTime.Now.AddDays(-1));
            }

            var result = await query.Select(lr => new LeavesReportViewModel
            {
                attendanceId = lr.LeaveRequestId,
                userName = lr.User.FirstName + " " + lr.User.LastName,
                leaveType = lr.MasterLeaveType.LeaveType,
                profilephoto = lr.User.ProfilePicture,
                StartDate = lr.StartDate,
                EndDate = lr.EndDate,
                Days = lr.NumberOfDays,
                Reason = lr.Reason,
                ApprovedBy = lr.ApprovedBy,
                Status = lr.Status,
                StatusHistory = lr.StatusHistory,

            }).ToListAsync();

            return result;

        }
    }
}
