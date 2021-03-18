using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Customers;
using Cresce.Core.Employees.GetEmployees;
using Cresce.Core.GetEntities;
using Cresce.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cresce.Core.Appointments
{
    internal class AppointmentServices : IAppointmentServices
    {
        private readonly IGetEntitiesService<Appointment> _appointmentsQuery;
        private readonly IGetEntitiesService<Employee> _employeesQuery;
        private readonly IGetEntitiesService<Service> _serviceQuery;
        private readonly IGetEntitiesService<Customer> _customerQuery;

        public AppointmentServices(
            IGetEntitiesService<Appointment> appointmentsQuery,
            IGetEntitiesService<Employee> employeesQuery,
            IGetEntitiesService<Service> serviceQuery,
            IGetEntitiesService<Customer> customerQuery
        )
        {
            _appointmentsQuery = appointmentsQuery;
            _employeesQuery = employeesQuery;
            _serviceQuery = serviceQuery;
            _customerQuery = customerQuery;
        }

        public async Task<IEnumerable<Appointment>> GetAppointments(IEmployeeAuthorization authorization)
        {
            var appointments = await _appointmentsQuery.GetEntities(authorization);
            var employees = await _employeesQuery.GetEntities(authorization);
            var services = await _serviceQuery.GetEntities(authorization);
            var customers = await _customerQuery.GetEntities(authorization);

            return appointments.Select(appointment => appointment with
            {
                Color = GetColor(employees, appointment),
                EventName = MakeEventName(services, customers, appointment),
            });
        }

        private string MakeEventName(
            IEnumerable<Service> services,
            IEnumerable<Customer> customers,
            Appointment appointment)
        {
            var service = services.First(e => e.Id == appointment.ServiceId);
            var customer = customers.First(e => e.Id == appointment.CustomerId);

            return customer.Name + "\n" + service.Name;
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
