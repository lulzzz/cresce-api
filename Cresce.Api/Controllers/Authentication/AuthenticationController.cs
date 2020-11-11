using System.Net.Http;
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

        // GET
        [HttpPost]
        public async Task<IActionResult> Login(CredentialsDto credentials)
        {
            if (await credentials.Verify(_loginService))
            {
                return new OkObjectResult(new LoginResultDto
                {
                    OrganizationUrl = $"api/v1/{credentials.User}/organization/"
                });
            }

            return Unauthorized();
        }
    }

    public class LoginResultDto
    {
        public string OrganizationUrl { get; set; }
    }
}
