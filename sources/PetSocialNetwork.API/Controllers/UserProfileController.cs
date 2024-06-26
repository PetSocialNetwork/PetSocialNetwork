using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.API.DTOs;
using PetSocialNetwork.Data;
using PetSocialNetwork.Domain.Membership;

namespace PetSocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IValidator<UserProfileDTO> _validator;
        private readonly PetSocialNetworkDbContext _context;

        public UserProfileController(IValidator<UserProfileDTO> validator, PetSocialNetworkDbContext context)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet("{id}", Name = "GetProfile")]
        public async Task<RestDTO<UserProfile?>> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _context.UserProfiles
                 .AsNoTracking()
                 .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            return new RestDTO<UserProfile?>
            {
                Data = result,
                PageIndex = 0,
                PageSize = 1,
                RecordCount = result != null ? 1 : 0,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                        href: Url.Action(null, "UserProfile", new { id }, Request.Scheme)!,
                        rel: "self",
                        type: "GET"),
                }
            };
        }

        [HttpPost(Name = "CreateProfile")]
        public async Task<RestDTO<UserProfile?>> Create(UserProfileDTO profile, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(profile, cancellationToken);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var entity = new UserProfile(profile.FirstName, profile.LastName, profile.Profession, profile.Gender, profile.Animal, profile.PetGender);
                        
            await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new RestDTO<UserProfile?>()
            {
                Data = entity,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(
                        href: Url.Action(null, "UserProfile", entity, Request.Scheme)!,
                        rel: "self",
                        type: "POST"),
                }
            };
        }
    }
}
