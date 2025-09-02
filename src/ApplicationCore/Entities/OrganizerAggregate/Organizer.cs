using ApplicationCore.Entities.EventAggregate;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;

namespace ApplicationCore.Entities.OrganizerAggregate;

public class Organizer : BaseEntity, IAggregateRoot
{
    public string IdentityGuid { get; private set; }
    public string Name { get; private set; }

    #pragma warning disable CS8618 // Required by Entity Framework
    private Organizer() { } // For EF

    public Organizer(string identityGuid, string name)
    {
        Guard.Against.NullOrEmpty(identityGuid, nameof(identityGuid));
        Guard.Against.NullOrEmpty(name, nameof(name));
        IdentityGuid = identityGuid;
        Name = name;
    }

    private readonly List<Event> _events = new();
    public IReadOnlyCollection<Event> Events => _events.AsReadOnly();
}
