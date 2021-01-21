using Cresce.Core.Employees.GetEmployees;
using Cresce.Core.Organizations;
using Cresce.Core.Services.GetServices;
using Cresce.Core.Sql.Employees;
using Cresce.Core.Sql.Organizations;
using Cresce.Core.Sql.Services;
using Cresce.Core.Sql.Users;
using Cresce.Core.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Cresce.Core.Sql
{
    public static class GatewaysConfiguration
    {
        public static void RegisterServices(IServiceCollection serviceCollection)
        {
            RegisterUserGateways(serviceCollection);
            RegisterOrganizationGateways(serviceCollection);
            RegisterEmployeeGateways(serviceCollection);
            RegisterServiceGateways(serviceCollection);
        }

        public static void RegisterDbContext(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<CresceContext>(builder =>
            {
                builder.UseSqlServer(connectionString);
            });
        }

        private static void RegisterServiceGateways(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGetServicesGateway, GetServicesGateway>();
        }

        private static void RegisterEmployeeGateways(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGetEmployeesGateway, GetEmployeesGateway>();
        }

        private static void RegisterOrganizationGateways(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGetUserOrganizationsGateway, GetUserOrganizationsGateway>();
        }

        private static void RegisterUserGateways(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGetUserGateway, GetUserGateway>();
        }
    }
}
