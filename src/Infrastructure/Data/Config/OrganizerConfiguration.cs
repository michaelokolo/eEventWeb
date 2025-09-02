using ApplicationCore.Entities.OrganizerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class OrganizerConfiguration : IEntityTypeConfiguration<Organizer>
{
    public void Configure(EntityTypeBuilder<Organizer> builder)
    {
        builder.Property(o => o.IdentityGuid)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(200);

        // Configure the private field for events
        var navigation = builder.Metadata.FindNavigation(nameof(Organizer.Events));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

    }
}
