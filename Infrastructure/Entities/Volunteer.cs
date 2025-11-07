using System.Collections.Generic;

namespace VolunteerHoursTracker.Infrastructure.Entities
{
    public class Volunteer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<VolunteerHour> VolunteerHours { get; set; }
    }
}
