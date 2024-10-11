using EventRegistrationSystem.Data;
using EventRegistrationSystem.Models;
using EventRegistrationSystem.Repositories.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EventRegistrationSystem.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly EventSystemDbContext _context;
        private readonly EmailService _emailService;

        public RegistrationsController(EventSystemDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<IActionResult> Register(int eventId)
        {
            var eventItem = await _context.Events.FindAsync(eventId);
            if (eventItem == null)
            {
                return NotFound();
            }

            var registration = new Registration { EventId = eventId };
            ViewBag.EventTitle = eventItem.Title;
            return View(registration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Registrations.Add(registration);
                await _context.SaveChangesAsync();

                // Trigger email notification
                var eventItem = await _context.Events.FindAsync(registration.EventId);
                await _emailService.SendConfirmationEmailAsync(registration.Email, registration.ParticipantName, eventItem.Title);

                return RedirectToAction("Index", "Home");
            }
            var eventTitle = (await _context.Events.FindAsync(registration.EventId))?.Title;
            ViewBag.EventTitle = eventTitle;
            return View(registration);
        }
    }
}
