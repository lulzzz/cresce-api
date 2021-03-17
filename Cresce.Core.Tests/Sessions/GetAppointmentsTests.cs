using System;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Sessions;
using NUnit.Framework;

namespace Cresce.Core.Tests.Sessions
{
    public class GetSessionsTests : ServicesTests<ISessionServices>
    {
        [Test]
        public async Task Get_Sessions_lists_returns_the_full_list_of_services()
        {
            var services = MakeService();

            var entities = await services.GetSessions(GetEmployeeAuthorization());

            CollectionAssert.AreEqual(new[]
            {
                new Session
                {
                    Id = 1,
                    Discount = 10.0,
                    Hours = 3.5,
                    Value = 30.0,
                    CustomerId = 1,
                    EmployeeId = 1,
                    ServiceId = 1,
                    StartedAt = new DateTime(2020, 02, 10)
                },
            }, entities);
        }

        [Test]
        public void Getting_Sessions_with_expired_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.GetSessions(GetExpiredEmployeeAuthorization())
            );
        }

        [Test]
        public void Getting_Sessions_with_invalid_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.GetSessions(GetInvalidEmployeeAuthorization())
            );
        }
    }
}
