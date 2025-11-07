using VolunteerHoursTracker.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VolunteerHoursTracker.Service.Interfaces
{
    public interface IVolunteerService
    {
        Task<List<Volunteer>> GetAllVolunteers();
        Task<Volunteer> GetVolunteerById(int id); // <-- Add this

        Task LogHours(int volunteerId, int hours);
        Task<List<Volunteer>> GetTopVolunteers(int count = 5);

        Task AddVolunteer(Volunteer volunteer);
        Task UpdateVolunteer(Volunteer volunteer);
        Task DeleteVolunteer(int id);
    }
}
