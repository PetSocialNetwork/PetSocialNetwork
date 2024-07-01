using PetSocialNetwork.Domain.Membership;

namespace PetSocialNetwork.API.Contracts
{
    public record UserProfileRequest(Guid Id, string FirstName, string LastName, Gender Gender, string? Profession, string? Animal, Gender PetGender);
}
