using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Cresce.Api.Controllers.Authentication
{
    internal class TokenHandler
    {
        private readonly JwtSecurityToken _token;

        public TokenHandler(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            _token = tokenHandler.CanReadToken(token)
                ? tokenHandler.ReadJwtToken(token)
                : null;
        }

        private bool IsExpired => _token.ValidTo < DateTime.UtcNow.AddSeconds(5);

        public UnauthorizedReason Reason =>
            _token != null
                ? UnauthorizedReason.Expired
                : UnauthorizedReason.Invalid;

        public bool IsOk => _token != null && !IsExpired;
    }
}
