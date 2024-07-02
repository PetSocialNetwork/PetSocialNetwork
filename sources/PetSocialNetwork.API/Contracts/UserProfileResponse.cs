using PetSocialNetwork.Domain.Membership;

namespace PetSocialNetwork.API.Contracts
{
    public record UserProfileResponse(Guid Id, string FirstName, string LastName, string userName, Gender Gender, string? Profession, string? Animal, Gender PetGender);
}
