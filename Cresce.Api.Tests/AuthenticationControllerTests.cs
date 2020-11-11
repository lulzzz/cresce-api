using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cresce.Api.Controllers.Authentication;
using Cresce.Core.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Cresce.Api.Tests
{
    public class AuthenticationControllerTests : WebApiTests
    {
        protected override void OverrideServices(IServiceCollection services)
        {
            services.AddTransient(provider =>
            {
                var mock = new Mock<ILoginService>();

                mock.Setup(e => e.AreValidCredentials(It.Is<Credentials>(credentials => credentials.UserId == "myUser")))
                    .ReturnsAsync(true);

                return mock.Object;
            });
        }

        [Test]
        public async Task Verifying_valid_login_credentials_returns_200()
        {
            // Arrange
            var client = GetClient();

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/authentication/", new CredentialsDto
            {
                User = "myUser",
                Password = "myPass"
            });

            // Assert
            response.EnsureSuccessStatusCode();
            var loginResult = await response.Content.ReadAsAsync<LoginResultDto>();
            Assert.That(loginResult, Is.Not.Null);
            Assert.That(loginResult.OrganizationUrl, Is.EqualTo("api/v1/myUser/organization/"));
        }

        [Test]
        public async Task Verifying_invalid_login_credentials_returns_401()
        {
            // Arrange
            var client = GetClient();

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/authentication/", new CredentialsDto
            {
                User = "myUser1",
                Password = "myPass1"
            });

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

    }
}
