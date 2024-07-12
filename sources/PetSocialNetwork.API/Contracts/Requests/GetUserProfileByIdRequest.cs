using MediatR;
using PetSocialNetwork.API.Contracts.Responses;

namespace PetSocialNetwork.API.Contracts.Requests
{
    public record GetUserProfileByIdRequest(Guid Id) : IRequest<UserProfileResponse>;
}
