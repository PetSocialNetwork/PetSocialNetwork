using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.API.Contracts;
using PetSocialNetwork.Data;
using PetSocialNetwork.Domain.Membership;

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

        [HttpGet("{id}", Name = "GetProfile")]
        public async Task<ActionResult<UserProfileResponse>> Get(Guid id, CancellationToken cancellationToken)
        {
            var existedUserProfile = await _context.UserProfiles
                 .AsNoTracking()
                 .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (existedUserProfile is null)
            {
                return NotFound();
            }

            var response = new UserProfileResponse
                (existedUserProfile.Id, existedUserProfile.FirstName, existedUserProfile.LastName, existedUserProfile.Gender, existedUserProfile.Profession, existedUserProfile.Animal, existedUserProfile.PetGender);

            return Ok(response);
        }

        [HttpPost(Name = "CreateProfile")]
        public async Task<ActionResult<UserProfileResponse>> Create(UserProfileRequest profile, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
           
            var userProfile = new UserProfile(Guid.NewGuid(),profile.FirstName, profile.LastName, profile.Profession, profile.Gender, profile.Animal, profile.PetGender);

            await _context.AddAsync(userProfile, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new UserProfileResponse
             (userProfile.Id, userProfile.FirstName, userProfile.LastName, userProfile.Gender, userProfile.Profession, userProfile.Animal, userProfile.PetGender);

            return Ok();
        }
    }
}
