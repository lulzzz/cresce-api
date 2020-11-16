using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Cresce.Api.Controllers.Authentication
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthenticationController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet("{token}")]
        public IActionResult VerifyToken(string token)
        {
            var tokenHandler = new TokenHandler(token);
            if (!tokenHandler.IsOk)
            {
                return Unauthorized(new UnauthorizedDto
                {
                    Reason = tokenHandler.Reason
                });
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login(CredentialsDto credentials)
        {
            if (await credentials.Verify(_loginService))
            {
                return new OkObjectResult(new LoginResultDto
                {
                    OrganizationUrl = $"api/v1/{credentials.User}/organization/",
                    Token = credentials.GenerateToken()
                });
            }

            return Unauthorized();
        }
    }

    public class UnauthorizedDto
    {
        public UnauthorizedReason Reason { get; set; }
    }

    public enum UnauthorizedReason
    {
        Unknown,
        Expired,
        Invalid
    }
}
