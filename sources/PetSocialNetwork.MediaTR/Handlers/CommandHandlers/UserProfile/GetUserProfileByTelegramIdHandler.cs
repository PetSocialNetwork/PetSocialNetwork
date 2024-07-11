using MediatR;
using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.API.Contracts.Requests;
using PetSocialNetwork.API.Contracts.Responses;
using PetSocialNetwork.Data;
using PetSocialNetwork.Domain.Exceptions;

namespace PetSocialNetwork.MediaTR.Handlers
{
    public class GetUserProfileByTelegramIdHandler : IRequestHandler<GetUserProfileByTelegramIdRequest, UserProfileResponse>,
                                      IRequestHandler<GetUserProfileByIdRequest, UserProfileResponse>
    {
        private readonly PetSocialNetworkDbContext _context;

        public GetUserProfileByTelegramIdHandler(PetSocialNetworkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<UserProfileResponse> Handle(GetUserProfileByTelegramIdRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                 .AsNoTracking()
                 .SingleOrDefaultAsync(it => it.TelegramId == request.TelegramId, cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException("User not found");
            }

            var response = new UserProfileResponse
                (user.UserProfile.Id, user.UserProfile.FirstName, user.UserProfile.LastName, user.UserProfile.UserName,
                user.UserProfile.Gender, user.UserProfile.Profession, user.UserProfile.Animal, user.UserProfile.PetGender);

            return response;
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
