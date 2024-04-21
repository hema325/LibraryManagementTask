using LibraryManagementTask.Entities;
using LibraryManagementTask.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementTask.Services.JWT
{
    public class TokenGenerator: ITokenGenerator
    {
        private readonly JWTSettings _settings;

        public TokenGenerator(IOptions<JWTSettings> settings)
        {
            _settings = settings.Value;
        }

        public string Generate(User user)
        {
            var claims = ExtractClaims(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpirationInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Claim[] ExtractClaims(User user)
            => new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
    }
}
