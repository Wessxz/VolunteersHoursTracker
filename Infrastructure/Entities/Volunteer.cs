using System.Collections.Generic;

namespace VolunteerHoursTracker.Infrastructure.Entities
{
    public class Volunteer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        // Initialize collection to prevent null reference errors
        public ICollection<VolunteerHour> VolunteerHours { get; set; } = new List<VolunteerHour>();
    }
}
