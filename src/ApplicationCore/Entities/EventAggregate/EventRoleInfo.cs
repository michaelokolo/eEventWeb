using Ardalis.GuardClauses;


namespace ApplicationCore.Entities.EventAggregate;

public class EventRoleInfo
{
    public string Role { get; private set; }
    public string Location { get; private set; }
    public decimal Budget { get; private set; }

    // EF Core can map to List<string>
    private readonly List<Requirement> _requirements = new();
    public IReadOnlyList<Requirement> Requirements => _requirements.AsReadOnly();

    #pragma warning disable CS8618 // Required by Entity Framework
    private EventRoleInfo() { }

    public EventRoleInfo(string role, string location, IEnumerable<Requirement> requirements, decimal budget)
    {
        Guard.Against.NullOrEmpty(role, nameof(role));
        Guard.Against.NullOrEmpty(location, nameof(location));
        Guard.Against.NullOrEmpty(requirements, nameof(requirements));
        Guard.Against.NegativeOrZero(budget, nameof(budget));

        Role = role;
        Location = location;
        Budget = budget;
        _requirements.AddRange(requirements);
    }
}
