using System;
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

                mock.Setup(e => e.AreValidCredentials(It.Is<Credentials>(c => c.UserId == "myUser")))
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
            Assert.That(loginResult.Token, Is.Not.Null);
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

        [Test]
        public async Task Validating_an_expired_token_returns_401()
        {
            // Arrange
            var client = GetClient();
            var token = GenerateExpiredToken();

            // Act
            var response = await client.GetAsync($"/api/v1/authentication/{token}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            var dto = await response.Content.ReadAsAsync<UnauthorizedDto>();
            Assert.That(dto, Is.Not.Null);
            Assert.That(dto.Reason, Is.EqualTo(UnauthorizedReason.Expired));
        }

        [Test]
        public async Task Validating_an_invalid_token_returns_401()
        {
            // Arrange
            var client = GetClient();
            const string token = "some_invalid_token";

            // Act
            var response = await client.GetAsync($"/api/v1/authentication/{token}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            var dto = await response.Content.ReadAsAsync<UnauthorizedDto>();
            Assert.That(dto, Is.Not.Null);
            Assert.That(dto.Reason, Is.EqualTo(UnauthorizedReason.Invalid));
        }

        [Test]
        public async Task Validating_a_valid_token_returns_200()
        {
            // Arrange
            var client = GetClient();
            var token = GenerateToken();

            // Act
            var response = await client.GetAsync($"/api/v1/authentication/{token}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        private static string GenerateToken()
        {
            return new CredentialsDto
            {
                User = "test User"
            }.GenerateToken(DateTime.UtcNow.AddMinutes(10));
        }

        private static string GenerateExpiredToken()
        {
            return new CredentialsDto
            {
                User = "test User"
            }.GenerateToken(DateTime.UtcNow.AddSeconds(1));
        }
    }
}

