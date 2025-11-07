using Microsoft.EntityFrameworkCore;
using VolunteerHoursTracker.Infrastructure.Entities;

namespace VolunteerHoursTracker.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<VolunteerHour> VolunteerHours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optional: seed some sample data
            modelBuilder.Entity<Volunteer>().HasData(
                new Volunteer { Id = 1, Name = "Alice", Email = "alice@test.com" },
                new Volunteer { Id = 2, Name = "Bob", Email = "bob@test.com" }
            );
        }
    }
}
