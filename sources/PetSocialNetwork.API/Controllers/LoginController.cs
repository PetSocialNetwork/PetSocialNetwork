using Microsoft.AspNetCore.Mvc;

namespace PetSocialNetwork.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    [HttpGet("telegram-hook")]
    public Task<IActionResult> HookTelegramLogin([FromQuery] TelegramAuthRequest request) =>
        Task.FromResult((IActionResult) StatusCode(StatusCodes.Status501NotImplemented));
}