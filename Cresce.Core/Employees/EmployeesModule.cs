using Cresce.Core.Employees.EmployeeValidation;
using Cresce.Core.Employees.GetEmployees;
using Cresce.Core.GetEntities;
using Microsoft.Extensions.DependencyInjection;

namespace Cresce.Core.Employees
{
    public class EmployeesModule : IServicesModule
    {
        public void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGetEmployeesService, GetEmployeesService>();
            serviceCollection.AddTransient<IGetEntitiesService<Employee>, GetEmployeesService>();
            serviceCollection.AddTransient<IEmployeeValidationService, EmployeeValidationService>();
            serviceCollection.AddTransient<IEmployeeService, EmployeeService>();
        }
    }
}
