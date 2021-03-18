using System;
using System.Threading.Tasks;
using Cresce.Core.Appointments;
using Cresce.Core.Authentication;
using NUnit.Framework;

namespace Cresce.Core.Tests.Appointments
{
    public class GetAppointmentsTests : ServicesTests<IAppointmentServices>
    {
        [Test]
        public async Task Get_Sessions_lists_returns_the_full_list_of_services()
        {
            var services = MakeService();

            var entities = await services.GetAppointments(GetEmployeeAuthorization());

            CollectionAssert.AreEqual(new[]
            {
                new Appointment
                {
                    Id = 1,
                    CustomerId = 1,
                    EmployeeId = 1,
                    ServiceId = 1,
                    From = new DateTime(2021, 3, 16, 10, 0, 0),
                    To = new DateTime(2021, 3, 16, 11, 0, 0),
                    Color = "0xFF2196F3",
                },
                new Appointment
                {
                    Id = 2,
                    CustomerId = 1,
                    EmployeeId = 1,
                    ServiceId = 1,
                    From = new DateTime(2021, 3, 16, 15, 0, 0),
                    To = new DateTime(2021, 3, 16, 16, 0, 0),
                    Color = "0xFF2196F3",
                    Recurrence = new Recurrence
                    {
                        Type = RecurrenceType.Weekly,
                        WeekDays = new [] { WeekDays.Monday, WeekDays.Tuesday },
                        Start = new DateTime(2021, 3, 16),
                        End = new DateTime(2021, 4, 16),
                    }
                },
            }, entities);
        }

        [Test]
        public void Getting_Sessions_with_expired_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.GetAppointments(GetExpiredEmployeeAuthorization())
            );
        }

        [Test]
        public void Getting_Sessions_with_invalid_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.GetAppointments(GetInvalidEmployeeAuthorization())
            );
        }
    }
}
