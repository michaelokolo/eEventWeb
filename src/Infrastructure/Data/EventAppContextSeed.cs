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
                    new Event("Tech Innovators Summit", "A conference showcasing the latest breakthroughs in AI, robotics, and software development.", DateTime.Now.AddDays(10), "/images/events/event1.jpg", organizer1!.Id),
                    new Event("Culinary Fest 2025", "A food festival celebrating local and international cuisines with live cooking demonstrations.", DateTime.Now.AddDays(20), "/images/events/event2.jpg", organizer2!.Id),
                    new Event("Green Future Expo", "An exhibition focusing on sustainable living, renewable energy, and eco-friendly innovations.", DateTime.Now.AddDays(30), "/images/events/event3.jpg", organizer1!.Id),
                    new Event("Global Startup Pitch", "Entrepreneurs from around the world pitch their innovative business ideas to investors.", DateTime.Now.AddDays(40), "/images/events/event4.jpg", organizer3!.Id),
                    new Event("Music & Arts Carnival", "A cultural festival featuring live bands, art exhibitions, and street performances.", DateTime.Now.AddDays(50), "/images/events/event5.jpg", organizer2!.Id),
                    new Event("Health & Wellness Fair", "Workshops and seminars on fitness, nutrition, and mental health awareness.", DateTime.Now.AddDays(60), "/images/events/event6.jpg", organizer3!.Id),
                    new Event("Space Exploration Talk", "A keynote session by leading scientists discussing the future of space travel.", DateTime.Now.AddDays(70), "/images/events/event7.jpg", organizer1!.Id)

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