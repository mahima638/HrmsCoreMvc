using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Models.Tickets;
using HrmsCoreMvc.Repositories.Tickets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrmsCoreMvc.Controllers.Ticketings
{
    public class TicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ApplicationDbContext _context;
        public TicketController(
            ITicketRepository ticketRepository,
            ApplicationDbContext context)
        {
            _ticketRepository = ticketRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "Admin")
            {
                var tickets = await _ticketRepository.GetTicketsAsync();
                return View(tickets);
            }
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");
            var myTickets = await _ticketRepository.GetMyTicketsAsync(userId.Value);
            return View(myTickets);
        }


        // Add Ticket - GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var users = await _ticketRepository.GetUsersAsync();
            ViewBag.Users = users;
            return View("RaiseTicket");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartWork(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");
            var ticket = await _ticketRepository.GetTicketByIdAsync(id);
            if (ticket == null)
                return NotFound();
            if (ticket.AssignedTo != userId.Value)
                return Forbid();
            if (ticket.Status != "Assigned")
                return RedirectToAction("Index");
            await _ticketRepository.UpdateTicketStatusAsync(id, "In Progress");
            TempData["SuccessMessage"] = "Ticket work started successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Resolve(int id, string solution)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");
            if (string.IsNullOrWhiteSpace(solution))
            {
                TempData["ErrorMessage"] = "Please enter solution.";
                return RedirectToAction("Index");
            }
            var ticket = await _ticketRepository.GetTicketByIdAsync(id);
            if (ticket == null)
                return NotFound();
            if (ticket.AssignedTo != userId.Value)
                return Forbid();
            if (ticket.Status != "In Progress")
                return RedirectToAction("Index");
            var result = await _ticketRepository.ResolveTicketAsync(
                id,
                userId.Value,
                solution);
            TempData["SuccessMessage"] = result;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Close(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");
            var result = await _ticketRepository.CloseTicketAsync(
                id,
                userId.Value);
            TempData["SuccessMessage"] = result;
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reopen(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");
            var result = await _ticketRepository.ReopenTicketAsync(id,userId.Value);
            TempData["SuccessMessage"] = result;
            return RedirectToAction("Index");
        }
        // Add Ticket - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            //var userId = HttpContext.Session.GetInt32("UserId");

            //if (userId == null)
            //{
            //    return Unauthorized("User session not found.");
            //}

            //ticket.RaisedBy = userId.Value;
            ticket.RaisedBy = 2;  //for testing

            await _ticketRepository.AddTicketAsync(ticket);
            TempData["SuccessMessage"] = "Ticket added successfully.";
            return RedirectToAction("Index");
        }
        // Ticket Details
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var tickets = await _ticketRepository.GetTicketsAsync();
            return View(tickets);
        }

        [HttpGet]
        public async Task<IActionResult> ViewDetails(int id)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(id);

            if (ticket == null)
                return NotFound();

            return View(ticket);
        }
    }
}