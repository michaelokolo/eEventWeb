namespace ApplicationCore.Entities.EventAggregate;

public class Requirement : BaseEntity
{
    public string Description { get; private set; }

    #pragma warning disable CS8618 // Required by Entity Framework
    private Requirement() { } // For EF

    public Requirement(string description)
    {
        Description = description;
    }
}
