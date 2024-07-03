using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetSocialNetwork.API.Configurations;
using PetSocialNetwork.API.Contracts.Requests;
using PetSocialNetwork.API.Contracts.Responses;
using PetSocialNetwork.API.Models;
using System.Security.Authentication;

namespace PetSocialNetwork.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ISender _mediator;
    public LoginController([FromServices] ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("telegram-hook")]
    public Task<IActionResult> HookTelegramLogin([FromQuery] TelegramAuthRequest request) =>
        Task.FromResult((IActionResult)StatusCode(StatusCodes.Status501NotImplemented));


    [HttpGet("login_by_telegram")]
    public async Task<ActionResult<LoginResponse>> LoginByTelegram(
            [FromQuery] string id,
            [FromQuery] string first_name,
            [FromQuery] string last_name,
            [FromQuery] string username,
            [FromQuery] string photo_url,
            [FromQuery] string auth_date,
            [FromQuery] string hash,
            [FromServices] TelegramBotConfig secretKey,
            CancellationToken cancellationToken)
    {
        try
        {
            var request = new LoginByTelegramRequest(id, first_name, last_name, username, photo_url, auth_date, hash, secretKey.Token);
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
        catch (AuthenticationException)
        {
            return Unauthorized();
        }
    }
}