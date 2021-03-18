using System.Collections.Generic;
using System.Threading.Tasks;


namespace Cresce.Core.Employees.GetEmployees
{
    public interface IGetEmployeesGateway
    {
        Task<IEnumerable<Employee>> GetEmployees(string organizationId);
    }
}
