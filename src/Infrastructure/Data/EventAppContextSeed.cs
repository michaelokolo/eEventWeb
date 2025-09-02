using ApplicationCore.Entities.EventAggregate;
using ApplicationCore.Entities.OrganizerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class EventAppContextSeed
{
    public static async Task SeedAsync(EventAppContext eventAppContext, ILogger logger, int retry = 0)
    {
        var retryForAvailability = retry;

        try
        {
            if (eventAppContext.Database.IsSqlServer())
            {
                eventAppContext.Database.Migrate();
            }

            // Seed Organizers if not present
            if (!await eventAppContext.Organizers.AnyAsync())
            {
                var organizers = new List<Organizer>
                {
                    new Organizer("guid1", "Organizer One"),
                    new Organizer("guid2", "Organizer Two"),
                    new Organizer("guid3", "Organizer Three")
                };

                await eventAppContext.Organizers.AddRangeAsync(organizers);
                await eventAppContext.SaveChangesAsync();
            }

            // Retrieve organizers from DB to get their actual IDs
            var organizerList = await eventAppContext.Organizers.ToListAsync();
            var organizer1 = organizerList.FirstOrDefault(o => o.IdentityGuid == "guid1");
            var organizer2 = organizerList.FirstOrDefault(o => o.IdentityGuid == "guid2");
            var organizer3 = organizerList.FirstOrDefault(o => o.IdentityGuid == "guid3");

            // Seed Events if not present
            if (!await eventAppContext.Events.AnyAsync())
            {
                var events = new List<Event>
                {
                    new Event("Event 1", "Description for Event 1", DateTime.Now.AddDays(10), organizer1!.Id),
                    new Event("Event 2", "Description for Event 2", DateTime.Now.AddDays(20), organizer2!.Id),
                    new Event("Event 3", "Description for Event 3", DateTime.Now.AddDays(30), organizer1!.Id),
                    new Event("Event 4", "Description for Event 4", DateTime.Now.AddDays(40), organizer3!.Id),
                    new Event("Event 5", "Description for Event 5", DateTime.Now.AddDays(50), organizer2!.Id),
                    new Event("Event 6", "Description for Event 6", DateTime.Now.AddDays(60), organizer3!.Id)
                };

                await eventAppContext.Events.AddRangeAsync(events);
                await eventAppContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;

            logger.LogError(ex.Message);
            await SeedAsync(eventAppContext, logger, retryForAvailability);
            throw;
        }
    }
}