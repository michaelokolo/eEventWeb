

using ApplicationCore.Entities.FreelancerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class FreelancerConfiguration : IEntityTypeConfiguration<Freelancer>
{
    public void Configure(EntityTypeBuilder<Freelancer> builder)
    {
        builder.Property(f => f.IdentityGuid)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(200);

        // Configure the private field for applications
        var navigation = builder.Metadata.FindNavigation(nameof(Freelancer.Applications));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
