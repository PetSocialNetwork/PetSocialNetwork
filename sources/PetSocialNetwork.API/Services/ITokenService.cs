using PetSocialNetwork.Domain.Membership;

namespace PetSocialNetwork.API.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user); 
    }
}
