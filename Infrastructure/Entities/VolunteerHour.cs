using System;

namespace VolunteerHoursTracker.Infrastructure.Entities
{
    public class VolunteerHour
    {
        public int Id { get; set; }
        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
    }
}
