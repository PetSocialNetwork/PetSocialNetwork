using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.API.Models;
using PetSocialNetwork.API.Services;
using PetSocialNetwork.Data;
using PetSocialNetwork.Domain.Membership;
using System.Security.Cryptography;
using System.Text;

namespace PetSocialNetwork.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly PetSocialNetworkDbContext _dbContext;
    public LoginController(ITokenService tokenService, PetSocialNetworkDbContext dbContext)
    {
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    [HttpGet("telegram-hook")]
    public Task<IActionResult> HookTelegramLogin([FromQuery] TelegramAuthRequest request) =>
        Task.FromResult((IActionResult)StatusCode(StatusCodes.Status501NotImplemented));

    [HttpGet("login_by_telegram")]
    public async Task<ActionResult<LoginResponse>> LoginByTelegram([FromQuery] string id,
            [FromServices] string secretKey,
            [FromQuery] string hash,
            CancellationToken cancellationToken)
    {
        var dataCheckString = $"id={id}";
        var calculatedHash = GenerateHmacSha256Hash(dataCheckString, secretKey);

        if (!hash.Equals(calculatedHash, StringComparison.OrdinalIgnoreCase))
        {
            return Unauthorized();
        }

        var user = await _dbContext.Users.SingleOrDefaultAsync(it => it.TelegramId == long.Parse(id), cancellationToken);

        if (user is null)
        {
            user = new User(Guid.NewGuid(), long.Parse(id));
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        var token = _tokenService.GenerateToken(user);
        var response = new LoginResponse(user.Id, user.TelegramId, token);

        return Ok(response);
    }

    private string GenerateHmacSha256Hash(string data, string key)
    {
        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
        {
            return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", string.Empty);
        }
    }
}