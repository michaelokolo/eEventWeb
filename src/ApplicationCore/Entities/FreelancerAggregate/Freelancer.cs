using ApplicationCore.Entities.EventAggregate;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;

namespace ApplicationCore.Entities.FreelancerAggregate;

public class Freelancer : BaseEntity, IAggregateRoot
{
    public string IdentityGuid { get; private set; }
    public string Name { get; private set; }

    #pragma warning disable CS8618 // Required by Entity Framework
    private Freelancer() { } // For EF

    public Freelancer(string identityGuid, string name)
    {
        Guard.Against.NullOrEmpty(identityGuid, nameof(identityGuid));
        Guard.Against.NullOrEmpty(name, nameof(name));
        IdentityGuid = identityGuid;
        Name = name;
    }

    private readonly List<Application> _applications = new();
    public IReadOnlyCollection<Application> Applications => _applications.AsReadOnly();
}
