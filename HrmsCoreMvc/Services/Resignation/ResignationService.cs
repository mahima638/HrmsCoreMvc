using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Resignation;
using HrmsCoreMvc.Repositories.Resignations;
using Microsoft.EntityFrameworkCore;


namespace HrmsCoreMvc.Services.Resignations
{
    public class ResignationService : IResignationRepository
    {
        private readonly ApplicationDbContext _context;
        public ResignationService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<Resignation>> GetResignationsAsync()
        {
            return await _context.resignations
                .Include(r => r.User)
                .Include(r => r.Departments).ToListAsync();
        }

        public async Task<Resignation?> GetResignationByIdAsync(int resignationId)
        {
            return await _context.resignations
                .Include(r => r.User)
                .Include(r => r.Departments)
                .FirstOrDefaultAsync(r => r.RId == resignationId);
        }

        public async Task<string> AddResignationAsync(Resignation resignations)
        {
            await _context.resignations.AddAsync(resignations);
            await _context.SaveChangesAsync();
            return "Added Successfully";
        }

        public async Task<string> UpdateResignationAsync(Resignation resignation)
        {
            _context.resignations.Update(resignation);
            await _context.SaveChangesAsync();
            return "Data Updated Successfully";
        }


        public async Task<string> DeleteResignationAsync(int resignationId)
        {
            var resignation = await _context.resignations.FirstOrDefaultAsync(r => r.RId == resignationId);
            if (resignation != null)
            {
                _context.resignations.Remove(resignation);
                await _context.SaveChangesAsync();
            }
            return "Data deleted Successfully";
        }


        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.user.ToListAsync();
        }

        public async Task<List<Departments>> GetDepartmentAsync()
        {
            return await _context.department.ToListAsync();
        }
    }
}
