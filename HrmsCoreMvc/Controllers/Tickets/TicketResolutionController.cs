using HrmsCoreMvc.Repositories.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreMvc.Controllers.Ticketings
{
    public class TicketResolutionController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketResolutionController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            await _ticketRepository.UpdateTicketStatusAsync(id, status);
            TempData["SuccessMessage"] = "Ticket status updated successfully.";
            return RedirectToAction("Index", "Ticket");
        }
    }
}