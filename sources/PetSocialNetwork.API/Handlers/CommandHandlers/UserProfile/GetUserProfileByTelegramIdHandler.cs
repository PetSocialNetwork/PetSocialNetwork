﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.API.Contracts.Requests;
using PetSocialNetwork.API.Contracts.Responses;
using PetSocialNetwork.Data;
using PetSocialNetwork.Domain.Exceptions;

namespace PetSocialNetwork.API.Handlers.CommandHandlers.UserProfile
{
    public class GetUserProfileByTelegramIdHandler : IRequestHandler<GetUserProfileByTelegramIdRequest, UserProfileResponse>
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
    }
}
