using VolunteerHoursTracker.Infrastructure;
using VolunteerHoursTracker.Infrastructure.Entities;
using VolunteerHoursTracker.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VolunteerHoursTracker.Service.Implementations
{
    public class VolunteerService : IVolunteerService
    {
        private readonly AppDbContext _context;

        public VolunteerService(AppDbContext context)
        {
            _context = context;
        }

        // Get all volunteers with their hours
        public async Task<List<Volunteer>> GetAllVolunteers()
        {
            return await _context.Volunteers
                                 .Include(v => v.VolunteerHours)
                                 .ToListAsync();
        }

        // Get volunteer by ID
        public async Task<Volunteer> GetVolunteerById(int id)
        {
            return await _context.Volunteers
                                 .Include(v => v.VolunteerHours)
                                 .FirstOrDefaultAsync(v => v.Id == id);
        }

        // Add a new volunteer
        public async Task AddVolunteer(Volunteer volunteer)
        {
            _context.Volunteers.Add(volunteer);
            await _context.SaveChangesAsync();
        }

        // Update existing volunteer
        public async Task UpdateVolunteer(Volunteer volunteer)
        {
            var existing = await _context.Volunteers.FindAsync(volunteer.Id);
            if (existing != null)
            {
                existing.Name = volunteer.Name;
                existing.Email = volunteer.Email;
                await _context.SaveChangesAsync();
            }
        }

        // Delete volunteer
        public async Task DeleteVolunteer(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer != null)
            {
                _context.Volunteers.Remove(volunteer);
                await _context.SaveChangesAsync();
            }
        }

        // Log hours for a volunteer
        public async Task LogHours(int volunteerId, int hours)
        {
            var vh = new VolunteerHour
            {
                VolunteerId = volunteerId,
                Hours = hours,
                Date = System.DateTime.Now
            };
            _context.VolunteerHours.Add(vh);
            await _context.SaveChangesAsync();
        }

        // Get top volunteers by total hours
        public async Task<List<Volunteer>> GetTopVolunteers(int count = 5)
        {
            return await _context.Volunteers
                                 .Include(v => v.VolunteerHours)
                                 .OrderByDescending(v => v.VolunteerHours.Sum(h => h.Hours))
                                 .Take(count)
                                 .ToListAsync();
        }
    }
}
