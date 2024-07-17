using PetSocialNetwork.App.ViewModel;

namespace PetSocialNetwork.App.Services.UserProfile.Contracts
{
    public interface IUserProfileService
    {
        Task<UserProfileViewModel> GetUserProfileAsync(long id);
    }
}
