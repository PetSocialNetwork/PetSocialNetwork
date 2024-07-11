using MediatR;
using PetSocialNetwork.API.Contracts.Responses;

namespace PetSocialNetwork.API.Contracts.Requests
{
    public record GetUserProfileByTelegramIdRequest(long TelegramId) : IRequest<UserProfileResponse>;
}
