using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetSocialNetwork.API.Contracts.Requests;
using PetSocialNetwork.API.Contracts.Responses;
using PetSocialNetwork.Domain.Exceptions;

namespace PetSocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IMediator _mediator; 

        public UserProfileController([FromServices] IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("profile/{id}", Name = "GetProfile")]
        public async Task<ActionResult<UserProfileResponse>> GetUserProfile(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(new GetUserProfileByIdRequest(id), cancellationToken);
                return Ok(response);
            }
            catch (UserProfileNotFoundException)
            {
                return NotFound();
            }
        }


        [HttpGet("telegram/{telegramId}", Name = "GetProfileByTelegramId")]
        public async Task<ActionResult<UserProfileResponse>> GetUserProfileByTelegramId(long telegramId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(new GetUserProfileByTelegramIdRequest(telegramId), cancellationToken);
                return Ok(response);
            }
            catch(UserNotFoundException)
            {
                return NotFound();
            }          
        }
    }
}
