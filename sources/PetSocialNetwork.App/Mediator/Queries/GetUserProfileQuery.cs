using MediatR;
using PetSocialNetwork.App.ViewModel;

namespace PetSocialNetwork.App.Mediator.Queries
{
    public class GetUserProfileQuery : IRequest<UserProfileViewModel>
    {
        public Guid Id { get; set; }
    }
}
