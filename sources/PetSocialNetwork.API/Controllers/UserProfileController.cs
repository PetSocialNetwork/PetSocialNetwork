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
        //private readonly IValidator<UserProfileDTO> _validator;
        private readonly PetSocialNetworkDbContext _context;

        public UserProfileController(
            //IValidator<UserProfileDTO> validator, 
            [FromServices] PetSocialNetworkDbContext context)
        {
            //_validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet("{id}", Name = "GetProfile")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _context.UserProfiles
                 .AsNoTracking()
                 .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost(Name = "CreateProfile")]
        public async Task<IActionResult> Create(UserProfileRequest profile, CancellationToken cancellationToken)
        {
            //var result = await _validator.ValidateAsync(profile, cancellationToken);

            //if (!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //}
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var entity = new UserProfile(profile.FirstName, profile.LastName, profile.Profession, profile.Gender, profile.Animal, profile.PetGender);

            await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
