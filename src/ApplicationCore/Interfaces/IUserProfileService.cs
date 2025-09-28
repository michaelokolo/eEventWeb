namespace ApplicationCore.Interfaces;

public interface IUserProfileService
{
    Task CreateFreelancerAsync(string identityGuid, string name);
    Task CreateOrganizerAsync(string identityGuid, string name);
    Task<string?> GetFreelancerNameAsync(string freelancerId);
    Task<string?> GetOrganizerNameAsync(string organizerId);

}