using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ResourceBookingSystem.Data;
using ResourceBookingSystem.Models;
using System;
using System.Linq;

public static class DatabaseSeeder
{
    public static void SeedDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var db = services.GetRequiredService<ApplicationDbContext>();

            if (!db.Resources.Any())
            {
                db.Resources.AddRange(
                    new Resource
                    {
                        Name = "Meeting Room A",
                        Capacity = 1,
                        Location = "3rd Floor, West Wing",
                        IsAvailable = true,
                        Description = "Large room with projector and whiteboard"
                    },
                    new Resource
                    {
                        Name = "Company Car 1",
                        Capacity = 4,
                        Location = "Parking Bay 5",
                        IsAvailable = true,
                        Description = "Toyota Corolla"
                    },
                    new Resource
                    {
                        Name = "Company Car 2",
                        Capacity = 4,
                        Location = "Parking Bay 3",
                        IsAvailable = true,
                        Description = "Honda Civic"
                    }
                );

                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("DatabaseSeeder");
            logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }
}