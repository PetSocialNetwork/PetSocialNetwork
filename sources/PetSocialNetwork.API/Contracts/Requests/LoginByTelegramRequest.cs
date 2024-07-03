using MediatR;
using PetSocialNetwork.API.Contracts.Responses;

namespace PetSocialNetwork.API.Contracts.Requests
{
    public record LoginByTelegramRequest(string Id, string FirstName, string LastName, string Username, 
        string PhotoUrl, string AuthDate, string Hash, string SecretKey) : IRequest<LoginResponse>;
}
