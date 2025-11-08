using System;

namespace VolunteerHoursTracker.Infrastructure.Entities
{
    public class VolunteerHour
    {
        public int Id { get; set; }
        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public DateTime Date { get; set; } = DateTime.Now; // Default to now
        public int Hours { get; set; }
    }
}
