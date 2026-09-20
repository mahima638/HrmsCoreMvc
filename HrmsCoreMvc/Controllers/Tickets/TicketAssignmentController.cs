using HrmsCoreMvc.Models.Tickets;
using HrmsCoreMvc.Repositories.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers.Ticketings
{
    public class TicketAssignmentController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketAssignmentController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketRepository.GetTicketsAsync();
            return View(tickets.Where(t => t.Status == "Open").ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Assign(int id)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(id);
            if (ticket == null)
                return NotFound();
            ViewBag.Users = await _ticketRepository.GetUsersAsync();
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(Ticket ticket)
        {
            var managerId = HttpContext.Session.GetInt32("UserId");
            if (managerId == null)
                return RedirectToAction("Login", "Account");
            ticket.AssignedBy = managerId.Value;
            await _ticketRepository.AssignTicketAsync(ticket);
            TempData["SuccessMessage"] = "Ticket assigned successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int TicketId, string AssignmentComment)
        {
            var managerId = HttpContext.Session.GetInt32("UserId");
            if (managerId == null)
                return RedirectToAction("Login", "Account");
            if (string.IsNullOrWhiteSpace(AssignmentComment))
            {
                TempData["ErrorMessage"] = "Please enter rejection comment.";
                return RedirectToAction("Assign", new { id = TicketId });
            }
            var result = await _ticketRepository.RejectTicketAsync(
                TicketId,
                managerId.Value,
                AssignmentComment);
            TempData["SuccessMessage"] = result;
            return RedirectToAction("Index");
        }
    }
}