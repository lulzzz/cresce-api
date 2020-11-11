using System.Threading.Tasks;
using Cresce.Core.Authentication;

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
    }
}
