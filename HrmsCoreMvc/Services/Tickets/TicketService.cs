using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Ticketing;
using HrmsCoreMvc.Models.Tickets;
using HrmsCoreMvc.Repositories.Tickets;
using Microsoft.EntityFrameworkCore;



namespace HrmsCoreMvc.Services.Tickets
{
    public class TicketService : ITicketRepository
    {
        private readonly ApplicationDbContext _context;
        public TicketService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Ticket>> GetTicketsAsync()
        {
            return await _context.tickets
                .Include(t => t.RaisedByUser)
                .Include(t => t.AssignedToUser)
                .Include(t => t.AssignedByUser)
                .ToListAsync();
        }
        
        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            return await _context.tickets
                .Include(t => t.RaisedByUser)
                .Include(t => t.AssignedToUser)
                .Include(t => t.AssignedByUser)
                .FirstOrDefaultAsync(t => t.TicketId == id);
        }
        public async Task<string> AddTicketAsync(Ticket ticket)
        {
            var lastTicket = await _context.tickets
                .OrderByDescending(t => t.TicketId)
                .FirstOrDefaultAsync();
            int nextNumber = 10001;
            if (lastTicket != null)
            {
                nextNumber = 10001 + lastTicket.TicketId;
            }
            ticket.TicketNo = "TKT-" + nextNumber;
            ticket.Status = "Open";
            ticket.CreatedDate = DateTime.Now;
            await _context.tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return "Ticket added successfully";
        }
        public async Task<string> AssignTicketAsync(Ticket ticket)
        {
            var existingTicket = await _context.tickets
                .FirstOrDefaultAsync(t => t.TicketId == ticket.TicketId);
            if (existingTicket == null)
                return "Ticket not found";
            existingTicket.AssignedTo = ticket.AssignedTo;
            existingTicket.AssignedBy = ticket.AssignedBy;
            existingTicket.AssignedDate = DateTime.Now;
            existingTicket.Status = "Assigned";
            await _context.SaveChangesAsync();
            return "Ticket assigned successfully";
        }
        public async Task<List<Ticket>> GetAssignedTicketsAsync(int userId)
        {
            return await _context.tickets
                .Include(t => t.RaisedByUser)
                .Include(t => t.AssignedToUser)
                .Include(t => t.AssignedByUser)
                .Where(t => t.AssignedTo == userId)
                .ToListAsync();
        }

        public async Task<string> ResolveTicketAsync(int id, int resolvedBy, string solution)
        {
            var ticket = await _context.tickets
                .FirstOrDefaultAsync(t => t.TicketId == id);
            if (ticket == null)
                return "Ticket not found";
            if (ticket.AssignedTo != resolvedBy)
                return "You are not assigned to this ticket";
            if (ticket.Status != "In Progress")
                return "Ticket is not in progress";
            var resolution = new TicketResolution
            {
                TicketId = id,
                ResolvedBy = resolvedBy,
                Solution = solution,
                ResolvedDate = DateTime.Now
            };
            await _context.Set<TicketResolution>().AddAsync(resolution);
            ticket.Status = "Resolved";
            ticket.ResolvedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return "Ticket resolved successfully";
        }

        public async Task<List<Ticket>> GetMyTicketsAsync(int userId)
        {
            return await _context.tickets
                .Include(t => t.RaisedByUser)
                .Include(t => t.AssignedToUser)
                .Include(t => t.AssignedByUser)
                .Include(t => t.Resolution)
                .Where(t => t.RaisedBy == userId || t.AssignedTo == userId)
                .ToListAsync();
        }
        public async Task<string> CloseTicketAsync(int id, int userId)
        {
            var ticket = await _context.tickets
                .FirstOrDefaultAsync(t => t.TicketId == id);
            if (ticket == null)
                return "Ticket not found";
            if (ticket.RaisedBy != userId)
                return "Only ticket creator can close the ticket";
            if (ticket.Status != "Resolved")
                return "Ticket is not resolved";
            ticket.Status = "Closed";
            ticket.ClosedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return "Ticket closed successfully";
        }

        public async Task<string> ReopenTicketAsync(int id, int userId)
        {
            var ticket = await _context.tickets
                .FirstOrDefaultAsync(t => t.TicketId == id);
            if (ticket == null)
                return "Ticket not found";
            if (ticket.RaisedBy != userId)
                return "Only ticket creator can reopen the ticket";
            if (ticket.Status != "Resolved")
                return "Ticket is not resolved";
            ticket.Status = "Reopened";
            await _context.SaveChangesAsync();
            return "Ticket reopened successfully";
        }

        public async Task<string> RejectTicketAsync(int id, int rejectedBy, string comment)
        {
            var ticket = await _context.tickets.FirstOrDefaultAsync(t => t.TicketId == id);
            if (ticket == null)
                return "Ticket not found";
            var oldStatus = ticket.Status;
            ticket.Status = "Rejected";
            await _context.Set<TicketComment>().AddAsync(new TicketComment
            {
                TicketId = id,
                CommentBy = rejectedBy,
                CommentText = comment
            });
            await _context.Set<TicketHistory>().AddAsync(new TicketHistory
            {
                TicketId = id,
                ChangedBy = rejectedBy,
                OldStatus = oldStatus,
                NewStatus = "Rejected"
            });
            await _context.SaveChangesAsync();
            return "Ticket rejected successfully";
        }
        public async Task<string> UpdateTicketStatusAsync(int id, string status)
        {
            var ticket = await _context.tickets
                .FirstOrDefaultAsync(t => t.TicketId == id);
            if (ticket != null)
            {
                ticket.Status = status;
                if (status == "In Progress")
                    ticket.StartedDate = DateTime.Now;
                if (status == "Resolved")
                    ticket.ResolvedDate = DateTime.Now;
                if (status == "Closed")
                    ticket.ClosedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return "Status updated successfully";
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.user.ToListAsync();
        }
       

    }
}
