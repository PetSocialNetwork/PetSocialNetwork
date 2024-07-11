using MediatR;
using PetSocialNetwork.App.Mediator.Queries;
using PetSocialNetwork.App.ViewModel;
using System.Net.Http.Json;

namespace PetSocialNetwork.App.Mediator.Handlers.QueryHandlers.UserProfile
{
    public class GetUserProfileQueryHandler : IPipelineBehavior<GetUserProfileQuery, UserProfileViewModel>
    {
        private readonly IHttpClientFactory _httpClient;

        public GetUserProfileQueryHandler(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<UserProfileViewModel> Handle(GetUserProfileQuery request, RequestHandlerDelegate<UserProfileViewModel> next, CancellationToken cancellationToken)
        {
            var client = _httpClient.CreateClient("BaseAPI");
            var response = await client.GetAsync($"/userprofile/profile/{request.Id}", cancellationToken);

            response.EnsureSuccessStatusCode();
            var userProfile = await response.Content.ReadFromJsonAsync<UserProfileViewModel>(cancellationToken);

            return userProfile;
        }
    }
}
