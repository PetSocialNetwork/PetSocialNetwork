using MediatR;
using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.API.Contracts.Requests;
using PetSocialNetwork.API.Contracts.Responses;
using PetSocialNetwork.Data;
using PetSocialNetwork.Domain.Exceptions;


namespace PetSocialNetwork.MediaTR.Handlers.CommandHandlers.UserProfile
{
    public class GetUserProfileByIdHandler : IRequestHandler<GetUserProfileByIdRequest, UserProfileResponse>
    {
        private readonly PetSocialNetworkDbContext _context;
        public GetUserProfileByIdHandler(PetSocialNetworkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<UserProfileResponse> Handle(GetUserProfileByIdRequest request, CancellationToken cancellationToken)
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
