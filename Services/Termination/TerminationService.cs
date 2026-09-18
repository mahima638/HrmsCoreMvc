using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Termination;
using HrmsCoreMvc.Repositories.Terminations;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Services.Terminations
{
    public class TerminationService : ITerminationRepository
    {
        private readonly ApplicationDbContext _context;
        public TerminationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Termination>> GetTerminationsAsync()
        {
            return await _context.terminations.Include(t => t.User).ToListAsync();
        }

        public async Task<Termination> GetTerminationByIdAsync(int terminationId)
        {
            return await _context.terminations.Include(t => t.User).FirstOrDefaultAsync(t => t.TId == terminationId);
        }

        public async Task<string> AddTerminationAsync(Termination terminations)
        {
            await _context.terminations.AddAsync(terminations);
            await _context.SaveChangesAsync();
            return "data Added Succesdully";
        }

        public async Task<string> UpdateTerminationAsync(Termination terminations)
        {
            _context.terminations.Update(terminations);
            await _context.SaveChangesAsync();
            return "Data Updated Successfully";
        }

        public async Task<string> DeleteTerminationAsync(int terminationId)
        {
            var termination= await _context.terminations.FirstOrDefaultAsync(t => t.TId == terminationId);
            if(termination !=null)
            {
                _context.terminations.Remove(termination);
                await _context.SaveChangesAsync();
            }
            return "Data deleted Successfully";
        }

        

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.user.ToListAsync();
        }
    }
}
