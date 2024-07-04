using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.API.Contracts.Requests;
using PetSocialNetwork.API.Contracts.Responses;
using PetSocialNetwork.Data;
using PetSocialNetwork.Domain.Exceptions;

namespace PetSocialNetwork.API.Handlers.CommandHandlers.UserProfileHandler
{
    public class GetUserProfileByIdHandler : BaseHandler<GetUserProfileByIdRequest, UserProfileResponse>
    {
        public GetUserProfileByIdHandler(PetSocialNetworkDbContext context) : base(context) { }
        public override async Task<UserProfileResponse> Handle(GetUserProfileByIdRequest request, CancellationToken cancellationToken)
        {
            var existedUserProfile = await _context.UserProfiles
                  .AsNoTracking()
                  .SingleOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (existedUserProfile is null)
            {
                throw new UserProfileNotFoundException("UserProfile not found");
            }

            var response = new UserProfileResponse
                (existedUserProfile.Id, existedUserProfile.FirstName, existedUserProfile.LastName, existedUserProfile.UserName,
                existedUserProfile.Gender, existedUserProfile.Profession, existedUserProfile.Animal, existedUserProfile.PetGender);

            return response;
        }
    }
}
