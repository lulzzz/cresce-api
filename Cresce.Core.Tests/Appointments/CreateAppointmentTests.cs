using System.Threading.Tasks;
using Cresce.Core.Appointments;
using Cresce.Core.Authentication;
using NUnit.Framework;

namespace Cresce.Core.Tests.Appointments
{
    public class CreateAppointmentTests : ServicesTests<IAppointmentServices>
    {
        [Test]
        public async Task Creating_an_Appointment_stores_the_given_Appointment()
        {
            var services = MakeService();
            var appointment = new Appointment
            {
                CustomerId = 1,
                ServiceId = 1,
            };

            await services.CreateAppointment(appointment, GetEmployeeAuthorization());

            await AssertAppointmentIsStored(services, appointment with
            {
                Id = 3,
                EmployeeId = 1,
                CustomerId = 1,
                ServiceId = 1,
                Color = "0xFF2196F3",
                EventName = "Diogo Quintas\nDevelopment"
            });
        }

        [Test]
        public async Task Creating_an_Appointment_return_newly_created_Appointment()
        {
            var services = MakeService();
            var appointment = new Appointment
            {
                CustomerId = 1,
                ServiceId = 1,
            };

            var createdAppointment = await services.CreateAppointment(appointment, GetEmployeeAuthorization());

            Assert.That(createdAppointment, Is.EqualTo(appointment with
            {
                Id = 3,
                EmployeeId = 1,
                CustomerId = 1,
                ServiceId = 1,
                Color = "0xFF2196F3",
                EventName = "Diogo Quintas\nDevelopment"
            }));
        }

        [Test]
        public void Getting_Appointments_with_expired_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.CreateAppointment(new Appointment(), GetExpiredEmployeeAuthorization())
            );
        }

        [Test]
        public void Getting_Appointments_with_invalid_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.CreateAppointment(new Appointment(), GetInvalidEmployeeAuthorization())
            );
        }

        private async Task AssertAppointmentIsStored(IAppointmentServices services, Appointment appointment)
        {
            CollectionAssert.Contains(
                await services.GetAppointments(GetEmployeeAuthorization()),
                appointment
            );
        }
    }
}
