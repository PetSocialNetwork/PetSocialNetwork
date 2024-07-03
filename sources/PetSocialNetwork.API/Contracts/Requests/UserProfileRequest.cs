using MediatR;
using PetSocialNetwork.API.Contracts.Responses;
using PetSocialNetwork.Domain.Membership;

namespace PetSocialNetwork.API.Contracts.Requests
{
    public record UserProfileRequest
        (Guid Id, string FirstName, string LastName, Gender Gender, string? Profession, string? Animal, Gender PetGender) : IRequest<UserProfileResponse>;

}
