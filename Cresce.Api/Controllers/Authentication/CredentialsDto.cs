using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace Cresce.Api.Controllers.Authentication
{
    public class CredentialsDto
    {
        public string User { get; set; }
        public string Password { get; set; }

        public Task<bool> Verify(ILoginService loginService)
        {
            return loginService.AreValidCredentials(
                new Credentials(User, Password)
            );
        }

        public string GenerateToken(DateTime? dateTime = null)
        {
            var expirationDate = dateTime ?? DateTime.UtcNow.AddDays(2);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, User),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Secret)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

