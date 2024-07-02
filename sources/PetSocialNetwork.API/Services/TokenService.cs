using Microsoft.IdentityModel.Tokens;
using PetSocialNetwork.API.Configurations;
using PetSocialNetwork.Domain.Membership;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PetSocialNetwork.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfig _jwtConfig;
        public TokenService(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig ?? throw new ArgumentNullException(nameof(jwtConfig));
        }

        public string GenerateToken(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = CreateClaimsIdentity(user),
                Expires = DateTime.UtcNow.Add(_jwtConfig.LifeTime),
                Audience = _jwtConfig.Audience,
                Issuer = _jwtConfig.Issuer,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_jwtConfig.SigningKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity CreateClaimsIdentity(User user)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
             {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("hasFullProfile", user.HasFullProfile.ToString()) 
            });
            return claimsIdentity;
        }
    }
}

