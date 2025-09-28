using ApplicationCore.Entities.FreelancerAggregate;
using ApplicationCore.Entities.OrganizerAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Ardalis.GuardClauses;

namespace ApplicationCore.Services;

public class UserProfileService : IUserProfileService
{
    private readonly IRepository<Freelancer> _freelancerRepo;
    private readonly IRepository<Organizer> _organizerRepo;


    public UserProfileService(IRepository<Freelancer> freelancerRepo, IRepository<Organizer> organizerRepo)
    {
        _freelancerRepo = freelancerRepo;
        _organizerRepo = organizerRepo;
    }

    public async Task CreateFreelancerAsync(string identityGuid, string name)
    {
        var freelancer = new Freelancer(identityGuid, name);
        await _freelancerRepo.AddAsync(freelancer);
    }

    public async Task CreateOrganizerAsync(string identityGuid, string name)
    {
        var organizer = new Organizer(identityGuid, name);
        await _organizerRepo.AddAsync(organizer);
    }

    public async Task<string?> GetFreelancerNameAsync(string freelancerId)
    {
        Guard.Against.NullOrEmpty(freelancerId, nameof(freelancerId));
        var spec = new FreelancerByIdentityGuidSpecification(freelancerId);
        var freelancer = await _freelancerRepo.FirstOrDefaultAsync(spec);
        return freelancer?.Name;
    }

    public async Task<string?> GetOrganizerNameAsync(string organizerId)
    {
        Guard.Against.NullOrEmpty(organizerId, nameof(organizerId));
        var spec = new OrganizerByIdentityGuidSpecification(organizerId);
        var organizer = await _organizerRepo.FirstOrDefaultAsync(spec);
        return organizer?.Name;
    }
}
