using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.Data;
using PetSocialNetwork.Domain.Membership;

namespace PetSocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IValidator<UserProfile> _validator;
        private readonly PetSocialNetworkDbContext _context;

        public UserProfileController(IValidator<UserProfile> validator, PetSocialNetworkDbContext context)
        {
            _validator = validator;
            _context = context;
        }

        [HttpGet("{id}", Name = "GetProfile")]
        public async Task<UserProfile?> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.UserProfiles
                 .AsNoTracking()
                 .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        [HttpPost(Name = "CreateProfile")]
        public async Task<UserProfile> Create(UserProfile profile, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(profile, cancellationToken);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            await _context.AddAsync(profile, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return profile;
        }
    }
}
