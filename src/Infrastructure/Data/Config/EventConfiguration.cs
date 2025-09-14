using ApplicationCore.Entities.EventAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(e => e.Title)
            .IsRequired(true)
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .IsRequired(true)
            .HasMaxLength(2000);

        builder.Property(e => e.Date)
            .IsRequired(true);

        builder.Property(e => e.PictureUri)
            .IsRequired(true);

        builder.Property(e => e.OrganizerId)
            .IsRequired();

        builder.OwnsOne(e => e.RoleInfo, ri =>
        {
            ri.Property(r => r.Role).HasMaxLength(100);
            ri.Property(r => r.Location).HasMaxLength(200);
            ri.Property(r => r.Budget).HasColumnType("decimal(18,2)");

            ri.OwnsMany(r => r.Requirements, r =>
            {
                r.Property(x => x.Description).HasMaxLength(500);
                r.WithOwner().HasForeignKey("EventId");
            });
        });

        // Configure the private field for applications
        var navigation = builder.Metadata.FindNavigation(nameof(Event.Applications));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
