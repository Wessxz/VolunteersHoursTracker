using Microsoft.AspNetCore.Mvc;
using VolunteerHoursTracker.Service.Interfaces;
using VolunteerHoursTracker.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VolunteerHoursTracker.Presentation.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly IVolunteerService _service;

        public VolunteerController(IVolunteerService service)
        {
            _service = service;
        }

        // GET: /Volunteer
        public async Task<IActionResult> Index()
        {
            var volunteers = await _service.GetAllVolunteers();
            return View(volunteers);
        }

        // POST: /Volunteer/Create
        [HttpPost]
        public async Task<IActionResult> Create(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Error = "Name and Email are required.";
                return View("Index", await _service.GetAllVolunteers());
            }

            await _service.AddVolunteer(new Volunteer
            {
                Name = name,
                Email = email,
                VolunteerHours = new List<VolunteerHour>()
            });

            return RedirectToAction(nameof(Index));
        }

        // Optional: Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteVolunteer(id);
            return RedirectToAction(nameof(Index));
        }

        // Optional: Edit (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var volunteer = await _service.GetVolunteerById(id);
            if (volunteer == null) return NotFound();
            return View(volunteer);
        }

        // Optional: Edit (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(Volunteer model)
        {
            if (!ModelState.IsValid) return View(model);
            await _service.UpdateVolunteer(model);
            return RedirectToAction(nameof(Index));
        }

        // ✅ Add GET action for logging hours
        public IActionResult LogHours(int id)
        {
            ViewBag.VolunteerId = id;
            return View();
        }

        // ✅ Add POST action for logging hours
        [HttpPost]
        public async Task<IActionResult> LogHours(int volunteerId, int hours)
        {
            await _service.LogHours(volunteerId, hours);
            return RedirectToAction(nameof(Index));
        }
    }
}
