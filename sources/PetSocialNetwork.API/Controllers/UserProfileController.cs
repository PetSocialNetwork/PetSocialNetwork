using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.API.Contracts;
using PetSocialNetwork.Data;

namespace PetSocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly PetSocialNetworkDbContext _context;

        public UserProfileController(
            [FromServices] PetSocialNetworkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet("profile/{id}", Name = "GetProfile")]
        public async Task<ActionResult<UserProfileResponse>> GetUserProfile(Guid id, CancellationToken cancellationToken)
        {
            var existedUserProfile = await _context.UserProfiles
                 .AsNoTracking()
                 .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (existedUserProfile is null)
            {
                return NotFound();
            }

            var response = new UserProfileResponse
                (existedUserProfile.Id, existedUserProfile.FirstName, existedUserProfile.LastName, existedUserProfile.UserName,
                existedUserProfile.Gender, existedUserProfile.Profession, existedUserProfile.Animal, existedUserProfile.PetGender);

            return Ok(response);
        }


        [HttpGet("telegram/{telegramId}", Name = "GetProfileByTelegramId")]
        public async Task<ActionResult<UserProfileResponse>> GetUserProfileByTelegramId(long telegramId, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(it => it.TelegramId == telegramId, cancellationToken);

            if (user is null)
            {
                return NotFound();
            }

            var response = new UserProfileResponse
                (user.UserProfile.Id, user.UserProfile.FirstName, user.UserProfile.LastName, user.UserProfile.UserName,
                user.UserProfile.Gender, user.UserProfile.Profession, user.UserProfile.Animal, user.UserProfile.PetGender);

            return Ok(response);
        }
    }
}
