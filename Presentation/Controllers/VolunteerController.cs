using Microsoft.AspNetCore.Mvc;
using VolunteerHoursTracker.Service.Interfaces;
using VolunteerHoursTracker.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Create(string name, string email, int age, int hours)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Error = "Name and Email are required.";
                return View("Index", await _service.GetAllVolunteers());
            }

            var volunteer = new Volunteer
            {
                Name = name,
                Email = email,
                Age = age,
                VolunteerHours = new List<VolunteerHour>()
            };

            if (hours > 0)
            {
                volunteer.VolunteerHours.Add(new VolunteerHour
                {
                    Hours = hours,
                    Date = System.DateTime.Now
                });
            }

            await _service.AddVolunteer(volunteer);

            return RedirectToAction(nameof(Index));
        }

        // Optional: Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteVolunteer(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Edit Volunteer
        public async Task<IActionResult> Edit(int id)
        {
            var volunteer = await _service.GetVolunteerById(id);
            if (volunteer == null) return NotFound();
            return View(volunteer);
        }

        // POST: Edit Volunteer
        [HttpPost]
        public async Task<IActionResult> Edit(Volunteer model, int hours)
        {
            if (!ModelState.IsValid) return View(model);

            // Replace VolunteerHours with new total
            model.VolunteerHours = new List<VolunteerHour>
            {
                new VolunteerHour
                {
                    Hours = hours,
                    Date = System.DateTime.Now
                }
            };

            await _service.UpdateVolunteer(model);

            return RedirectToAction(nameof(Index));
        }

        // GET: Log Hours
        public IActionResult LogHours(int id)
        {
            ViewBag.VolunteerId = id;
            return View();
        }

        // POST: Log Hours
        [HttpPost]
        public async Task<IActionResult> LogHours(int volunteerId, int hours)
        {
            await _service.LogHours(volunteerId, hours);
            return RedirectToAction(nameof(Index));
        }
    }
}
