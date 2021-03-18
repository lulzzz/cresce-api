using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.GetEntities;

namespace Cresce.Core.Employees.GetEmployees
{
    public interface IGetEmployeesService : IGetEntitiesService<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployees(IAuthorization authorization, string organizationId);
    }
}
