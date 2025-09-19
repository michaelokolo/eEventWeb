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
                    new Organizer("guid1", "Walking Friends"),
                    new Organizer("guid2", "Manchester Creators"),
                    new Organizer("guid3", "Art & Lens Society")
                };

                await eventAppContext.Organizers.AddRangeAsync(organizers);
                await eventAppContext.SaveChangesAsync();
            }

            // Retrieve organizers from DB to get their actual IDs
            var organizerList = await eventAppContext.Organizers.ToListAsync();
            var organizer1 = organizerList.FirstOrDefault(o => o.IdentityGuid == "guid1");
            var organizer2 = organizerList.FirstOrDefault(o => o.IdentityGuid == "guid2");
            var organizer3 = organizerList.FirstOrDefault(o => o.IdentityGuid == "guid3");


            var requirements1 = new List<Requirement> {
                    new Requirement("Capture high-quality images of the event"),
                    new Requirement("Edit and deliver photos within a week after the event"),
                    new Requirement("Coordinate with event organizers for specific shots"),
                    new Requirement("Provide your own photography equipment"),
                    new Requirement("Experience in event photography is a plus")
            };
            var roleInfo1 = new EventRoleInfo("Photographer", "Derby", requirements1, 100);

            var requirements2 = new List<Requirement> {
                    new Requirement("Film key moments throughout the event"),
                    new Requirement("Edit and deliver a highlight video within two weeks"),
                    new Requirement("Work closely with the photographer for consistent coverage"),
                    new Requirement("Bring and manage your own video equipment"),
                    new Requirement("Experience in event videography preferred")
            };
            var roleInfo2 = new EventRoleInfo("Videographer", "Nottingham", requirements2, 150);

            var requirements3 = new List<Requirement> {
                    new Requirement("Assist attendees with seating and directions"),
                    new Requirement("Manage entry and exit points efficiently"),
                    new Requirement("Provide excellent customer service"),
                    new Requirement("Handle any emergencies calmly and effectively"),
                    new Requirement("Previous experience in event assistance is beneficial")
                };
            var roleInfo3 = new EventRoleInfo("Event Usher", "Leicester", requirements3, 50);


            var requirements4 = new List<Requirement> {
                    new Requirement("Set up and manage sound equipment"),
                    new Requirement("Conduct sound checks before the event"),
                    new Requirement("Monitor audio levels during the event"),
                    new Requirement("Troubleshoot technical sound issues quickly"),
                    new Requirement("Experience with live audio systems is required")
                };
            var roleInfo4 = new EventRoleInfo("Sound Technician", "Birmingham", requirements4, 120);


            var requirements5 = new List<Requirement> {
                    new Requirement("Prepare and deliver meals for attendees"),
                    new Requirement("Set up catering tables and serving areas"),
                    new Requirement("Ensure food safety and hygiene standards"),
                    new Requirement("Provide a variety of dietary options"),
                    new Requirement("Clear up after the meal service")
                };
            var roleInfo5 = new EventRoleInfo("Caterer", "Manchester", requirements5, 200);

            var requirements6 = new List<Requirement> {
                    new Requirement("Assist speakers and performers on stage"),
                    new Requirement("Manage props and stage equipment"),
                    new Requirement("Coordinate with lighting and sound teams"),
                    new Requirement("Ensure smooth stage transitions"),
                    new Requirement("Remain alert and ready during the event")
                };
            var roleInfo6 = new EventRoleInfo("Stage Assistant", "Sheffield", requirements6, 80);

            var requirements7 = new List<Requirement> {
                    new Requirement("Oversee event setup and logistics"),
                    new Requirement("Coordinate with vendors and service providers"),
                    new Requirement("Manage event schedule and timelines"),
                    new Requirement("Address and resolve issues as they arise"),
                    new Requirement("Ensure a smooth overall event experience")
                };
            var roleInfo7 = new EventRoleInfo("Event Coordinator", "London", requirements7, 250);


            // Seed Events if not present
            if (!await eventAppContext.Events.AnyAsync())
            {
                var events = new List<Event>
                {
                    new Event("Tech Innovators Summit", "A conference showcasing the latest breakthroughs in AI, robotics, and software development.", DateTime.Now.AddDays(10), "/images/events/event1.jpg", organizer1!.IdentityGuid, roleInfo1),
                    new Event("Culinary Fest 2025", "A food festival celebrating local and international cuisines with live cooking demonstrations.", DateTime.Now.AddDays(20), "/images/events/event2.jpg", organizer2!.IdentityGuid, roleInfo2),
                    new Event("Green Future Expo", "An exhibition focusing on sustainable living, renewable energy, and eco-friendly innovations.", DateTime.Now.AddDays(30), "/images/events/event3.jpg", organizer1!.IdentityGuid, roleInfo3),
                    new Event("Global Startup Pitch", "Entrepreneurs from around the world pitch their innovative business ideas to investors.", DateTime.Now.AddDays(40), "/images/events/event4.jpg", organizer3!.IdentityGuid, roleInfo4),
                    new Event("Music & Arts Carnival", "A cultural festival featuring live bands, art exhibitions, and street performances.", DateTime.Now.AddDays(50), "/images/events/event5.jpg", organizer2!.IdentityGuid, roleInfo5),
                    new Event("Health & Wellness Fair", "Workshops and seminars on fitness, nutrition, and mental health awareness.", DateTime.Now.AddDays(60), "/images/events/event6.jpg", organizer3!.IdentityGuid, roleInfo6),
                    new Event("Space Exploration Talk", "A keynote session by leading scientists discussing the future of space travel.", DateTime.Now.AddDays(70), "/images/events/event7.jpg", organizer1!.IdentityGuid, roleInfo7)

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