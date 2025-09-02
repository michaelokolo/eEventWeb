using ApplicationCore.Entities.EventAggregate;
using ApplicationCore.Entities.FreelancerAggregate;
using ApplicationCore.Entities.OrganizerAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data;

public class EventAppContext : DbContext
{
    #pragma warning disable CS8618 // Required by Entity Framework
    public EventAppContext(DbContextOptions<EventAppContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<Freelancer> Freelancers { get; set; }
    public DbSet<Organizer> Organizers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
