using ApplicationCore.Entities.EventAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(e => e.Date)
            .IsRequired();

        builder.Property(e => e.OrganizerId)
            .IsRequired();

        // Configure the private field for applications
        var navigation = builder.Metadata.FindNavigation(nameof(Event.Applications));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
