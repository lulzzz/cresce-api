using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cresce.Api.Models;
using NUnit.Framework;

namespace Cresce.Api.Tests.Controllers.EmployeeScope
{
    public class AppointmentsControllerTests : WebApiTests
    {
        [Test]
        public async Task Posting_an_Appointment_returns_201()
        {
            var client = await GetAuthenticatedEmployeeClient();

            var response = await client.PostAsJsonAsync(
                "api/v1/Appointments",
                new NewAppointmentModel
                {
                    CustomerId = 1,
                    ServiceId = 1,
                    From = new DateTime(2020, 01, 23),
                    To = new DateTime(2020, 01, 23)
                }
            );

            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.Headers.Location!.ToString(), Is.EqualTo("api/v1/Appointment/2"));
            Assert.That(await response.GetContent<SessionModel>(), Is.EqualTo(new SessionModel
            {
                Id = 2,
                Hours = 4.0,
                CustomerId = 1,
                EmployeeId = 1,
                ServiceId = 1,
                StartedAt = new DateTime(2020, 01, 23)
            }));
        }
    }
}
