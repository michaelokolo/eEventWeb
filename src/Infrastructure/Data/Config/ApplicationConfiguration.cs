using ApplicationCore.Entities.EventAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.Property(a => a.Status)
            .IsRequired();

        builder.Property(a => a.AppliedOn)
            .IsRequired();

        builder.HasOne<Event>()
            .WithMany(e => e.Applications)
            .HasForeignKey(a => a.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(a => a.FreelancerId)
            .IsRequired();

    }
}
