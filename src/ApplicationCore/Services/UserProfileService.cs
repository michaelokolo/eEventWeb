using ApplicationCore.Entities.FreelancerAggregate;
using ApplicationCore.Entities.OrganizerAggregate;
using ApplicationCore.Interfaces;

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
}
