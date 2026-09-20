using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Tickets;

namespace HrmsCoreMvc.Repositories.Tickets
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetTicketsAsync();
        Task<Ticket?> GetTicketByIdAsync(int id);
        Task<string> AddTicketAsync(Ticket ticket);
        Task<string> AssignTicketAsync(Ticket ticket);
        Task<string> UpdateTicketStatusAsync(int id, string status);
        Task<List<User>> GetUsersAsync();
        Task<List<Ticket>> GetAssignedTicketsAsync(int userId);
        Task<string> ResolveTicketAsync(int id, int resolvedBy, string solution);
        Task<List<Ticket>> GetMyTicketsAsync(int userId);
        Task<string> CloseTicketAsync(int id, int userId);
        Task<string> ReopenTicketAsync(int id, int userId);
        Task<string> RejectTicketAsync(int id, int rejectedBy, string comment);
    }
}