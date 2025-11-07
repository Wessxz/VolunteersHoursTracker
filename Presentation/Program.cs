using Microsoft.EntityFrameworkCore;
using VolunteerHoursTracker.Infrastructure;
using VolunteerHoursTracker.Infrastructure.Entities;
using VolunteerHoursTracker.Service.Implementations;
using VolunteerHoursTracker.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args); // 1️⃣

// Register services
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("VolunteerDB"));
builder.Services.AddScoped<IVolunteerService, VolunteerService>();

// Build the app
var app = builder.Build(); // 2️⃣

// Seed database (use 'app', NOT 'builder')
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Volunteers.Any())
    {
        db.Volunteers.AddRange(
            new Volunteer
            {
                Name = "Juan",
                Email = "juan@example.com",
                VolunteerHours = new List<VolunteerHour>
                {
                    new VolunteerHour { Hours = 5 },
                    new VolunteerHour { Hours = 3 }
                }
            },
            new Volunteer
            {
                Name = "Maria",
                Email = "maria@example.com",
                VolunteerHours = new List<VolunteerHour>
                {
                    new VolunteerHour { Hours = 8 }
                }
            }
        );

        db.SaveChanges();
    }
}

// Configure middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Map routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Volunteer}/{action=Index}/{id?}");

// Run
app.Run(); // 3️⃣
