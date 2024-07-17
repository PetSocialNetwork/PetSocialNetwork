using MediatR;
using PetSocialNetwork.App.Mediator.Queries;
using PetSocialNetwork.App.Services.UserProfile.Contracts;
using PetSocialNetwork.App.ViewModel;

namespace PetSocialNetwork.App.Services.UserProfile
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IMediator _mediator;

        public UserProfileService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<UserProfileViewModel> GetUserProfileAsync(long id)
        {
            try
            {
                return await _mediator.Send(new GetUserProfileQuery { Id = id });
            }
            catch (HttpRequestException httpRequestException)
            {
                throw new ApplicationException(httpRequestException.Message);
            }
            catch (Exception e)
            {
                throw new ApplicationException (e.Message);
            }
        }
    }
}
