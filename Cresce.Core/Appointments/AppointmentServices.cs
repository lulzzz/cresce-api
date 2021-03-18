using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Employees.GetEmployees;
using Cresce.Core.GetEntities;
using Microsoft.Extensions.DependencyInjection;

namespace Cresce.Core.Appointments
{
    internal class AppointmentServices : IAppointmentServices
    {
        private readonly IGetEntitiesService<Appointment> _getEntitiesGateway;
        private readonly IGetEmployeesGateway _getEmployees;

        public AppointmentServices(
            IGetEntitiesService<Appointment> getEntitiesGateway,
            IGetEmployeesGateway getEmployees
        )
        {
            _getEntitiesGateway = getEntitiesGateway;
            _getEmployees = getEmployees;
        }

        public async Task<IEnumerable<Appointment>> GetAppointments(IEmployeeAuthorization authorization)
        {
            var appointments = await _getEntitiesGateway.GetEntities(authorization);
            var employees = await _getEmployees.GetEmployees(authorization.OrganizationId);

            return appointments.Select(appointment => appointment with
            {
                Color = GetColor(employees, appointment)
            });
        }

        private static string GetColor(IEnumerable<Employee> employees, Appointment appointment)
        {
            return employees.First(e => e.Id == appointment.EmployeeId).Color;
        }
    }

    class AppointmentsModule : IServicesModule
    {
        public void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.RegisterGetEntities<Appointment>();
            serviceCollection.AddTransient<IAppointmentServices, AppointmentServices>();
        }
    }
}
