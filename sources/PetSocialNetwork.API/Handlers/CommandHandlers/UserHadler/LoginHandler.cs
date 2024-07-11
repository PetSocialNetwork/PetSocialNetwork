using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.API.Contracts.Requests;
using PetSocialNetwork.API.Contracts.Responses;
using PetSocialNetwork.API.Services;
using PetSocialNetwork.Data;
using PetSocialNetwork.Domain.Membership;
using PetSocialNetwork.NotificationService;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace PetSocialNetwork.API.Handlers.CommandHandlers.UserHadler
{
    public class LoginHandler : BaseHandler<LoginByTelegramRequest, LoginResponse>
    {
        private readonly ITokenService _tokenService;
        private readonly IBus _bus;
        public LoginHandler(PetSocialNetworkDbContext context, [FromServices] IBus bus,
            [FromServices] ITokenService tokenService) : base(context)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }
        public override async Task<LoginResponse> Handle(LoginByTelegramRequest request, CancellationToken cancellationToken)
        {
            var dataCheckString =
                    $"auth_date={request.AuthDate}\nfirst_name={request.FirstName}\nid={request.Id}\n" +
                    $"last_name={request.LastName}\nphoto_url={request.PhotoUrl}\nusername={request.Username}";
            var calculatedHash = GenerateHmacSha256Hash(dataCheckString, request.SecretKey);

            if (!request.Hash.Equals(calculatedHash, StringComparison.OrdinalIgnoreCase))
            {
                throw new AuthenticationException("Unauthorized user");
            }

            var user =
                await _context.Users.SingleOrDefaultAsync(it => it.TelegramId == long.Parse(request.Id), cancellationToken);

            if (user is null)
            {
                user = new User(Guid.NewGuid(), long.Parse(request.Id), false);
                user.AddUserProfile(request.FirstName, request.LastName, request.Username);
                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            var token = _tokenService.GenerateToken(user);
            var response = new LoginResponse(user.Id, user.TelegramId, token);

        
            //нужно получать chatId
            var registrationMessage = new TelegramRegistrationMessage{ChatId = user.TelegramId, Message = "Добро пожаловать в телеграм" };
            await _bus.PubSub.PublishAsync(registrationMessage, "registration_queue", cancellationToken);

            return response;
        }

        private string GenerateHmacSha256Hash(string data, string key)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", string.Empty);
            }
        }
    }
}
