using System;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Sessions;
using NUnit.Framework;

namespace Cresce.Core.Tests.Sessions
{
    public class CreateSessionTests : ServicesTests<ISessionServices>
    {
        [Test]
        public async Task Creating_an_Session_stores_the_given_Session()
        {
            var services = MakeService();
            var session = new Session
            {
                Hours = 2.5,
                CustomerId = 1,
                ServiceId = 1,
                StartedAt = new DateTime(2020, 02, 12)
            };

            await services.CreateSession(session, GetEmployeeAuthorization());

            await AssertSessionIsStored(services, session with
            {
                Id = 2,
                EmployeeId = 1
            });
        }

        [Test]
        public async Task Creating_an_Session_return_newly_created_Session()
        {
            var services = MakeService();
            var session = new Session
            {
                Hours = 2.5,
                CustomerId = 1,
                ServiceId = 1,
                StartedAt = new DateTime(2020, 02, 12)
            };

            var createdSession = await services.CreateSession(session, GetEmployeeAuthorization());

            Assert.That(createdSession, Is.EqualTo(session with
            {
                Id = 2,
                EmployeeId = 1
            }));
        }

        [Test]
        public void Getting_Sessions_with_expired_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.CreateSession(new Session(), GetExpiredEmployeeAuthorization())
            );
        }

        [Test]
        public void Getting_Sessions_with_invalid_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.CreateSession(new Session(), GetInvalidEmployeeAuthorization())
            );
        }

        private async Task AssertSessionIsStored(ISessionServices services, Session newSession)
        {
            CollectionAssert.Contains(
                await services.GetSessions(GetEmployeeAuthorization()),
                newSession
            );
        }
    }
}
